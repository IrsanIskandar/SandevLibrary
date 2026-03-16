using Konscious.Security.Cryptography;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SandevLibrary.SecurityAlgorithm;

/// <summary>
/// Argon2id password hashing utility.
/// One-way hashing (NO decrypt).
/// </summary>
public static class Argon2Algorithm
{
    private const int SaltSize = 16;       // 128-bit
    private const int HashSize = 32;       // 256-bit
    private const int MemorySizeKb = 65536; // 64 MB
    private const int Iterations = 4;
    private static readonly int DegreeOfParallelism = Environment.ProcessorCount;

    /// <summary>
    /// Hash password using Argon2id.
    /// </summary>
    public static string Hash(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentException("Value cannot be null or empty.", nameof(plainText));

        // 🔐 APPLY PEPPER HERE
        string input = SecurityPepper.Apply(plainText);

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = ComputeHash(input, salt);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    /// <summary>
    /// Verify password against Argon2 hash.
    /// </summary>
    public static bool Verify(string plainText, string hashedValue)
    {
        if (string.IsNullOrWhiteSpace(plainText) || string.IsNullOrWhiteSpace(hashedValue))
            return false;

        var parts = hashedValue.Split('.');
        if (parts.Length != 2)
            return false;

        // 🔐 APPLY PEPPER HERE
        string input = SecurityPepper.Apply(plainText);

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expectedHash = Convert.FromBase64String(parts[1]);
        byte[] actualHash = ComputeHash(input, salt);

        return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
    }

    private static byte[] ComputeHash(string plainText, byte[] salt)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(plainText))
        {
            Salt = salt,
            MemorySize = MemorySizeKb,
            Iterations = Iterations,
            DegreeOfParallelism = DegreeOfParallelism
        };

        return argon2.GetBytes(HashSize);
    }
}
