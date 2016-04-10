using System;
using ForumBuilder.BL_DB;


namespace ForumBuilder.Controllers
{
    class ThreadController : IThreadController
    {
        private static ThreadController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        SubForumController subForumController = SubForumController.getInstance;


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
            if (!demoDB.addThread(thread))
                return false;
            return subForumController.createThread(thread, forum, subForum);
        }

        // delete post from thread only and delete thread from subforum
        public Boolean deleteThread(Int32 firstPostToDelete)
        {
            if(demoDB.getThreadByFirstPostId(firstPostToDelete)!=null)
                return demoDB.removeThreadByfirstPostId(firstPostToDelete) && subForumController.deleteThread(firstPostToDelete);
            return false;
        }
    }
}
