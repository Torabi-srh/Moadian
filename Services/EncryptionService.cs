using System;
using System.Security.Cryptography;
using System.Text;

namespace Moadian.Services
{
    public class EncryptionService
    {
        private const string CIPHER = "aes-256-gcm";
        private const int TAG_LENGTH = 16;

        private readonly RSA taxOrgPublicKey;
        private readonly string encryptionKeyId;

        public EncryptionService(string publicKey, string encryptionKeyId)
        {
            //RSACryptoServiceProvider provider = PemKeyUtils.GetRSAProviderFromPemFile(taxOrgPublicKey, false);
            //this.taxOrgPublicKey = provider;
            this.taxOrgPublicKey = RSA.Create();
            string pemstr = File.ReadAllText(publicKey).Trim();
            this.taxOrgPublicKey.ImportFromPem(pemstr.ToCharArray());
            this.encryptionKeyId = encryptionKeyId;
        }

        public string Encrypt(string text, byte[] key, byte[] iv)
        {
            text = XorStrings(text, Encoding.UTF8.GetString(key));

            var cipher = new AesGcm(key);
            byte[] tag = new byte[TAG_LENGTH];

            byte[] cipherText = new byte[text.Length];
            cipher.Encrypt(iv, Encoding.UTF8.GetBytes(text), cipherText, tag);

            byte[] encrypted = new byte[cipherText.Length + tag.Length];
            Array.Copy(cipherText, encrypted, cipherText.Length);
            Array.Copy(tag, 0, encrypted, cipherText.Length, tag.Length);

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string encryptedText, byte[] key, byte[] iv)
        {
            byte[] encrypted = Convert.FromBase64String(encryptedText);

            byte[] cipherText = new byte[encrypted.Length - TAG_LENGTH];
            Array.Copy(encrypted, cipherText, cipherText.Length);

            byte[] tag = new byte[TAG_LENGTH];
            Array.Copy(encrypted, cipherText.Length, tag, 0, tag.Length);

            var cipher = new AesGcm(key);
            byte[] decrypted = new byte[cipherText.Length];
            cipher.Decrypt(iv, cipherText, tag, decrypted);

            string text = Encoding.UTF8.GetString(decrypted);

            return XorStrings(text, Encoding.UTF8.GetString(key));
        }

        public string EncryptAesKey(string aesKey)
        {
            byte[] encryptedKey = taxOrgPublicKey.Encrypt(Encoding.UTF8.GetBytes(aesKey), RSAEncryptionPadding.Pkcs1);
            return Convert.ToBase64String(encryptedKey);
        }

        public string GetEncryptionKeyId()
        {
            return encryptionKeyId;
        }

        private static string XorStrings(string source, string key)
        {
            int sourceLength = source.Length;
            int keyLength = key.Length;
            StringBuilder result = new StringBuilder(sourceLength);
            for (int i = 0; i < sourceLength; i++)
            {
                result.Append((char)(source[i] ^ key[i % keyLength]));
            }
            return result.ToString();
        }
    }
}
