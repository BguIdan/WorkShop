using System;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    class PostController :IPostController
    {
        
        public Boolean deletePost(Int32 postId, String deletingUser)
        {
            List<int> donePosts = new List<int>();
            List<int> undonePosts = new List<int>();
            undonePosts.Add(postId);
            while (undonePosts.Count != 0)
            {
                Post p = undonePosts.ElementAt(0);
                undonePosts.RemoveAt(0);
                List<Post> related = DemoDB.getInstance.getRelatedPosts(p._id);
                while (related != null && related.Count != 0)
                {
                    undonePosts.Add(related.ElementAt(0));
                    related.RemoveAt(0);
                }
                donePosts.Add(p);
            }
            return true;
        }

        public Boolean addPost(String headLine, String Content, String writerName, DateTime timePublished, Int32 commentedPost/*if new thread, -1*/)
        {
            return true;
        }
    }
}
