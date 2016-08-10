using System;

namespace MovieMover
{
    public class Logger
    {
        public static void LogInfo(string format, params object[] args)
        {
            var message = String.Format(format, args);
            Console.WriteLine("{0:dd/MM/yyyy HH:mm:ss}: {1}", DateTime.Now, message);
        }
    }
}
