using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Utility
{
    class ByteArrayToBase64
    {
        static void Test()
        {
            byte[] bytes = { 2, 4, 8, 16, 32, 64, 128 };
            string base64 = Convert.ToBase64String(bytes);
            bytes = Convert.FromBase64String(base64);
        }
    }
}
