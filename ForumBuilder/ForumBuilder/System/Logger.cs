using System;
using System.IO;

namespace ForumBuilder.Systems
{
    class Logger : ILogger
    {
        private static Logger _singleton;
        private StreamWriter _sw;

        private Logger(String fullPath)
        {
            _sw = File.CreateText(fullPath);//File.AppendText("log.txt");// new StreamWriter(fullPath);
        }

        public static Logger getInstance
        {
            get
            {
                if (_singleton == null)
                {
                    String fullPath = Path.GetFullPath("log.txt");
                    _singleton = new Logger(fullPath); 
                }
                return _singleton;
            }
        }

        public void logPrint(String contentToPrint)
        {
            _sw.WriteLine("Log Entry : \t\t" + DateTime.Now);
            _sw.WriteLine( contentToPrint);
            _sw.WriteLine("-----------------------------------------------");
        }
    }
}
