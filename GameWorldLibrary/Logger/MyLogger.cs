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
        private Func<string, TraceEventType, string> _formatter = (msg, level) => $"{level}: {msg}";


        public void SetFormatter(Func<string, TraceEventType, string> formatter)
        {
            _formatter = formatter;
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
                
                foreach (TraceListener listener in TraceManager.Listeners)
                {
                    listener.WriteLine(_formatter(message, TraceEventType.Information));    
                    listener.Flush();
                }
            }

        


        public void LogWarning(string message)
        {
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Warning));
                listener.Flush();
            }
        }


        public void LogError(string message)
        {
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Error));
                listener.Flush();
            }
        }


        public void LogCritical(string message)
        {
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Critical));
                listener.Flush();
            }
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


