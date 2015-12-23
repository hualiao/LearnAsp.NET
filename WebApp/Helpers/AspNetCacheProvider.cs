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

        public IEnumerable<T> Fetch(string key, Func<IEnumerable<T>> retrieveData,
                DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            return FetchAndCache<IEnumerable<T>>(key, retrieveData,
                    absoluteExpiry, relativeExpiry);
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

        #endregion

    }
}