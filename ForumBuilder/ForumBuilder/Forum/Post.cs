using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    public class Post : IPost
    {
        public Post(){

        }
        public Boolean deletePost(IPost toDelete)
        {
            return true;
        }
    }
}
