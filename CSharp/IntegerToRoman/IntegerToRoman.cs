using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.IntegerToRoman
{
    public class RomanNumerals
    {
        private class Denomination
        {
            public int Value { get; set; }
            public string Symbol { get; set; }
        }

        private static readonly Denomination[] table = 
        {
            new Denomination { Value = 1000, Symbol = "M" },
            new Denomination { Value = 900, Symbol = "CM" },
            new Denomination { Value = 500, Symbol = "D" },
            new Denomination { Value = 400, Symbol = "CD" },
            new Denomination { Value = 100, Symbol = "C" },
            new Denomination { Value = 90, Symbol = "XC" },
            new Denomination { Value = 50, Symbol = "L" },
            new Denomination { Value = 40, Symbol = "XL" },
            new Denomination { Value = 10, Symbol = "X" },
            new Denomination { Value = 9, Symbol = "IX" },
            new Denomination { Value = 5, Symbol = "V" },
            new Denomination { Value = 4, Symbol = "IV" },
            new Denomination { Value = 1, Symbol = "I" }
        };

        public static string Translate(int number)
        {
            var result = new StringBuilder();

            int remainder = number;

            //For single-dimensional arrays travel starting with index 0 and ending with index Length - 1
            foreach (var current in table)
            {
                while (remainder >= current.Value)
                {
                    result.Append(current.Symbol);
                    remainder -= current.Value;
                }
            }

            return result.ToString();
        }
    }

}
