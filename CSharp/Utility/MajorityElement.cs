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
    }
}
