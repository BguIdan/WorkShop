using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    interface IPostManager
    {
        Boolean deletePost(Int32 postId, String deletingUser);
        Boolean addPost(String headLine, String content, String writerName, Int32 commentedPost);
    }
}
