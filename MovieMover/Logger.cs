using System;

namespace MovieMover
{
    public class Logger
    {
        public static void LogInfo(string format, params object[] args)
        {
            var message = String.Format(format, args);
            Log(message);
        }

        public static void LogException(Exception exception, string format, params object[] args)
        {
            var message = String.Format(format, args);
            message += "\r\n\tException: " + exception;
            Log(message);
        }

        private static void Log(string message)
        {
            Console.WriteLine("{0:dd/MM/yyyy HH:mm:ss}: {1}", DateTime.Now, message);
        }
    }
}
