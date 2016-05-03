using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    class PostManagerClient : ClientBase<IPostManager>, IPostManager
    {
        Boolean deletePost(Int32 postId, String deletingUser)
        {
            return Channel.deletePost(postId, deletingUser);
        }

        Boolean addPost(String headLine, String content, String writerName, Int32 commentedPost)
        {
            return Channel.addPost(headLine, content, writerName, commentedPost);
        }
    }
}
