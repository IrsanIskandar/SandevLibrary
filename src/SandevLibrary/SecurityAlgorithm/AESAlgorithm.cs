using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SandevLibrary.SecurityAlgorithm
{
    public class AESAlgorithm
    {
        private const string _KEY = "*#SaNd3V+c0rP0r4TioN#*gCjKDZGCYb+KIgdqKfFGQ82n2w8Gi+At1qCAvbeuHFUsefk";
        private const string _IV = "47l5QsSe1POo31adQ/u7nQ==";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesToBeEncrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            try
            {
                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        encryptedBytes = ms.ToArray();
                    }
                    ms.Close();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Your Exception : " + ex.Message);
            }

            return encryptedBytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytesToBeDecrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            try
            {
                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                    ms.Close();
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Your Exception : " + ex.Message);
            }

            return decryptedBytes;
        }

        #region AES Text Encryption
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptText(string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(password);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(_KEY);

            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            string result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string DecryptText(string password)
        {
            // Get the bytes of the string
            byte[] bytesToBeDecrypted = Convert.FromBase64String(password);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(_KEY);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            string result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }
        #endregion

        #region Getting Randomized Encryption Result with Salt
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptTextWithSalt(string plainText)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(_KEY);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(plainText);

            byte[] baSalt = GetRandomBytes();
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];

            baEncrypted = AES_Encrypt(baEncrypted, baPwdHash);

            string result = Convert.ToBase64String(baEncrypted);


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string DecryptTextWithSalt(string plainText)
        {
            byte[] baPwd = Encoding.UTF8.GetBytes(_KEY);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Convert.FromBase64String(plainText);

            byte[] baDecrypted = AES_Decrypt(baText, baPwdHash);

            // Remove salt
            int saltLength = GetSaltLength();
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];

            string result = Encoding.UTF8.GetString(baResult);


            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static byte[] GetRandomBytes()
        {
            int saltLength = GetSaltLength();
            byte[] ba = new byte[saltLength];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetSaltLength()
        {
            return 164;
        }
        #endregion
    }
}
