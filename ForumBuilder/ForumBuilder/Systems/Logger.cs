using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ForumBuilder.Systems
{
    class Logger : ILogger
    {
        private static Logger _singleton;
        private static StreamWriter _sw; 

        public static Logger getInstance
        {
            get
            {
                if (_singleton == null)
                {
                    String fullPath = Path.GetFullPath("log.txt");
                    _singleton = new Logger();
                    _sw = new StreamWriter(fullPath);
                }
                return _singleton;
            }

        }

        public void logPrint(String name, String contentToPrint)
        {
            _sw.WriteLine("Log Entry : " , DateTime.Now.ToLongTimeString());
            _sw.WriteLine(name + ": " + contentToPrint);
            _sw.WriteLine("-----------------------------------------------");
        }
    }
}
