using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.GenericExtension
{
    static class GenericExtension
    {
        // The developer want the string in alpahbetical then by lengthwise ascending order
        // So he write show below
        // public static T[] AlphaLengthWise<T>(this T[] names)
        // {
        //     var query = names.OrderBy(a => a.Length).ThenBy(a => a);
        //     return query;
        // }
        // Error 1: T does not contain definition for Length
        // Error 2: can not convert System.Linq.IOrderedEnumerable to T[]
        // Ref: http://stackoverflow.com/questions/1825952/how-to-create-a-generic-extension-method
        public static IEnumerable<T> AlphaLengthWise<T, L>(
            this IEnumerable<T> names, Func<T, L> lengthProvider)
        {
            return names
                .OrderBy(a => lengthProvider(a))
                .ThenBy(a => a);
        }

        /// <summary>
        /// Using the 'Where' Clause with Generics
        /// Ref: http://www.codeproject.com/Articles/29079/Using-Generic-Extension-Methods
        /// </summary>
        public static string DoSerialize<T>(this T entity) where T : Person
        {
            //Serialze the entity object and return its string represenatation
            return String.Empty;
        }
    }
}
