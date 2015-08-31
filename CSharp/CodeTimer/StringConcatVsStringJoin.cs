using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CSharp.CodeTimer
{
    // Ref: http://www.alexjamesbrown.com/blog/development/c-string-concat-vs-string-join/
    class StringConcatVsStringJoin
    {
        const int numberOfTimesToRun = 1000000; //string.concat or string.join a million times to get a better reading

        const string space = " ";
        const string the = "the";
        const string quick = "quick";
        const string brown = "brown";
        const string fox = "fox";
        const string jumps = "jumps";
        const string over = "over";
        const string lazy = "lazy";
        const string dog = "dog";

        private static void Go()
        {
            var concatStopWatch = Stopwatch.StartNew();

            for (var i = 0; i < numberOfTimesToRun; i++)
                string.Concat(the, space, quick, space, brown, space, fox, space, jumps, space, over, space, the, space, lazy, space, dog);

            concatStopWatch.Stop();

            var joinStopWatch = Stopwatch.StartNew();
            for (var i = 0; i < numberOfTimesToRun; i++)
                string.Join(space, the, quick, brown, fox, jumps, over, the, lazy, dog);

            joinStopWatch.Stop();

            var stringbuilderStopWatch = Stopwatch.StartNew();
            for (var i = 0; i < numberOfTimesToRun; i++)
                new StringBuilder().Append(space).Append(the).Append(quick).Append(brown).Append(fox).Append(jumps).Append(over).Append(the).Append(lazy).Append(dog);

            stringbuilderStopWatch.Stop();


            Console.WriteLine("string.join - {0}", joinStopWatch.ElapsedMilliseconds);
            Console.WriteLine("string.concat- {0}", concatStopWatch.ElapsedMilliseconds);
            Console.WriteLine("StringBuilder.Append- {0}", stringbuilderStopWatch.ElapsedMilliseconds);
        }
    }
}