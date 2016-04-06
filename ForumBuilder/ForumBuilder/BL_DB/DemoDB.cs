using System;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;

namespace ForumBuilder.BL_DB
{
    class DemoDB 
    {
        private List<Forum> forums;
        private List<SubForum> subForums;
        private List<Thread> threads;
        private List<Post> posts;
        private List<User> users;
        private List<Message> messages;
        private static DemoDB singleton;

        private DemoDB()
        {
            forums = new List<Forum>();
            subForums = new List<SubForum>();
            threads = new List<Thread>();
            posts = new List<Post>();
            users = new List<User>();

        }
        public int /* the new post id*/ getAvilableIntOfPost() {
            int max = 0;
            foreach (Post p in posts)
            {
                if (p.id >= max)
                    max=p.id+1;
            }
            return max;
        }
        public Boolean addPost(Post post) {
            foreach(Post p in posts)
            {
                if (p.id == post.id)
                    return false;
            }
            posts.Add(post);
            return true;
        }
        public bool addThreadToSubForum(Thread thread, string forum, string subForum)
        {

            foreach (SubForum sf in subForums)
            {
                if (sf.forum.Equals(forum) && sf.name.Equals(subForums))
                {
                    foreach (int t in sf.threads)
                    {
                        if (t == thread.firstPost.id)
                            return false;
                    }
                    sf.threads.Add(thread.firstPost.id);
                    return true;
                }
            }
            return false;
        }

        public Boolean addThread(Thread thread)
        {
            foreach(Thread t in threads)
            {
                if (t.firstPost.id == thread.firstPost.id)
                    return false;
            }
            threads.Add(thread);
            return true;
        }

        public static DemoDB getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new DemoDB();
                }
                return singleton;
            }
            
        }

        public Boolean isForumExists(string name)
        {
            for (int i = 0; i < forums.Count; i++)
            {
                if (((Forum)(forums.ElementAt(i))).forumName.Equals((name)))
                    return true;
            }
                return false;
        }

        public Boolean isSubForumExists(string forumName, string subForumName)
        {
            for (int i = 0; i < subForums.Count; i++)
            {
            //    if ((subForums.ElementAt(i))._name == subForumName && (subForums.ElementAt(i))._forumName == forumName)
                    return true;
            }
            return false;
        }

        public User getUser(string userName)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if ((users.ElementAt(i)).userName.Equals(userName))
                    return users.ElementAt(i);
            }
            return null;
        }

       public List<Post> getRelatedPosts(int postId)
        {
            List<Post> curPost = new List<Post>();
            for (int i = 0; i < posts.Count; i++)
            {
                if ((posts.ElementAt(i).parentId == postId))
                {
                    curPost.Add(posts.ElementAt(i));
                }
            }
            return curPost;

        }

        public Boolean createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            
            if (forumName.Equals("") || descrption.Equals("") || forumPolicy.Equals("") || forumRules.Equals("") || administrators == null)
            {
                Console.WriteLine("cannot create new forum because one or more of the fields was empty");
                return false;
            }
            Forum newForum = new Forum(forumName, descrption, forumPolicy, forumRules, administrators);
            forums.Add(newForum);
            return true;
           
        }

        public Boolean initialize(String userName, String password, String email)
        {

            if (userName.Equals("") || password.Equals("") || email.Equals(""))
            {
                Console.WriteLine("one or more of the fields is missing");
                return false;
            }
            // should check if the password is strong enough
            // should check if the the email is in a correct format
            // should send configuration email to the super user's email
            Console.WriteLine("the system was initialized successully");
            return true;
        }

        public Boolean setForumPreferences(String forumName ,String newDescription, String newForumPolicy, String newForumRules)
        {
            bool isChanged = false;
            for (int i = 0; i < forums.Count() && !isChanged; i++)
            {
                if (forums[i].forumName.Equals(forumName))
                {
                    forums[i].description = newDescription;
                    forums[i].forumPolicy = newForumPolicy;
                    forums[i].forumRules = newForumRules;
                    Console.WriteLine(forumName + "preferences had changed successfully");
                    isChanged = true;
                }
                
            }
            if (!isChanged) Console.WriteLine("This forum" + forumName + "doesn't exist");
            return isChanged;
        }

        public List<Message> Messages
        {
            get { return messages; }
        }

    }
}
