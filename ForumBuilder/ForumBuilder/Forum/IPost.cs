using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    interface IPost
    {
        Boolean deletePost(IPost toDelete);
    }
}
