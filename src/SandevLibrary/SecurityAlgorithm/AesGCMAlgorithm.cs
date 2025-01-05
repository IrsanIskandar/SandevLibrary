using System;
using System.Security.Cryptography;
using System.Text;

namespace SandevLibrary.SecurityAlgorithm;

public class AesGCMAlgorithm
{
    public static string EncryptAESGCM(string plainText, string password)
    {
        try
        {
            // Generate salt (16 bytes) dan IV (12 bytes)
            byte[] salt = new byte[16];
            byte[] iv = new byte[12];
            using (RNGCryptoServiceProvider rng = new())
            {
                rng.GetBytes(salt);
                rng.GetBytes(iv);
            }

            // Derivasi Key menggunakan PBKDF2
            using Rfc2898DeriveBytes rfc2898 = new(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] key = rfc2898.GetBytes(32); // AES-256 membutuhkan 32 byte kunci

            using AesGcm aes = new(key);
            byte[] encryptedText = new byte[plainText.Length];
            byte[] authTag = new byte[16]; // Authentication Tag 16 bytes
            aes.Encrypt(iv, Encoding.UTF8.GetBytes(plainText), encryptedText, authTag);

            // Gabungkan Salt + IV + Ciphertext + Tag
            byte[] combinedData = new byte[salt.Length + iv.Length + encryptedText.Length + authTag.Length];
            Buffer.BlockCopy(salt, 0, combinedData, 0, salt.Length);
            Buffer.BlockCopy(iv, 0, combinedData, salt.Length, iv.Length);
            Buffer.BlockCopy(encryptedText, 0, combinedData, salt.Length + iv.Length, encryptedText.Length);
            Buffer.BlockCopy(authTag, 0, combinedData, salt.Length + iv.Length + encryptedText.Length, authTag.Length);

            return Convert.ToBase64String(combinedData);
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Encryption failed: " + ex.Message);
        }
    }

    public static string DecryptAESGCM(string cipherTextBase64, string password)
    {
        try
        {
            byte[] cipherText = Convert.FromBase64String(cipherTextBase64);

            // Ekstrak Salt, IV, Ciphertext, dan Tag
            byte[] salt = cipherText[..16];
            byte[] iv = cipherText[16..28];

            // Ciphertext diambil sampai 16 byte sebelum akhir
            byte[] encryptedText = cipherText[28..(cipherText.Length - 16)];

            // Tag autentikasi adalah 16 byte terakhir
            byte[] authTag = cipherText[^16..];

            // Derivasi Key dengan PBKDF2
            using Rfc2898DeriveBytes rfc2898 = new(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] key = rfc2898.GetBytes(32); // AES-256 membutuhkan 32 byte kunci

            using AesGcm aes = new(key);
            byte[] decryptedText = new byte[encryptedText.Length];
            aes.Decrypt(iv, encryptedText, authTag, decryptedText);
            return Encoding.UTF8.GetString(decryptedText);
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Decryption failed: " + ex.Message);
        }
    }
}
