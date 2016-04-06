﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private static DemoDB singleton;

        private DemoDB()
        {
            forums = new List<Forum>();
            subForums = new List<SubForum>();
            threads = new List<Thread>();
            posts = new List<Post>();
            users = new List<User>();

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
            try
            {
                if (forumName.Equals("") || descrption.Equals("") || forumPolicy.Equals("") || forumRules.Equals("") || administrators == null)
                {
                    Console.WriteLine("one of the fields was empty");
                    return false;
                }
                Forum newForum = new Forum(forumName, descrption, forumPolicy, forumRules, administrators);
                forums.Add(newForum);
                return true;
            }
            catch
            {
                return false;
            }
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
    }
}
