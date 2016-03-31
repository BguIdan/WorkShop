using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.System
{
    interface ILogger
    {
        Boolean addLogMessage(int code, String content);
    }
}
