using Moadian.Dto;
using Moadian.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.API
{
    public class Api
    {
        private TokenModel? token = null;
        private readonly string username;
        private readonly HttpClientService httpClient;

        public Api(string username, HttpClientService httpClient)
        {
            this.username = username;
            this.httpClient = httpClient;
        }

        public async Task<TokenModel> GetToken()
        {
            var getTokenDto = new GetTokenDto() { username = this.username };
            var packet = new Packet(Constants.PacketType.GET_TOKEN, getTokenDto);

            packet.retry = false;
            packet.fiscalId = this.username;

            var headers = GetEssentialHeaders();

            var response = await this.httpClient.SendPacket("req/api/self-tsp/sync/GET_TOKEN", packet, headers);
            return null;
            //var tokenData = response["result"]["data"];
            //return new TokenModel(tokenData["token"], tokenData["expiresIn"]);
        }

        public async Task<dynamic> InquiryByReferenceNumberAsync(string referenceNumber)
        {
            var inquiryByReferenceNumberDto = new InquiryByReferenceNumberDto();
            inquiryByReferenceNumberDto.SetReferenceNumber(referenceNumber);

            var packet = new Packet(Constants.PacketType.PACKET_TYPE_INQUIRY_BY_REFERENCE_NUMBER, inquiryByReferenceNumberDto);

            packet.retry = false;
            packet.fiscalId = this.username;

            var headers = GetEssentialHeaders();
            headers["Authorization"] = "Bearer " + this.token?.Token;

            var path = "req/api/self-tsp/sync/" + Constants.PacketType.PACKET_TYPE_INQUIRY_BY_REFERENCE_NUMBER;

            return await this.httpClient.SendPacket(path, packet, headers);
        }

        public async Task<dynamic> GetEconomicCodeInformationAsync(string taxID)
        {
            RequireToken();

            var packet = new Packet(Constants.PacketType.GET_ECONOMIC_CODE_INFORMATION, JsonConvert.SerializeObject(new { economicCode = taxID }));

            packet.retry = false;
            packet.fiscalId = this.username;

            var headers = GetEssentialHeaders();
            headers["Authorization"] = "Bearer " + this.token?.Token;

            var path = "req/api/self-tsp/sync/" + Constants.PacketType.GET_ECONOMIC_CODE_INFORMATION;

            return await this.httpClient.SendPacket(path, packet, headers);
        }

        public async Task<dynamic> SendInvoicesAsync(List<object> invoiceDtos)
        {
            var packets = new List<Packet>();

            foreach (var invoiceDto in invoiceDtos)
            {
                var packet = new Packet(Constants.PacketType.INVOICE_V01, invoiceDto);
                packet.uid = "AAA";
                packets.Add(packet);
            }

            var headers = GetEssentialHeaders();

            headers[Constants.TransferConstants.AUTHORIZATION_HEADER] = this.token?.Token;

            dynamic res = null;
            try
            {
                res = await this.httpClient.SendPackets("req/api/self-tsp/async/normal-enqueue", packets, headers, true, true);
            }
            catch (Exception e)
            {
            }

            return res?.GetBody().GetContents();
        }

        public async Task<dynamic> GetFiscalInfoAsync()
        {
            RequireToken();

            var packet = new Packet(Constants.PacketType.GET_FISCAL_INFORMATION, this.username);

            var headers = GetEssentialHeaders();

            headers["Authorization"] = "Bearer " + this.token?.Token;

            return await this.httpClient.SendPacket("req/api/self-tsp/sync/GET_FISCAL_INFORMATION", packet, headers);
        }

        public Api SetToken(TokenModel? token)
        {
            this.token = token;
            return this;
        }
        public dynamic InquiryByReferenceNumber(string referenceNumber)
        {
            var inquiryByReferenceNumberDto = new InquiryByReferenceNumberDto();
            inquiryByReferenceNumberDto.SetReferenceNumber(referenceNumber);

            var packet = new Packet(Constants.PacketType.PACKET_TYPE_INQUIRY_BY_REFERENCE_NUMBER, inquiryByReferenceNumberDto);

            packet.retry = false;
            packet.fiscalId = this.username;
            var headers = GetEssentialHeaders();
            headers["Authorization"] = "Bearer " + this.token.Token;

            var path = "req/api/self-tsp/sync/" + Constants.PacketType.PACKET_TYPE_INQUIRY_BY_REFERENCE_NUMBER;

            return this.httpClient.SendPacket(path, packet, headers);
        }

        public dynamic GetEconomicCodeInformation(string taxId)
        {
            RequireToken();

            var packet = new Packet(Constants.PacketType.GET_ECONOMIC_CODE_INFORMATION, JsonConvert.SerializeObject(new { EconomicCode = taxId }));

            packet.retry = false;
            packet.fiscalId = this.username;
            var headers = GetEssentialHeaders();
            headers["Authorization"] = "Bearer " + this.token.Token;

            var path = "req/api/self-tsp/sync/" + Constants.PacketType.GET_ECONOMIC_CODE_INFORMATION;

            return this.httpClient.SendPacket(path, packet, headers);
        }

        public dynamic SendInvoices(List<InvoiceDto> invoiceDtos)
        {
            var packets = new List<Packet>();

            foreach (var invoiceDto in invoiceDtos)
            {
                var packet = new Packet(Constants.PacketType.INVOICE_V01, invoiceDto);
                packet.uid = "AAA";
                packets.Add(packet);
            }

            var headers = GetEssentialHeaders();

            headers[Constants.TransferConstants.AUTHORIZATION_HEADER] = this.token.Token;

            dynamic res = null;

            try
            {
                res = this.httpClient.SendPackets(
                    "req/api/self-tsp/async/normal-enqueue",
                    packets,
                    headers,
                    true,
                    true
                );
            }
            catch (Exception e)
            {
            }

            return res?.GetBody()?.GetContents();
        }

        public dynamic GetFiscalInfo()
        {
            RequireToken();

            var packet = new Packet(Constants.PacketType.GET_FISCAL_INFORMATION, this.username);

            var headers = GetEssentialHeaders();

            headers["Authorization"] = "Bearer " + this.token.Token;

            return this.httpClient.SendPacket("req/api/self-tsp/sync/GET_FISCAL_INFORMATION", packet, headers);
        }

        private Dictionary<string, string> GetEssentialHeaders()
        {
            return new Dictionary<string, string>
            {
                { Constants.TransferConstants.TIMESTAMP_HEADER, DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString() },
                { Constants.TransferConstants.REQUEST_TRACE_ID_HEADER, Guid.NewGuid().ToString() }
            };
        }

        private async void RequireToken()
        {
            if (this.token == null || this.token.IsExpired())
            {
                this.token = await this.GetToken();
            }
        }

    }
}
