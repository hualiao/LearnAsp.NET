using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApp.Helpers
{
    /// <summary>
    /// A simple Logger which writes to Windows Event Log
    /// </summary>
    public class Logger
    {
        static EventLog log;

        static Logger()
        {
            log = new EventLog("Application", Environment.MachineName, "Image Grid");
        }

        /// <summary>
        /// Logs the message into Window Event Log.
        /// </summary>
        /// <param name="message"></param>
        public static void Log(string message)
        {
            log.WriteEntry(message, EventLogEntryType.Warning, 21001);
        }
    }
}