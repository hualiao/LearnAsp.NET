using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Utility
{
    // Ref:http://jeremycomer.com/blog/?p=74
    class SortDictionary
    {
        private static Dictionary<int, string> m_dictionary = new Dictionary<int, string>();

        static SortDictionary()
        {
            m_dictionary.Add(4, "Four");
            m_dictionary.Add(2, "Bob");
            m_dictionary.Add(1, "Ron");
            m_dictionary.Add(3, "house");
        }

        /// <summary>
        /// LINQ query to sort by the keys
        /// </summary>
        public void SortByLinq()
        {
            var sortedDictionary1 = from key in m_dictionary.Keys
                                    orderby key ascending
                                    select key;
            foreach (int key in sortedDictionary1)
            {
                Console.WriteLine(key.ToString() + " " + m_dictionary[key]);
            }
            Console.WriteLine("");

            // LINQ query to sort by values
            var sortedDictionary2 = from key in m_dictionary.Keys
                                    orderby m_dictionary[key] ascending
                                    select key;
            foreach (int key in sortedDictionary2)
            {
                Console.WriteLine(key.ToString() + " " + m_dictionary[key]);
            }

            foreach (KeyValuePair<int, string> data in m_dictionary.OrderBy(key => key.Key))
            {
                Console.WriteLine("Key: {0}, Value: {1}", data.Key, data.Value);
            }

            foreach (KeyValuePair<int, string> data in m_dictionary.OrderBy(key => key.Value))
            {
                Console.WriteLine("Key: {0}, Value: {1}", data.Key, data.Value);
            }
        }

        public void SortByList()
        {
            List<int> iKeys = new List<int>(m_dictionary.Keys);
            iKeys.Sort();
            foreach (int key in iKeys)
            {
                Console.WriteLine(key.ToString() + " " + m_dictionary[key]);
            }
            Console.WriteLine("");
            // sort the value in the list
            List<string> sKeys = new List<string>(m_dictionary.Values);
            sKeys.Sort();
            foreach (string key in sKeys)
            {
                Console.WriteLine(key);
            }
        }

        public void SortByKeyValuePairList()
        {
            List<KeyValuePair<int, string>> m_list = new List<KeyValuePair<int, string>>(m_dictionary);

            // Sort the keys
            m_list.Sort((lhs, rhs) =>
            {
                return Comparer<int>.Default.Compare(lhs.Key, rhs.Key);
            });

            //m_list.Sort(delegate(KeyValuePair<int, string> lhs,
            //KeyValuePair<int, string> rhs)
            //{
            //    return Comparer<int>.Default.Compare(lhs.Key, rhs.Key);
            //});

            foreach (KeyValuePair<int, string> data in m_list)
            {
                Console.WriteLine(data.Key.ToString() + " " + data.Value);
            }
            Console.WriteLine("");

            // Sort the Values
            m_list.Sort((lhs, rhs) =>
            {
                return Comparer<string>.Default.Compare(lhs.Value, rhs.Value);
            });

            foreach (KeyValuePair<int, string> data in m_list)
            {
                Console.WriteLine(data.Key.ToString() + " " + data.Value);
            }
        }
    }
}
