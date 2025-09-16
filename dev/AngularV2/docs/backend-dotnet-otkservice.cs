// Simple OT K service implementation using IMemoryCache for demo purposes.
// In production, use a distributed cache (Redis) or a secure DB, and sign tokens.

using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;

public interface IOtkService
{
    OneTimeToken CreateOneTimeToken(string userId, string app, TimeSpan ttl);
    OneTimeTokenInfo? ValidateAndConsume(string token);
}

public class OtkService : IOtkService
{
    private readonly IMemoryCache _cache;

    public OtkService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public OneTimeToken CreateOneTimeToken(string userId, string app, TimeSpan ttl)
    {
        var raw = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        var token = Uri.EscapeDataString(raw);
        var info = new OneTimeTokenInfo
        {
            Token = token,
            UserId = userId,
            App = app,
            ExpiresAt = DateTime.UtcNow.Add(ttl)
        };
        // store token server-side keyed by token
        _cache.Set(GetCacheKey(token), info, ttl);
        return new OneTimeToken { Token = token, ExpiresAt = info.ExpiresAt };
    }

    public OneTimeTokenInfo? ValidateAndConsume(string token)
    {
        var key = GetCacheKey(token);
        if (!_cache.TryGetValue<OneTimeTokenInfo>(key, out var info)) return null;
        // remove to ensure single-use
        _cache.Remove(key);
        if (info.ExpiresAt < DateTime.UtcNow) return null;
        return info;
    }

    private static string GetCacheKey(string token) => $"otk:{token}";
}

public class OneTimeToken { public string Token { get; set; } = default!; public DateTime ExpiresAt { get; set; } }
public class OneTimeTokenInfo { public string Token { get; set; } = default!; public string UserId { get; set; } = default!; public string App { get; set; } = default!; public DateTime ExpiresAt { get; set; } public IDictionary<string,string>? Claims { get; set; } }
