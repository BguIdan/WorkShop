using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using BL_Back_End;



namespace ForumBuilder.Common.ServiceContracts
{
    [ServiceContract]
    public interface IPostManager
    {
        [OperationContract]
        Boolean deletePost(Int32 postId, String deletingUser);

        [OperationContract]
        Boolean addPost(String headLine, String content, String writerName, Int32 commentedPost);
        
        [OperationContract]
        List<Post> getAllPosts(String forumName, String subforumName);

    }
}
