using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using CSharp.CodeTimer;
using CSharp.IntegerToRoman;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTimer.CodeTimer.Time("Thread Sleep", 1, () => { Thread.Sleep(3000); });
            CodeTimer.CodeTimer.Time("Empty Method", 10000000, () => { });

            //check result here http://romannumerals.babuo.com/roman-numerals-1-5000
            Console.WriteLine(RomanNumerals.Translate(2015));
            Console.WriteLine(RomanNumerals.Translate(2048));
            Console.WriteLine(RomanNumerals.Translate(2049));
            Console.WriteLine(RomanNumerals.Translate(2050));
            Console.WriteLine(RomanNumerals.Translate(2051));

            Console.ReadKey();
        }
    }
}
