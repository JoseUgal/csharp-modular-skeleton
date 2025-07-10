using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Caching;

public static class CacheOptions
{
    private static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
    };

    public static DistributedCacheEntryOptions Create(TimeSpan? expiration)
    {
        if (expiration.HasValue)
        {
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration.Value
            };
        }

        return DefaultExpiration;
    }
}
