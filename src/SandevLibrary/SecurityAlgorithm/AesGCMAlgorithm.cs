using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SandevLibrary.SecurityAlgorithm;

public class AesGCMAlgorithm
{
    //private static readonly int _keySize = 32; ///32 = 256bits encryption key (32 * 8).
    //private static readonly int _nonceSize = AesGcm.NonceByteSizes.MaxSize;
    //private static readonly int _tagSize = AesGcm.TagByteSizes.MaxSize;

    private static readonly int _nonceSize = 12;
    private static readonly int _tagSize = 16;

    //private static int KeySize { get { return _keySize; } }
    private static int NonceSize { get { return _nonceSize; } }
    private static int TagSize { get { return _tagSize; } }

    public static string GenKey(int keySize)
    {
        byte[] key = new byte[keySize]; // 8, 16, 32 size encryption key.
                                        // filling the arrays with strong random bytes.
        RandomNumberGenerator.Fill(key);

        // Converting the key into base 64 strings for easier manipulation.
        string keyString = Convert.ToBase64String(key);

        return keyString;
    }

    public static string Encrypt(string text, string base64key, string? authenticationTag = null)
    {
        byte[] key = Convert.FromBase64String(base64key);
        byte[] nonce = new byte[NonceSize]; // Nonce (12 bytes) fixed size
        byte[] tag = new byte[TagSize]; // Tag size is 16 bytes for AES-GCM

        // Generate random nonce
        RandomNumberGenerator.Fill(nonce);

        byte[] cipherText = new byte[text.Length];
        byte[] encryptedText;
        
        using (AesGcm cipher = new(key))
        {
            cipher.Encrypt(
                nonce,
                Encoding.UTF8.GetBytes(text),
                cipherText,
                tag,
                authenticationTag != null ? Encoding.UTF8.GetBytes(authenticationTag) : null
            );

            encryptedText = Concat(nonce, Concat(cipherText, tag)); // Concatenate Nonce, CipherText, and Tag
        }

        return Convert.ToBase64String(encryptedText);
    }

    public static string Decrypt(string encryptedText, string base64key, string? authenticationTag = null)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        byte[] key = Convert.FromBase64String(base64key);

        byte[] nonce = SubArray(encryptedBytes, 0, NonceSize); // 12 bytes nonce
        byte[] tag = SubArray(encryptedBytes, encryptedBytes.Length - 16, TagSize); // 16 bytes tag
        byte[] cipherText = SubArray(encryptedBytes, 12, encryptedBytes.Length - 12 - 16); // CipherText

        byte[] decryptedText = new byte[cipherText.Length];

        using (AesGcm cipher = new(key))
        {
            cipher.Decrypt(
                nonce,
                cipherText,
                tag,
                decryptedText,
                authenticationTag != null ? Encoding.UTF8.GetBytes(authenticationTag) : null
            );
        }

        return Encoding.UTF8.GetString(decryptedText);
    }

    private static byte[] Concat(byte[] a, byte[] b)
    {
        byte[] result = new byte[a.Length + b.Length];
        Array.Copy(a, 0, result, 0, a.Length);
        Array.Copy(b, 0, result, a.Length, b.Length);
        return result;
    }

    private static byte[] SubArray(byte[] data, int start, int length)
    {
        byte[] result = new byte[length];
        Array.Copy(data, start, result, 0, length);
        return result;
    }
}
