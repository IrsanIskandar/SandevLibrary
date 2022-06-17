using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SandevLibrary.SecurityAlgorithm
{
    public class RC2Algorithm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptString(string message, string plainText)
        {
            byte[] byresToBeEncrypted = Encoding.UTF8.GetBytes(message);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] bytesToBeEncrypted = RC2_Encrypt(byresToBeEncrypted, passwordBytes);
            string result = Convert.ToBase64String(bytesToBeEncrypted);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="chiperText"></param>
        /// <returns></returns>
        public static string DecryptString(string message, string chiperText)
        {
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(message);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(chiperText);
            byte[] bytesToBeDecrypted = RC2_Decrypt(bytesToBeEncrypted, passwordBytes);
            string result = Encoding.UTF8.GetString(bytesToBeDecrypted);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesToBeEncrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns></returns>
        private static byte[] RC2_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            string salt = "ONL1N3B4S3D3DUC4";
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            using (MemoryStream msStream = new MemoryStream())
            {
                using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                {
                    RC2.KeySize = 128;
                    RC2.BlockSize = 64;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    RC2.Key = key.GetBytes(RC2.KeySize / 8);
                    RC2.IV = key.GetBytes(RC2.BlockSize / 8);
                    RC2.UseSalt = true;
                    RC2.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(msStream, RC2.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = msStream.ToArray();
                }
            }
            return encryptedBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesToBeDecrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns></returns>
        private static byte[] RC2_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            string salt = "ONL1N3B4S3D3DUC4";
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            using (MemoryStream msStream = new MemoryStream())
            {
                using (RC2CryptoServiceProvider RC2 = new RC2CryptoServiceProvider())
                {
                    RC2.KeySize = 128;
                    RC2.BlockSize = 64;
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    RC2.Key = key.GetBytes(RC2.KeySize / 8);
                    RC2.IV = key.GetBytes(RC2.BlockSize / 8);
                    RC2.UseSalt = true;
                    RC2.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(msStream, RC2.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        //cs.ReadByte();
                        cs.Close();
                    }
                    decryptedBytes = msStream.ToArray();
                }
            }
            return decryptedBytes;
        }
    }
}
