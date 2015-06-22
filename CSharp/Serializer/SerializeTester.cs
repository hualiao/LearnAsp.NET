using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CSharp.Serializer
{
    internal struct Measurements
    {
        public int Size;
        public long Time;
    }

    internal class SerializeTester
    {
        public static void Tests(int repetitions, Dictionary<string, ISerDeser> serializers)
        {
            var measurements = new Dictionary<string, Measurements[]>();
            foreach (var serializer in serializers)
                measurements[serializer.Key] = new Measurements[repetitions];
            var original = Person.Generate();
            for (int i = 0; i < repetitions; i++)
            {
                foreach (var serializer in serializers)
                {
                    var sw = Stopwatch.StartNew();
                    var serialized = serializer.Value.Serialize<Person>(original);
                    var processed = serializer.Value.Deserialize<Person>(serialized);

                    sw.Stop();
                    measurements[serializer.Key][i].Time = sw.ElapsedTicks;
                    Report.TimeAndDocument(serializer.Key, sw.ElapsedTicks, serialized);
                    var errors = original.Compare(processed);
                    errors[0] = serializer.Key + errors[0];
                }
                GC.Collect();
                GC.WaitForFullGCComplete();
                GC.Collect();
            }
            Report.AllResult(measurements);

        }
    }
}
