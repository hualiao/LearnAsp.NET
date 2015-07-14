using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Utility
{

    /// <summary>
    /// Ref:http://geekswithblogs.net/BlackRabbitCoder/archive/2015/07/07/little-puzzlersndashthe-majority-element.aspx
    /// </summary>
    public class MajorityElement
    {
        static Random _rnd = new Random();

        public static Tuple<bool, int, int> HasMajorityElement(int[] array)
        {
            return array
            .GroupBy(item => item)
            .OrderByDescending(g => g.Count())
            .Select(g =>
            new Tuple<bool, int, int>(
            (((double)g.Count() / (double)array.Length) > .5),
            g.Key,
            g.Count()))
            .First();
        }

        public static void MajorityByForEach()
        {
            var l = new List<int> { 1, 2, 1, 4, 2, 1, 1, 5, 1, 1 };
            var d = new Dictionary<int, int>();

            l.ForEach(x =>
            {
                if (d.ContainsKey(x))
                {
                    d[x]++;
                    if (d[x] > l.Count() / 2)
                    {
                        Console.WriteLine(x + d[x]);
                    }
                    return;
                }
                d.Add(x, 1);
            });
        }

        public static int Majority(ICollection<int> seq)
        {
            var seqLength = seq.Count;

            var m = seq
            .GroupBy(i => i)
            .Select(g => new
            {
                num = g.Key,
                freq = g.Count()
            })
            .FirstOrDefault(g => seqLength / g.freq < 2);

            return m != null ? m.num : -1;
        }

        static void RandomizeArray(int[] a)
        {

            for (int i = a.Length - 1; i > 0; i--)
            {
                int pos = _rnd.Next(i + 1);
                int x = a[pos];
                a[pos] = a[i];
                a[i] = x;
            }

        }

        static int[] GenerateArray(int n)
        {

            int value = 1;
            int pos = 0;
            int[] a = new int[n];

            while (pos < n)
            {
                int count = _rnd.Next(n - pos) + 1;
                while (count-- > 0)
                    a[pos++] = value;
                value++;
            }

            RandomizeArray(a);

            return a;

        }
        static void PrintArray(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
                Console.Write("{0,3}", a[i]);
            Console.WriteLine();
        }

        static int GetCountForValue(int[] a, int x)
        {

            int count = 0;

            for (int i = 0; i < a.Length; i++)
                if (a[i] == x)
                    count++;
                else
                    count--;

            return count;

        }


        static int FindMajorityElement(int[] a)
        {

            int count = 1;
            int candidate = a[0];

            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] == candidate)
                {
                    count++;
                }
                else if (count == 0)
                {
                    candidate = a[i];
                    count = 1;
                }
                else
                {
                    count--;
                }
            }

            if (count > 0)
                count = GetCountForValue(a, candidate);

            if (count > 0)
                return candidate;

            return -1;

        }

        /// <summary>
        /// Ref: http://www.codinghelmet.com/?path=exercises/majority-element
        /// </summary>
        /// <param name="args"></param>
        public static void MajorityByOnO1(string[] args)
        {

            while (true)
            {

                Console.Write("n=");
                int n = int.Parse(Console.ReadLine());

                if (n <= 0)
                    break;

                int[] a = GenerateArray(n);

                PrintArray(a);

                int majority = FindMajorityElement(a);

                if (majority < 0)
                    Console.WriteLine("No majority element.");
                else
                {
                    int count = 0;
                    for (int i = 0; i < n; i++)
                        if (a[i] == majority)
                            count++;
                    Console.WriteLine("Majority number is {0} (occurring {1} times).", majority, count);
                }
            }

            Console.Write("Press ENTER to continue... ");
            Console.ReadLine();

        }
    }
}
