using System;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    class ThreadController : IThreadController
    {
        private static ThreadController singleton;
        DemoDB demoDB = DemoDB.getInstance;

        public static ThreadController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ThreadController();
                }
                return singleton;
            }

        }
        public Boolean addFirstPost(Post newPost, String forum, String subForum)
        {
            Thread thread = new Thread(newPost);
            SubForumController subForumController;
            if (!demoDB.addThread(thread))
                return false;
            return subForumController.createThread(thread, forum, subForum);
        }

        // should delete first post too
        public Boolean deleteThread(Int32 firstPostToDelete, String deleteUser)
        {
            return true;
        }
    }
}
