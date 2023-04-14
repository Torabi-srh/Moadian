using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Moadian.Services
{

    public class SignatureService
    {
        private readonly string keyId;
        private readonly RSA taxOrgPrivateKey;

        public SignatureService(string privateKey, string keyId = null)
        {
            // RSACryptoServiceProvider provider = PemKeyUtils.GetRSAProviderFromPemFile(privateKey, true); 
            // this.taxOrgPrivateKey = provider;
            taxOrgPrivateKey = RSA.Create();
            string pemstr = File.ReadAllText(privateKey).Trim();
            taxOrgPrivateKey.ImportFromPem(pemstr.ToCharArray());
            this.keyId = keyId;
        }

        public string Sign(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);

            byte[] signature = this.taxOrgPrivateKey.SignData(data, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            return Convert.ToBase64String(signature);
        }

        public string GetKeyId()
        {
            return keyId;
        }
    }
}
