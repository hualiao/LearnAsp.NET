using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CSharp.Algorithm
{
    // Ref: http://geekswithblogs.net/BlackRabbitCoder/archive/2015/08/04/solution-to-little-puzzlersndashldquolist-all-anagrams-in-a-wordrdquo.aspx

    public class WordFileDao : IWordDao
    {
        public string FileName { get; private set; }

        public WordFileDao(string fileName)
        {
            FileName = fileName;
        }

        public IEnumerable<string> RetrieveWords()
        {
            // you can start enumerating the collection stirngs
            // make sure to remove any accidental space and normalize to lower case
            return File.ReadLines(FileName).Select(l => l.Trim().ToLower());
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
            // at lookup time, sort the input word as key,
            // and return all words (minus the input word) in the sequence
            string input = word.ToLower();
            string key = string.Concat(input.OrderBy(c => c));

            return _anagramLookup[key].Where(w => w != input).ToList();
        }
    }

    interface IWordDao 
    {
        IEnumerable<string> RetrieveWords();
    }
}
