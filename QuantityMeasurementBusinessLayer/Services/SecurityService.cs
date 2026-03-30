using Microsoft.Extensions.Configuration;
using QuantityMeasurementBusinessLayer.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QuantityMeasurementBusinessLayer.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly byte[] _key;

        public SecurityService(IConfiguration configuration)
        {
            var keyStr = configuration["Encryption:Key"] ?? throw new ArgumentNullException("Encryption key missing");
            // Must be legally sized AES key (e.g., 32 characters for AES-256)
            _key = Encoding.UTF8.GetBytes(keyStr.PadRight(32).Substring(0, 32));
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length); // prepend IV for decoding
            
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            var fullCipher = Convert.FromBase64String(cipherText);
            using var aes = Aes.Create();
            aes.Key = _key;
            
            var iv = new byte[aes.BlockSize / 8];
            var cipher = new byte[fullCipher.Length - iv.Length];
            
            Array.Copy(fullCipher, iv, iv.Length);
            Array.Copy(fullCipher, iv.Length, cipher, 0, cipher.Length);
            aes.IV = iv;
            
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipher);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
    }
}
