using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    internal class Randomizer
    {
        private const string PoolUpperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNM"; // length = 26
        private const string PoolLowerCaseLetters = "qwertyuiopasdfghjklzxcvbnm"; // length = 26

        private const string PoolPunctuation = "                    ,,,,...!?--:;";
        // be asare of the JSON special sympols

        public static Random Rand = new Random();

        public static string Name
        {
            get { return GetCapitalChar() + Word; }
        }

        public static string Word
        {
            get
            {
                var wordLength = GetWordLength();
                var sb = new StringBuilder(wordLength);
                for (var i = 0; i < wordLength; i++)
                    sb.Append(GetChar());
                return sb.ToString();
            }
        }

        public static string Id
        {
            get
            {
                var wordLength = GetIdLength();
                var sb = new StringBuilder(wordLength);
                for (var i = 0; i < wordLength; i++)
                    sb.Append(GetDigit());
                return sb.ToString();
            }
        }

        public static string Phrase
        {
            get
            {
                var phraseLength = GetPhraseLength();
                var sb = new StringBuilder(phraseLength);
                sb.Append(Name);
                for (var i = 0; i < phraseLength; i++)
                    sb.Append(Word + GetPunctuation());
                return sb.ToString();
            }
        }

        public static DateTime GetDate(DateTime startDT, DateTime stopDT)
        {
            return startDT.AddDays(Rand.Next((stopDT - startDT).Days));
        }

        private static char GetCapitalChar()
        {
            return PoolUpperCaseLetters[Rand.Next(PoolUpperCaseLetters.Length)];
        }

        private static char GetChar()
        {
            return PoolLowerCaseLetters[Rand.Next(PoolLowerCaseLetters.Length)];
        }

        private static char GetPunctuation()
        {
            return PoolPunctuation[Rand.Next(PoolPunctuation.Length)];
        }

        private static char GetDigit()
        {
            return Rand.Next(0, 9).ToString()[0];
        }

        private static int GetWordLength()
        {
            return Rand.Next(1, 29);
        }

        private static int GetIdLength()
        {
            return Rand.Next(1, 10);
        }

        private static int GetPhraseLength()
        {
            return Rand.Next(1, 20);
        }
    }
}
