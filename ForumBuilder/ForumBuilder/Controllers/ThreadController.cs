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
    class ThreadController
    {
        private Thread _thread;

        public Boolean createFirstPost(IPost newPost)
        {
            return true;
        }
        // should delete first post too
        public Boolean deleteThread(IThread toDelete)
        {
            return true;
        }
    }
}
