
using System.Security.Cryptography;

public class ChecksumHelper
{
    public static string CalculateSha256(string filePath)
    {
        // Use SHA256.Create() to initialize the algorithm
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(filePath);
        
        // Compute the hash byte array
        byte[] hashBytes = sha256.ComputeHash(stream);
        
        // Convert the byte array to a readable hex string
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}