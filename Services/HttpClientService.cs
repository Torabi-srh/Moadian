using Moadian.Dto;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Services
{

    public class HttpClientService
    {
        private readonly HttpClient client;
        private readonly SignatureService signatureService;
        private readonly EncryptionService encryptionService;

        public HttpClientService(SignatureService signatureService, EncryptionService encryptionService, string baseUri = "https://tp.tax.gov.ir")
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.signatureService = signatureService;
            this.encryptionService = encryptionService;
        }

        public async Task<object?> SendPacket(string path, Packet packet, Dictionary<string, string> headers)
        {
            var cloneHeader = new Dictionary<string, string>(headers);

            if (cloneHeader.ContainsKey("Authorization"))
            {
                cloneHeader["Authorization"] = cloneHeader["Authorization"].Replace("Bearer ", "");
            }
            var pack = packet.ToArray();
            foreach (var item in cloneHeader)
            {
                pack.Add(item.Key, item.Value);
            }
            var normalizedData = Normalizer.NormalizeArray(pack);
            var signature = signatureService.Sign(normalizedData);

            var content = new Dictionary<string, object>
            {
                { "packet", packet.ToArray() },
                { "signature", signature }
            };
            string contentJson = JsonConvert.SerializeObject(content);
            var response = await Post(path, contentJson, headers);
            var stringData = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(stringData);
        }

        public async Task<object> SendPackets(string path, List<Packet> packets, Dictionary<string, string> headers, bool encrypt = false, bool sign = false)
        {
            headers = FillEssentialHeaders(headers);

            if (sign)
            {
                foreach (var packet in packets)
                {
                    SignPacket(packet);
                }
            }

            if (encrypt)
            {
                packets = EncryptPackets(packets);
            }

            var cloneHeader = new Dictionary<string, string>(headers);
            cloneHeader["Authorization"] = cloneHeader["Authorization"].Replace("Bearer ", "");
            var pack = packets[0].ToArray();
            foreach (var item in cloneHeader)
            {
                pack.Add(item.Key, item.Value);
            }
            var normalized = Normalizer.NormalizeArray(pack);
            var signature = signatureService.Sign(normalized);

            var content = new Dictionary<string, object>
        {
            { "packets", packets.ConvertAll(p => p.ToArray()) },
            { "signature", signature },
            { "signatureKeyId", null }
        };

            return await Post(path, JsonConvert.SerializeObject(content), headers);
        }

        private void SignPacket(Packet packet)
        {
            var normalized = Normalizer.NormalizeArray(packet.data is string ? null : ((PacketDataInterface)packet.data)?.ToArray());
            var signature = signatureService.Sign(normalized);

            packet.dataSignature = signature;
            // TODO: Not sure?
            // packet.SetSignatureKeyId(signatureService.GetKeyId());
        }

        private List<Packet> EncryptPackets(List<Packet> packets)
        {
            var aesHex = BitConverter.ToString(RandomNumberGenerator.GetBytes(32)).Replace("-", "");
            var iv = BitConverter.ToString(RandomNumberGenerator.GetBytes(16)).Replace("-", "");
            var encryptedAesKey = encryptionService.EncryptAesKey(aesHex);

            foreach (var packet in packets)
            {
                packet.iv = iv;
                packet.symmetricKey = encryptedAesKey;
                packet.encryptionKeyId = encryptionService.GetEncryptionKeyId();
                packet.data = (Encoding.UTF8.GetBytes(encryptionService.Encrypt(JsonConvert.SerializeObject(packet.data is string ? packet.data : ((PacketDataInterface)packet.data)?.ToArray()), hex2bin(aesHex), hex2bin(iv))));
            }

            return packets;
        }
        private async Task<HttpResponseMessage> Post(string path, string content, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return await client.SendAsync(request);
        }

        /**
         * @param Dictionary<string, string> headers
         * @return Dictionary<string, string>
         */
        private Dictionary<string, string> FillEssentialHeaders(Dictionary<string, string> headers)
        {
            if (!headers.ContainsKey(Constants.TransferConstants.TIMESTAMP_HEADER))
            {
                headers.Add(Constants.TransferConstants.TIMESTAMP_HEADER, "1678654079000");
            }

            if (!headers.ContainsKey(Constants.TransferConstants.REQUEST_TRACE_ID_HEADER))
            {
                headers.Add(Constants.TransferConstants.REQUEST_TRACE_ID_HEADER, "AAA");
            }

            return headers;
        }

        private byte[] hex2bin(string hexdata)
        {
            if (hexdata == null)
                throw new ArgumentNullException("hexdata");
            if (hexdata.Length % 2 != 0)
                throw new ArgumentException("hexdata should have even length");

            byte[] bytes = new byte[hexdata.Length / 2];
            for (int i = 0; i < hexdata.Length; i += 2)
                bytes[i / 2] = (byte)(HexValue(hexdata[i]) * 0x10
                + HexValue(hexdata[i + 1]));
            return bytes;
        }
        private int HexValue(char c)
        {
            int ch = (int)c;
            if (ch >= (int)'0' && ch <= (int)'9')
                return ch - (int)'0';
            if (ch >= (int)'a' && ch <= (int)'f')
                return ch - (int)'a' + 10;
            if (ch >= (int)'A' && ch <= (int)'F')
                return ch - (int)'A' + 10;
            throw new ArgumentException("Not a hexadecimal digit.");
        }
    }
}
