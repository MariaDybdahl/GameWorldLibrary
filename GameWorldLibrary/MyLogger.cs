using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary
{
    public class MyLogger
    {


        private static MyLogger instance = null;
        private static object _lock = new object();
       
        private readonly TraceSource? tc = null;
        private readonly string logName = "GameLogger";

        public static MyLogger GetInstance()
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new MyLogger();
                }
            }
            return instance;
        }

        private MyLogger()
        {
            tc = new TraceSource(logName, SourceLevels.All);
           

#if DEBUG
            tc.Listeners.Add(new ConsoleTraceListener());
#endif
            
        }

        public void LogInfo(string message)
        {
            tc.TraceEvent(TraceEventType.Information, 0, message);
            tc.Flush();
        }


        public void LogWarning(string message)
        {
            tc.TraceEvent(TraceEventType.Warning, 0, message);
            tc.Flush();
        }


        public void LogError(string message)
        {
            tc.TraceEvent(TraceEventType.Error, 0, message);
            tc.Flush();
        }


        public void LogCritical(string message)
        {
            tc.TraceEvent(TraceEventType.Critical, 0, message);
            tc.Flush();
        }
        public void AddListener(TraceListener listener)
        {
            if (listener != null)
            {
                tc.Listeners.Add(listener);
            }
        }
        public void RemoveListener(TraceListener listener)
        {
            if (listener != null)
            {
                tc.Listeners.Remove(listener);
            }
        }

    }
}


