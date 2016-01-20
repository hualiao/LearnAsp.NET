using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace WebApp.Helpers
{
    public class AspNetCacheProvider<T> : ICacheProvider<T>
    {
        #region ICacheProvider<T> Members

        public T Fetch(string key, Func<T> retrieveData,
            DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            return FetchAndCache<T>(key, retrieveData,
                absoluteExpiry, relativeExpiry);
        }

        public T Fetch(string key, Func<T> retrieveData,
            DateTime? absoluteExpiry, TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies)
        {
            return FetchAndCache<T>(key, retrieveData,
                absoluteExpiry, relativeExpiry, cacheDependencies);
        }

        public IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData,
                DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            return FetchAndCache<IEnumerable<T>>(key, retrieveData,
                    absoluteExpiry, relativeExpiry);
        }

        public IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData,
            DateTime? absoluteExpiry, TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies)
        {
            return FetchAndCache<IEnumerable<T>>(key, retrieveData,
                    absoluteExpiry, relativeExpiry, cacheDependencies);
        }

        #endregion

        #region Helper Methods

        private U FetchAndCache<U>(string key, Func<U> retrieveData,
            DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            U value;
            if(!TryGetValue<U>(key,out value))
            {
                value=retrieveData();
                if(!absoluteExpiry.HasValue)
                    absoluteExpiry=Cache.NoAbsoluteExpiration;

                if(!relativeExpiry.HasValue)
                    relativeExpiry=Cache.NoSlidingExpiration;

                HttpContext.Current.Cache.Insert(key,value,
                    null,absoluteExpiry.Value,relativeExpiry.Value);
            }
            return value;
        }

        private bool TryGetValue<U>(string key, out U value)
        {
            object cachedValue = HttpContext.Current.Cache.Get(key);
            if (cachedValue == null)
            {
                value = default(U);
                return false;
            }
            else
            {
                try
                {
                    value = (U)cachedValue;
                    return true;
                }
                catch
                {
                    value = default(U);
                    return false;
                }
            }
        }

        private U FetchAndCache<U>(string key, Func<U> retrieveData,
            DateTime? absoluteExpiry, TimeSpan? relativeExpiry,
            IEnumerable<ICacheDependency> cacheDependencies)
        {
            U value;
            if(!TryGetValue<U>(key,out value))
            {
                value = retrieveData();
                if (!absoluteExpiry.HasValue)
                    absoluteExpiry = Cache.NoAbsoluteExpiration;

                if (!relativeExpiry.HasValue)
                    relativeExpiry = Cache.NoSlidingExpiration;

                CacheDependency aspNetCacheDependencies = null;

                if (cacheDependencies != null)
                {
                    if (cacheDependencies.Count() == 1)
                        // We know that the implementation of ICacheDependency
                        // will also implement IAspNetCacheDependency
                        // so we can use a cast here and call the
                        // CreateAspNetCacheDependency() method
                        aspNetCacheDependencies = ((IAspNetCacheDependency)cacheDependencies.
                            ElementAt(0)).CreateAspNetCacheDependency();
                    else if (cacheDependencies.Count() > 1)
                    {
                        AggregateCacheDependency aggregateCacheDependency =
                            new AggregateCacheDependency();
                        foreach (ICacheDependency cacheDependency in cacheDependencies)
                        {
                            // We know that the implementations of ICacheDependency
                            // will also implement IAspNetCacheDependency
                            // so we can use a cast here and call the
                            // CreateAspNetCacheDependency() method
                            aggregateCacheDependency.Add(((IAspNetCacheDependency)cacheDependency)
                                .CreateAspNetCacheDependency());
                        }
                        aspNetCacheDependencies = aggregateCacheDependency;
                    }
                }

                HttpContext.Current.Cache.Insert(key, value,
                    aspNetCacheDependencies,
                    absoluteExpiry.Value, relativeExpiry.Value);
            }
            return value;
        }

        #endregion

    }
}