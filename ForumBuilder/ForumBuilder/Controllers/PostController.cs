using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;
using ForumBuilder.Services;

namespace ForumBuilder.Controllers
{
    class PostController
    {
        private Post _post;
        
        public Boolean deletePost()
        {
            List<Post> donePosts = new List<Post>();
            List<Post> undonePosts = new List<Post>();
            undonePosts.Add(this);
            while (undonePosts.Count != 0)
            {
                Post p = undonePosts.ElementAt(0);
                undonePosts.RemoveAt(0);
                List<Post> related = Services.Service.getInstance.getRelatedPosts(p._id);
                while (related != null && related.Count != 0)
                {
                    undonePosts.Add(related.ElementAt(0));
                    related.RemoveAt(0);
                }
                donePosts.Add(p);
            }
            return true;
        }

        public Boolean addPost(IPost newPost)
        {
            return true;
        }
    }
}
