using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.User;

namespace ForumBuilder.Forums
{
    interface ISubForum
    {
        Boolean dismissModerator(IUser dismissedModerator);
        Boolean createThread(String headLine, String Content);
        Boolean nominateModerator(IUser newModerator);
    }
}
