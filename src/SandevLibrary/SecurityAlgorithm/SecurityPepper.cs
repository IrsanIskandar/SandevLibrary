using System;

namespace SandevLibrary.SecurityAlgorithm;

/// <summary>
/// Global pepper configuration for hashing algorithms.
/// Should be stored in environment variable or secure vault.
/// </summary>
public static class SecurityPepper
{
    private static string? _pepper;

    public static void Set(string pepper)
    {
        if (string.IsNullOrWhiteSpace(pepper))
            throw new ArgumentException("Pepper cannot be empty.");

        _pepper = pepper;
    }

    internal static string Apply(string value)
    {
        return _pepper is null
            ? value
            : string.Concat(value, _pepper);
    }
}
