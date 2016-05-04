using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;
using BL_Back_End;

namespace PL.proxies
{
    public class PostManagerClient : ClientBase<IPostManager>, IPostManager
    {
        public PostManagerClient()
        {

        }

        Boolean deletePost(Int32 postId, String deletingUser)
        {
            return Channel.deletePost(postId, deletingUser);
        }

        Boolean addPost(String headLine, String content, String writerName, Int32 commentedPost)
        {
            return Channel.addPost(headLine, content, writerName, commentedPost);
        }

        List<Post> getAllPosts(String forumName, String subforumName)
        {
            return Channel.getAllPosts(forumName, subforumName);
        }

    }
}
