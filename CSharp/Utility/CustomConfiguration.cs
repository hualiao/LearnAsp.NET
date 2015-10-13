using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CSharp.Utility
{
    /// <summary>
    /// Ref:http://stackoverflow.com/questions/1189364/reading-settings-from-app-config-or-web-config-in-net
    /// </summary>
    class CustomConfiguration
    {
        public static void Test()
        {
            string configvalue1 = ConfigurationManager.AppSettings["countoffiles"];
            string configvalue2 = ConfigurationManager.AppSettings["logfilelocation"];
        }
    }
}
