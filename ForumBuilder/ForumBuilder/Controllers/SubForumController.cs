using System;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    class SubForumController : ISubForumController
    {
        private static SubForumController singleton;

        DemoDB demoDB = DemoDB.getInstance;
        public static SubForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SubForumController();
                }
                return singleton;
            }

        }
        public bool createThread(Thread thread, String forum, String subForum)
        {
            return demoDB.addThreadToSubForum(thread, forum, subForum);
        }
        // delete thread from subforum
        public bool deleteThread(int firstPostId)
        {
            return demoDB.deleteThreadFromSubforum(firstPostId);
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
