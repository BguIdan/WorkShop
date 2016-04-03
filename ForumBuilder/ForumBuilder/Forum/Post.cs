using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Forum
{
    public class Post : IPost
    {
        private int id;
        private string title;
        private string content;
        private int parentId;


        public Post(int id, string title, string content, int parentId)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.parentId = parentId;
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
                List<Post> related = Service.getRelatedPosts(p.Id);
                while (related!=null && related.Count != 0)
                {
                    undonePosts.Add(related.ElementAt(0));
                    related.RemoveAt(0);
                }
                donePosts.Add(p);
            }
            return true;
        }
        public Int32 _id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Int32 _parentId
        {
            get
            {
                return parentId;
            }

            set
            {
                parentId = value;
            }
        }
    }
}
