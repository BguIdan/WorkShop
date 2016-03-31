using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.User;

namespace ForumBuilder.Forum
{
    class SubForum : ISubForum
    {
        public SubForum()
        {

        }
        Boolean dismissModerator(IUser dismissedModerator)
        {
            return true;
        }
            
        Boolean createThread(String headLine, String Content)
        {
            return true;
        }
        Boolean nominateModerator(IUser newModerator)
        {
            return true;
        }
    }
}
