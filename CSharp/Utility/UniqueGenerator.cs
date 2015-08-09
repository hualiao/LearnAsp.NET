using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Utility
{
    /// <summary>
    /// Ref: http://madskristensen.net/post/generate-unique-strings-and-numbers-in-c
    /// </summary>
    public class UniqueGenerator
    {
        static Random random = new Random();
        /// <summary>
        /// Ref: http://stackoverflow.com/questions/7513391/newguid-vs-system-guid-newguid-tostringd
        /// </summary>
        /// <returns></returns>
        public static string GUID()
        {
            Guid guid = Guid.NewGuid();
            // "N" - xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx (32 digits)
            // "D" - xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx (32 digits separated by hyphens)
            // "B" - {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx} (same as "D" with addition of braces)
            // "P" - (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx) (same as "D" with addition of parentheses)
            // "X" - {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
            return guid.ToString();
            //return guid.ToString("N");
            //return guid.ToString("D");
        }

        /// <summary>
        /// not realy unique
        /// </summary>
        /// <returns></returns>
        public static string UniqueNum()
        {
            int rnd = random.Next(1, 1000000);
            return rnd.ToString();
        }

        private string GenerateId()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1); // plus 1 remove 0 value
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks); // Ticks return long, 1 Ticks = 100 nanosecond 
        }

        private long GenerateId()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
