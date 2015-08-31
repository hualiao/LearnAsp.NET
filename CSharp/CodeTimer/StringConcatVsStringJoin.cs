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
public class AnagramFinder 
{ 
    private readonly ILookup<string, string> _anagramLookup; 
 
    public AnagramFinder(IWordDao dao) 
    { 
        // construct the lookup at initialization using the
        // dictionary words with the sorted letters as the key
        _anagramLookup = dao.RetrieveWords().ToLookup( 
            k => string.Concat(k.OrderBy(c => c))); 
    }
 
    public IList<string> FindAnagrams(string word) 
    { 
        // at lookup time, sort the input word as the key,
        // and return all words (minus the input word) in the sequence
        string input = word.ToLower(); 
        string key = string.Concat(input.OrderBy(c => c));
 
        return _anagramLookup[key].Where(w => w != input).ToList(); 
    } 
}