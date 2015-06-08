using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using CSharp.CodeTimer;
using CSharp.IntegerToRoman;
using CSharp.NullCheck;

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

            Person person = Person.Generate();
            string passportNumber = person.IfNotNull(x => x.Passport).IfNotNull(x => x.Number);
            Console.WriteLine(passportNumber);
            passportNumber = person.IFNotDefault(x => x.Passport).IFNotDefault(x => x.Number);
            Console.WriteLine(passportNumber);
            passportNumber = person.IFNotDefault(x => x.Passport).IFNotDefault(x => x.Number, x => x.Number != "default number!");
            Console.WriteLine(passportNumber);

            passportNumber = true /*TrueCondition*/
            ? ComputeSomething()
            : null;
            Console.WriteLine(passportNumber);
            passportNumber = true.IFNotDefault(_ => ComputeSomething());
            Console.WriteLine(passportNumber);

            Console.ReadKey();
        }

        public static string ComputeSomething() { return "Test"; }

    }
}
