using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.NullCheck
{
    /// <summary>
    /// Detail:http://geekswithblogs.net/nabuk/archive/2014/03/26/get-rid-of-deep-null-checks.aspx
    /// </summary>
    static class NullCheckExtention
    {
        public static TResult IfNotNull<TResult, TSource>(this TSource source,
            Func<TSource, TResult> onNotDefault)
            where TSource : class
        {
            if (onNotDefault == null) throw new ArgumentNullException("onNotDefault");

            return source == null ? default(TResult) : onNotDefault(source);
        }

        public static TResult IFNotDefault<TResult, TSource>(this TSource source,
            Func<TSource, TResult> onNotDefault,
            Predicate<TSource> isNotDefault = null)
        {
            if (onNotDefault == null) throw new ArgumentNullException("onNotDefault");

            var isDefault = isNotDefault == null
                ? EqualityComparer<TSource>.Default.Equals(source)
                : !isNotDefault(source);

            return isDefault ? default(TResult) : onNotDefault(source);
        }
    }
}
