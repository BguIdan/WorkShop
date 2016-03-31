using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    class Thread : IThread
    {
        public Thread()
        {

        }
        Boolean addPost(IPost newPost)
        {
            return true;
        }
        Boolean deleteThread(IThread toDelete)
        {
            return true;
        }
    }
}
