using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;
using ForumBuilder.Services;

namespace ForumBuilder.Controllers
{
    class SubForumController
    {
        private SubForum _subForum;

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

        public Boolean deleteThread(IThread deleteThread)
        {
            throw new NotImplementedException();
        }

    }
}
