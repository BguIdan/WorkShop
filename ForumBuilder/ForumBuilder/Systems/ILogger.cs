﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Systems
{
    public interface ILogger
    {
        void logPrint(String name, String contentToPrint);
    }
}
