using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    public class Post : IPost
    {
        private string _id;
        private string _title;
        private string _content;
        private string _parentId;
        public Post(string id, string title, string content, string parentId)
        {
            _id = id;
            _title = title;
            _content = content;
            _parentId = parentId;
        }
        public Boolean deletePost()
        {
            List<Post> donePosts = new List<Post>();
            List<Post> undonePosts = new List<Post>();
            undonePosts.Add(this);
            while (undonePosts.Count != 0)
            {
                Post p = undonePosts.ElementAt(0);
                undonePosts.RemoveAt(0);
                List<Post> related=Service.getRelatedPosts(p);
                while (related.Count != 0)
                {
                    undonePosts.Add(related.ElementAt(0));
                    related.RemoveAt(0);
                }
                donePosts.Add(p);
            }
            return true;
        }
    }
}
