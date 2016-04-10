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

        public bool dismissModerator(string dismissedModerator, string dismissByAdmin, string subForumName, string forumName)
        {
            SubForum subFourm = ForumController.getInstance.getSubForum(subForumName, forumName);
            if (ForumController.getInstance.isAdmin(dismissByAdmin, forumName) && ForumController.getInstance.isMember(dismissedModerator, forumName))
            {
                return demoDB.dismissModerator(dismissedModerator, dismissByAdmin, subForum);
            }
            return false;
        }

        public bool nominateModerator(string newModerator, string nominatorUser, DateTime date, string subForumName, string forumName)
        {
            SubForum subFourm = ForumController.getInstance.getSubForum(subForumName, forumName);
            if (ForumController.getInstance.isAdmin(nominatorUser, forumName) && ForumController.getInstance.isMember(newModerator, forumName))
            {
                return demoDB.nominateModerator(newModerator, nominatorUser, date, subForum);
            }
            return false;
        }
    }
}
