using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
namespace Ultimate.IntegrationSystem.Api.Infrastructure.Security
{


        public interface ISecretCipher
        {
            string Encrypt(string plain);
            string Decrypt(string cipherBase64);
        }

        /// <summary>
        /// AES-256 مع IV عشوائي مُضمّن في بداية النص المشفّر (prefix).
        /// المفتاح يجب أن يأتي من ENV: CONFIG_MASTER_KEY (Base64 بطول 32 بايت).
        /// </summary>
        public sealed class AesSecretCipher : ISecretCipher
        {
            private readonly byte[] _key; // 32 bytes

            public AesSecretCipher(IConfiguration cfg)
            {
                var keyB64 = cfg["CONFIG_MASTER_KEY"]
                    ?? throw new InvalidOperationException("Missing ENV CONFIG_MASTER_KEY (Base64 32 bytes).");
                _key = Convert.FromBase64String(keyB64);
                if (_key.Length != 32)
                    throw new InvalidOperationException("CONFIG_MASTER_KEY must decode to 32 bytes.");
            }

            public string Encrypt(string plain)
            {
                using var aes = Aes.Create();
                aes.Key = _key;
                aes.GenerateIV(); // IV عشوائي لكل عملية

                using var ms = new MemoryStream();
                ms.Write(aes.IV, 0, aes.IV.Length); // نحفظ الـ IV في مقدمة البايتات

                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                    sw.Write(plain);

                return Convert.ToBase64String(ms.ToArray());
            }

            public string Decrypt(string cipherBase64)
            {
                var data = Convert.FromBase64String(cipherBase64);
                using var aes = Aes.Create();
                aes.Key = _key;

                var iv = new byte[16];
                Array.Copy(data, 0, iv, 0, iv.Length);
                aes.IV = iv;

                using var ms = new MemoryStream(data, iv.Length, data.Length - iv.Length);
                using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
        }
    

}
