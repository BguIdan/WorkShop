using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    class Post : IPost
    {
        public Post(){

        }
        Boolean deletePost(IPost toDelete) {
            return true;
        }
    }
}
