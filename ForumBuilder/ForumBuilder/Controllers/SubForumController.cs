using System;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Controllers
{
    class SubForumController : ISubForumController
    {
        private SubForum _subForum;

        public bool createThread(string headLine, string Content, string userName)
        {
            throw new NotImplementedException();
        }

        public bool deleteThread(int firstPostId, string deleteUserName)
        {
            throw new NotImplementedException();
        }

        public bool dismissModerator(string dismissedModerator, string dismissByAdmin)
        {
            throw new NotImplementedException();
        }

        public bool nominateModerator(string newModerator, string nominatorUser)
        {
            throw new NotImplementedException();
        }
    }
}
