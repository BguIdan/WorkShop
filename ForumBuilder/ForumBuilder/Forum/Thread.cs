using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    public class Thread : IThread
    {
        public Thread()
        {

        }
        public Boolean addPost(IPost newPost)
        {
            return true;
        }
        public Boolean deleteThread(IThread toDelete)
        {
            return true;
        }
    }
}
