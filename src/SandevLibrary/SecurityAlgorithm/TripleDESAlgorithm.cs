using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SandevLibrary.SecurityAlgorithm
{
    public class TripleDESAlgorithm
    {
        //private const string mysecurityKey = "MyTestSampleKey";

        public static string Encrypt(string TextToEncrypt, string securityKey)
        {
            byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(TextToEncrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));

            MyMD5CryptoService.Clear();

            TripleDESCryptoServiceProvider MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();

            byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);

            MyTripleDESCryptoService.Clear();

            return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
        }

        public static string Decrypt(string TextToDecrypt, string securityKey)
        {
            byte[] MyDecryptArray = Convert.FromBase64String(TextToDecrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();

            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(securityKey));

            MyMD5CryptoService.Clear();

            TripleDESCryptoServiceProvider MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();

            MyTripleDESCryptoService.Key = MysecurityKeyArray;

            MyTripleDESCryptoService.Mode = CipherMode.ECB;

            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService.CreateDecryptor();

            byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyDecryptArray, 0, MyDecryptArray.Length);

            MyTripleDESCryptoService.Clear();

            return UTF8Encoding.UTF8.GetString(MyresultArray);
        }

        private static void EncryptFile(String inName, String outName, byte[] desKey, byte[] desIV)
        {
            //Create the file streams to handle the input and output files.  
            FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
            FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            //Create variables to help with read and write.  
            byte[] bin = new byte[100]; //This is intermediate storage for the encryption.  
            long rdlen = 0; //This is the total number of bytes written.  
            long totlen = fin.Length; //This is the total length of the input file.  
            int len; //This is the number of bytes to be written at a time.  
            DES des = new DESCryptoServiceProvider();
            CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);
            Console.WriteLine("Encrypting...");
            //Read from the input file, then encrypt and write to the output file.  
            while (rdlen < totlen)
            {
                len = fin.Read(bin, 0, 100);
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
                Console.WriteLine("{0} bytes processed", rdlen);
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }
    }
}
