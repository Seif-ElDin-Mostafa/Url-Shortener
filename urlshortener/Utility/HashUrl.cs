using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace urlshortener.Utility;

public static class HashUrl
{
    public static string GenerateShortUrl(string originalUrl, int userId)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
        var combinedUrl = $"{originalUrl}{timestamp}{userId}";

        var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(combinedUrl);
        var hash = sha256.ComputeHash(bytes);
        var shortUrl = BitConverter.ToString(hash)
            .Replace("-", "")
            .Substring(0, 8); // Use the first 8 characters of the hash for the short URL

        return shortUrl.ToLowerInvariant();
    }
}
