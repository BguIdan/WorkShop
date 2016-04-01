using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.User;

namespace ForumBuilder.Forum
{
    public class SubForum : ISubForum
    {
        public SubForum()
        {

        }
        public Boolean dismissModerator(IUser dismissedModerator)
        {
            return true;
        }

        public Boolean createThread(String headLine, String Content)
        {
            return true;
        }
        public Boolean nominateModerator(IUser newModerator)
        {
            return true;
        }
    }
}
