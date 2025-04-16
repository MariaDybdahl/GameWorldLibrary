using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWorldLibrary.Logger
{
    public class MyLogger
    {

        private static MyLogger instance = null;
        private static object _lock = new object();

        private readonly TraceSource? TraceManager = null;
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

        public MyLogger(string logName)
        {
            this.logName = logName;
        }

        private MyLogger()
        {
            TraceManager = new TraceSource(logName, SourceLevels.All);


#if DEBUG
            TraceManager.Listeners.Add(new ConsoleTraceListener());
#endif

        }

        public void LogInfo(string message)
        {
            TraceManager.TraceEvent(TraceEventType.Information, 0, message);
            TraceManager.Flush();
        }


        public void LogWarning(string message)
        {
            TraceManager.TraceEvent(TraceEventType.Warning, 0, message);
            TraceManager.Flush();
        }


        public void LogError(string message)
        {
            TraceManager.TraceEvent(TraceEventType.Error, 0, message);
            TraceManager.Flush();
        }


        public void LogCritical(string message)
        {
            TraceManager.TraceEvent(TraceEventType.Critical, 0, message);
            TraceManager.Flush();
        }
        public void AddListener(TraceListener listener)
        {
            if (listener != null)
            {
                TraceManager.Listeners.Add(listener);
            }
        }
        public void RemoveListener(TraceListener listener)
        {
            if (listener != null)
            {
                TraceManager.Listeners.Remove(listener);
            }
        }

    }
}


