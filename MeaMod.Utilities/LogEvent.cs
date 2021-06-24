using System;
using System.Diagnostics;

namespace MeaMod.Utilities
{
    public class LogEvent
    {
        public static void Write(string source, string message, bool isConsole = false)
        {
            Write(source, message, "Application", EventLogEntryType.Information, 1000, isConsole);
        }

        public static void Write(string source, string message, string logName, bool isConsole = false)
        {
            Write(source, message, logName, EventLogEntryType.Information, 1000, isConsole);
        }

        public static void Write(string source, string message, string logName, EventLogEntryType eventType, bool isConsole = false)
        {
            Write(source, message, logName, eventType, 1000, isConsole);
        }

        public static void Write(string source, string message, string logName, EventLogEntryType eventType, int eventID, bool isConsole = false)
        {
            if (isConsole == true)
            {
                Console.WriteLine(message);
            }
            else
            {
                var objEventLog = new EventLog();
                try
                {
                    if (!EventLog.SourceExists(source))
                    {
                        EventLog.CreateEventSource(source, logName);
                    }

                    objEventLog.Source = source;
                    objEventLog.WriteEntry(message, eventType, eventID);
                }
                catch (Exception)
                {
                }

                Debug.WriteLine(message);
            }
        }

        // Old Write Console
        [Obsolete("This method is obsolete. Call Write instead.", false)]
        public static void WriteConsole(string source, string message, bool isConsole = false)
        {
            Write(source, message, "Application", EventLogEntryType.Information, 1000, isConsole);
        }

        [Obsolete("This method is obsolete. Call Write instead.", false)]
        public static void WriteConsole(string source, string message, string logName, bool isConsole = false)
        {
            Write(source, message, logName, EventLogEntryType.Information, 1000, isConsole);
        }

        [Obsolete("This method is obsolete. Call Write instead.", false)]
        public static void WriteConsole(string source, string message, string logName, EventLogEntryType eventType, bool isConsole = false)
        {
            Write(source, message, logName, eventType, 1000, isConsole);
        }

        [Obsolete("This method is obsolete. Call Write instead.", false)]
        public static void WriteConsole(string source, string message, string logName, EventLogEntryType eventType, int eventID, bool isConsole = false)
        {
            Write(source, message, logName, eventType, eventID, isConsole);
        }

        public static bool CheckSourceExists(string source, string logName)
        {
            if (EventLog.SourceExists(source))
            {
                var evLog = new EventLog() { Source = source };
                if ((evLog.Log ?? "") != (logName ?? ""))
                {
                    EventLog.DeleteEventSource(source);
                }
            }

            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, logName);
                EventLog.WriteEntry(source, string.Format("Event Log Created '{0}'/'{1}'", logName, source), EventLogEntryType.Information);
            }

            var eventLogs = EventLog.GetEventLogs();
            foreach (var e in eventLogs)
            {
                if ((e.Log ?? "") == (logName ?? ""))
                {
                    if (e.MaximumKilobytes < 1048576L)
                    {
                        e.MaximumKilobytes = 1048576L;
                    }
                }
            }

            return EventLog.SourceExists(source);
        }
    }
}