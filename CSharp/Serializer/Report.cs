using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CSharp.Serializer
{
    internal static class Report
    {
        public static void AllResult(Dictionary<string,Measurements[]> measurements)
        {
            Header();
            foreach(var oneTestMeasurments in measurements)

        }

        public static void TimeAndDocument(string name, long timeTicks, string document)
        {
            Trace.WriteLine(name + ": " + timeTicks + " ticks Document: " + document);
        }

        public static void Errors(List<string> errors)
        {
            if (errors.Count <= 1) return;
            foreach (var error in errors)
            {
                Trace.WriteLine(error);
            }
        }

        private static void Header()
        {
            const string header = "Serializer:    Time: Avg-90%   -100%    Min    99st%      Max  Size: Avg\n"
                                  +
                                  "=========================================================================";
            Console.WriteLine(header);
            Trace.WriteLine(header);
        }

        private static void SingleResult(KeyValuePair<string, Measurements[]> oneTestMeasurements)
        {
            var report =
                String.Format("{0, -20} {1,7:N0} {2,7:N0} {3,6:N0} {4,9:N0} {5,10:N0} {6,6:N0}",
                oneTestMeasurements.Key,
                );

        }
    }
}
