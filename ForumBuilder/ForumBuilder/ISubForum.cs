using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Controllers;

namespace ForumBuilder
{
    interface ISubForum
    {
        Boolean dismissModerator(IModerderator dismissedModerator);
        Boolean createThread(String headLine, String Content);
    }
}
