using System;

namespace SandevLibrary.SecurityAlgorithm;

/// <summary>
/// BCrypt password hashing utility.
/// One-way hashing (NO decrypt).
/// </summary>
public static class BCryptAlgorithm
{
    private const int WorkFactor = 12;

    /// <summary>
    /// Hash password using BCrypt.
    /// </summary>
    public static string Hash(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
            throw new ArgumentException("Value cannot be null or empty.", nameof(plainText));

        // 🔐 APPLY PEPPER HERE
        string input = SecurityPepper.Apply(plainText);

        return BCrypt.Net.BCrypt.HashPassword(input, WorkFactor);
    }

    /// <summary>
    /// Verify password against BCrypt hash.
    /// </summary>
    public static bool Verify(string plainText, string hashedValue)
    {
        if (string.IsNullOrWhiteSpace(plainText) || string.IsNullOrWhiteSpace(hashedValue))
            return false;

        // 🔐 APPLY PEPPER HERE
        string input = SecurityPepper.Apply(plainText);

        return BCrypt.Net.BCrypt.Verify(input, hashedValue);
    }
}
