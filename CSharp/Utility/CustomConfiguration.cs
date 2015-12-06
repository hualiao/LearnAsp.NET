//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Configuration;

//namespace CSharp.Utility
//{
//    /// <summary>
//    /// Ref:http://stackoverflow.com/questions/1189364/reading-settings-from-app-config-or-web-config-in-net
//    ///     https://bardevblog.wordpress.com/2013/11/17/kickstart-c-custom-configuration/
//    /// </summary>
//    class CustomConfiguration
//    {
//        public static void Test()
//        {
//            string configvalue1 = ConfigurationManager.AppSettings["countoffiles"];
//            string configvalue2 = ConfigurationManager.AppSettings["logfilelocation"];


//            string devUrl = string.Empty;
//            var connectionManagerDatabaseServers = ConfigurationManager.GetSection("ConnectionManagerDatabaseServers") as NameValueCollection;
//            if (connectionManagerDatabaseServers != null)
//            {
//                devUrl = connectionManagerDatabaseServers["Dev"];
//                Console.WriteLine("Dev:" + devUrl);
//                foreach (var serverKey in connectionManagerDatabaseServers.AllKeys)
//                {
//                    string serverValue = connectionManagerDatabaseServers.GetValues(serverKey).FirstOrDefault();
//                    Console.WriteLine("ServerValue:" + serverValue);
//                }
//            }
//        }
//    }
//}
