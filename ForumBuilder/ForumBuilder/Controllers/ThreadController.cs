using System;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Controllers
{
    class ThreadController : IThreadController
    {
        private Thread _thread;

        public Boolean addFirstPost(string headLine, string Content, string writerName, DateTime timePublished)
        {
            return true;
        }

        // should delete first post too
        public Boolean deleteThread(Int32 firstPostToDelete, String deleteUser)
        {
            return true;
        }
    }
}
