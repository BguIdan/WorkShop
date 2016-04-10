﻿using System;
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
        private StreamWriter _sw;

        private Logger(String fullPath)
        {
            _sw = new StreamWriter(fullPath);
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
            _sw.WriteLine("Log Entry : " , DateTime.Now.ToLongTimeString());
            _sw.WriteLine( contentToPrint);
            _sw.WriteLine("-----------------------------------------------");
        }
    }
}