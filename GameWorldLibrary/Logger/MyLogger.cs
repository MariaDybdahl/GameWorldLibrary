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

        /// <summary>
        /// Returnerer en instans af MyLogger-klassen. 
        /// Bruger Singleton-mønsteret med låsning for at sikre trådsikker adgang.
        /// </summary>
        /// <returns>Den fælles instans af MyLogger.</returns>
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

        /// <summary>
        /// En funktion (delegate), der bestemmer hvordan logbeskeder formateres, 
        /// baseret på beskedens indhold og logniveau.
        /// Som standard formateres beskeder som "Level: Message".
        /// Denne formatter bruges internt i loggeren, og kan overskrives via <see cref="SetFormatter"/>.
        /// </summary>
        private Func<string, TraceEventType, string> _formatter = (msg, level) => $"{level}: {msg}";

        /// <summary>
        /// Sætter en brugerdefineret formatteringsfunktion til logbeskeder.
        /// Formatteren bestemmer hvordan logbeskeder vises, baseret på beskedens indhold og logniveau.
        /// </summary>
        /// <param name="formatter">
        /// En funktion der modtager en logbesked og dens <see cref="TraceEventType"/> og returnerer en formatteret streng.
        /// Eksempel: (msg, level) => $"[{level}] {msg}"
        /// </param>
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

        /// <summary>
        /// Logger en informationsbesked til alle registrerede TraceListeners.
        /// </summary>
        /// <param name="message">Beskeden der skal logges som information.</param>
        public void LogInfo(string message)
        {
                
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Information));    
                listener.Flush();
            }
        }

        /// <summary>
        /// Logger en advarselsbesked til alle registrerede TraceListeners.
        /// </summary>
        /// <param name="message">Beskeden der skal logges som advarsel.</param>
        public void LogWarning(string message)
        {
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Warning));
                listener.Flush();
            }
        }

        /// <summary>
        /// Logger en fejlbesked til alle registrerede TraceListeners.
        /// </summary>
        /// <param name="message">Beskeden der skal logges som fejl.</param>
        public void LogError(string message)
        {
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Error));
                listener.Flush();
            }
        }

        /// <summary>
        /// Logger en kritisk fejlbesked til alle registrerede TraceListeners.
        /// </summary>
        /// <param name="message">Beskeden der skal logges som kritisk fejl.</param>
        public void LogCritical(string message)
        {
            foreach (TraceListener listener in TraceManager.Listeners)
            {
                listener.WriteLine(_formatter(message, TraceEventType.Critical));
                listener.Flush();
            }
        }

        /// <summary>
        /// Tilføjer en TraceListener til loggeren, hvis den ikke er null.
        /// </summary>
        /// <param name="listener">Den listener der skal tilføjes.</param>
        public void AddListener(TraceListener listener)
        {
            if (listener != null)
            {
                TraceManager.Listeners.Add(listener);
            }
        }

        /// <summary>
        /// Fjerner en TraceListener fra loggeren, hvis den ikke er null.
        /// </summary>
        /// <param name="listener">Den listener der skal fjernes.</param>
        public void RemoveListener(TraceListener listener)
        {
            if (listener != null)
            {
                TraceManager.Listeners.Remove(listener);
            }
        }

    }
}


