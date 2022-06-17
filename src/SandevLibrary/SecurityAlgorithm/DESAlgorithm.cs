using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SandevLibrary.SecurityAlgorithm
{
    public class DESAlgorithm
    {
        private const string _securityKey = "*#SaNd3V+c0rP0r4TioN#*gCjKDZGCYb+KIgdqKfFGQ82n2w8Gi+At1qCAvbeuHFUsefk";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="PlainText"></param>
        /// <returns></returns>
        public static string EncryptPlainTextToCipherText(string PlainText)
        {
            //Getting the bytes of Input String.
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();

            //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));

            //De-allocatinng the memory after doing the Job.
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            //Assigning the Security key to the TripleDES Service Provider.
            objTripleDESCryptoService.Key = securityKeyArray;

            //Mode of the Crypto service is Electronic Code Book.
            objTripleDESCryptoService.Mode = CipherMode.ECB;

            //Padding Mode is PKCS7 if there is any extra byte is added.
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();

            //Transform the bytes array to resultArray
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);

            //Releasing the Memory Occupied by TripleDES Service Provider for Encryption.
            objTripleDESCryptoService.Clear();

            //Convert and return the encrypted data/byte into string format.
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="CipherText"></param>
        /// <returns></returns>
        public static string DecryptCipherTextToPlainText(string CipherText)
        {
            byte[] toEncryptArray = Convert.FromBase64String(CipherText);

            using (MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider())
            {
                //Gettting the bytes from the Security Key and Passing it to compute the Corresponding Hash Value.
                byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));

                //De-allocatinng the memory after doing the Job.
                objMD5CryptoService.Clear();
                using (TripleDESCryptoServiceProvider objTripleDESCryptoService = new TripleDESCryptoServiceProvider())
                {
                    //Assigning the Security key to the TripleDES Service Provider.
                    objTripleDESCryptoService.Key = securityKeyArray;

                    //Mode of the Crypto service is Electronic Code Book.
                    objTripleDESCryptoService.Mode = CipherMode.ECB;

                    //Padding Mode is PKCS7 if there is any extra byte is added.
                    objTripleDESCryptoService.Padding = PaddingMode.PKCS7;

                    var objCrytpoTransform = objTripleDESCryptoService.CreateDecryptor();

                    //Transform the bytes array to resultArray
                    byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    //Releasing the Memory Occupied by TripleDES Service Provider for Decryption.
                    objTripleDESCryptoService.Clear();

                    //Convert and return the decrypted data/byte into string format.
                    return UTF8Encoding.UTF8.GetString(resultArray);
                }
            }
        }

        /// <summary>
        /// Encrypt a string using dual encryption method. Return a encrypted cipher Text
        /// </summary>
        /// <param name="toEncrypt">string to be encrypted</param>
        /// <param name="useHashing">use hashing? send to for extra secirity</param>
        /// <returns></returns>
        public static string EncryptWithHasing(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            AppSettingsReader settingsReader = new AppSettingsReader();
            // Get the key from config file
            //string key = (string)settingsReader.GetValue(_securityKey, typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(_securityKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// DeCrypt a string using dual encryption method. Return a DeCrypted clear string
        /// </summary>
        /// <param name="cipherString">encrypted string</param>
        /// <param name="useHashing">Did you use hashing to encrypt this data? pass true is yes</param>
        /// <returns></returns>
        public static string DecryptWithHasing(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            //string key = (string)settingsReader.GetValue(_securityKey, typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_securityKey));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(_securityKey);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
