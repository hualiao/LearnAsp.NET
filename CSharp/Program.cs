using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using CSharp.CodeTimer;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            CodeTimer.CodeTimer.Time("Thread Sleep", 1, () => { Thread.Sleep(3000); });
            CodeTimer.CodeTimer.Time("Empty Method", 10000000, () => { });

            Console.ReadKey();
        }
    }
}
