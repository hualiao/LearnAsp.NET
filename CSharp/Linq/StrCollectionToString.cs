using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Linq
{
    /// <summary>
    /// Ref:http://stackoverflow.com/questions/799446/creating-a-comma-separated-list-from-iliststring-or-ienumerablestring
    /// </summary>
    class StrCollectionToString
    {
        public static void StrCollectionToSingleString()
        {
            // IEnumerable<string> can be converted into a string array very easily with LINQ (.NET 3.5)
            IEnumerable<string> strings = new List<string> { "Liao", "Hua" };
            string[] array = strings.ToArray();
            // .NET 2.0 or 3.5
            string[] ary = Helper.ToArray<string>(strings);
            // C# 3 and .NET 3.5 way:
            string joined = string.Join(",", array.ToArray());
            Console.WriteLine(".NET 3.5:");
            Console.WriteLine("\t" + joined);
            // C# 2 and .NET 2 way:
            joined = string.Join(",", new List<string>(strings).ToArray());
            Console.WriteLine(".NET 2");
            Console.WriteLine("\t" + joined);
            // .NET 4.0
            joined = string.Join(",", strings);
            Console.WriteLine(".NET 4 IEnumerable<string>");
            Console.WriteLine("\t" + joined);
            // String.Join overload
            IEnumerable<Person> persons = new List<Person>() { 
                new Person { FirstName = "Liao", LastName = "Hua" }, 
                new Person { FirstName = "F", LastName = "L" } 
            };
            joined = String.Join(",", persons);
            Console.WriteLine(".NET 4 IEnumerable<T>");
            Console.WriteLine("\t" + joined);

            joined = strings.Aggregate((a, x) => a + "," + x);
            Console.WriteLine(".NET 3.5 Aggregate");
            Console.WriteLine("\t" + joined);
        }

    }

    class Helper
    {
        public static T[] ToArray<T>(IEnumerable<T> source)
        {
            return new List<T>(source).ToArray();
        }
    }
}
