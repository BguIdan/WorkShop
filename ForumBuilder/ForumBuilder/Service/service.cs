using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forum;
using ForumBuilder.Users;


namespace ForumBuilder.Service
{
    class Service
    {
        List<IForum> forums;
        List<ISubForum> subForums;
        List<IThread> threads;
        List<IPost> posts;
        List<IUser> users;

        public Service()
        {
            forums = new List<IForum>();
            subForums = new List<ISubForum>();
            threads = new List<IThread>();
            posts = new List<IPost>();
            users = new List<IUser>();

        }

        Boolean isForumExists(string name)
        {
            for (int i = 0; i < forums.Count; i++)
            {
                if ((forums.ElementAt(i))._forumName == name)
                    return true;
            }
                return false;
        }

        Boolean isSubForumExists(string forumName, string subForumName)
        {
            for (int i = 0; i < subForums.Count; i++)
            {
                if ((subForums.ElementAt(i))._name == subForumName && (subForums.ElementAt(i))._forumName == forumName)
                    return true;
            }
            return false;
        }

        IUser getUser(string userName)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if ((users.ElementAt(i))._userName == userName)
                    return users.ElementAt(i);
            }
            return null;
        }

        List<IPost> getRelatedPosts(int postId)
        {
            List<IPost> curPost = new List<IPost>();
            for (int i = 0; i < posts.Count; i++)
            {
                if ((posts.ElementAt(i))._parentId == postId)
                {
                    curPost.Add(posts.ElementAt(i));
                }
            }
            return curPost;

        }
    }
}
