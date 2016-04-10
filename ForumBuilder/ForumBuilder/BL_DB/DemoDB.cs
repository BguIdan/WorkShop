using System;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.Controllers;
using ForumBuilder.Users;
using System.Net.Mail;
using ForumBuilder.BL_Back_End;

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
        private List<SuperUser> superUsers;
        private static DemoDB singleton;

        private DemoDB()
        {
            forums = new List<Forum>();
            subForums = new List<SubForum>();
            threads = new List<Thread>();
            posts = new List<Post>();
            users = new List<User>();

        }
        public int /* the new post id*/ getAvilableIntOfPost()
        {
            int max = 0;
            foreach (Post p in posts)
            {
                if (p.id >= max)
                    max = p.id + 1;
            }
            return max;
        }

        internal bool dismissModerator(string dismissedModerator, string dismissByAdmin, SubForum subForum)
        {
            subForum.moderators.Remove(dismissedModerator);
            return true;
        }

        internal bool deleteThreadFromSubforum(int firstPostId)
        {
            foreach (SubForum sf in subForums)
            {
                if (sf.threads.Contains(firstPostId))
                {
                    sf.threads.Remove(firstPostId);
                    return true;
                }
            }
            return false;

        }

        internal bool nominateModerator(string newModerator, string nominatorUser, DateTime date, SubForum subForum)
        {
            if (DateTime.Now.CompareTo(date) > 0)
            {
                Console.WriteLine("the date already past");
                return false;
            }
            subForum.moderators.Remove(newModerator);
            subForum.moderators.Add(newModerator, date);
            return true;
        }

        internal Forum getforumByName(string forumName)
        {
            foreach (Forum f in forums)
            {
                if (f.forumName.Equals(forumName))
                    return f;
            }
            return null;
        }

        internal bool removeThreadByfirstPostId(int firstPostToDelete)
        {
            Thread tr = null;
            foreach (Thread t in threads)
            {
                if (t.firstPost.id == firstPostToDelete)
                    tr = t;
            }
            if (tr == null)
                return false;
            threads.Remove(tr);
            return true;

        }

        public Boolean addPost(Post post)
        {
            foreach (Post p in posts)
            {
                if (p.id == post.id)
                    return false;
            }
            posts.Add(post);
            return true;
        }

        internal bool banMember(string bannedMember, string bannerUserName, string forumName)
        {
            Forum forum = this.getforumByName(forumName);
            forum.members.Remove(bannedMember);
            return true;
        }

        internal bool changePolicy(string newPolicy, string forumName)
        {
            Forum forum = this.getforumByName(forumName);
            forum.forumPolicy = newPolicy;
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

        internal bool nominateAdmin(string newAdmin, string nominatorName, string forumName)
        {
            if (isSuperUser(nominatorName))
            {
                Forum forum = getforumByName(forumName);
                string adminNameIfExist = "";
                foreach (String s in forum.administrators)
                {
                    if (s.Equals(newAdmin))
                    {
                        adminNameIfExist = s;
                    }
                }
                if (adminNameIfExist.Equals(""))
                {
                    forum.administrators.Add(newAdmin);
                }
                return true;
            }
            return false;
        }
        public bool isSuperUser(string userName)
        {
            foreach (SuperUser superUser in superUsers)
            {
                if (superUser._userName.Equals(userName))
                    return true;
            }
            return false;
        }

        internal bool dismissAdmin(string adminToDismissed, string forumName)
        {
            Forum forum = getforumByName(forumName);
            forum.administrators.Remove(adminToDismissed);
            return true;
        }



        public Boolean addThread(Thread thread)
        {
            foreach (Thread t in threads)
            {
                if (t.firstPost.id == thread.firstPost.id)
                    return false;
            }
            threads.Add(thread);
            return true;
        }

        public void removePost(Post p)
        {
            posts.Remove(p);
        }

        internal Post getPost(int postId)
        {
            foreach (Post p in posts)
            {
                if (p.id == postId)
                    return p;
            }
            return null;
        }

        internal SubForum getSubforumByThread(Thread t)
        {
            foreach (SubForum sf in subForums)
            {
                if (sf.threads.Contains(t.firstPost.id))
                {
                    return sf;
                }
            }
            return null;
        }

        internal Thread getThreadByFirstPostId(int postId)
        {
            foreach (Thread t in threads)
            {
                if (t.firstPost.id == postId)
                    return t;
            }
            return null;
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

        public Boolean addUser(User user)
        {
            foreach (User u in users)
            {
                if (u.userName.Equals(user.userName))
                    return false;
            }
            users.Add(user);
            return true;
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
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (password.ElementAt(i) <= '9' && password.ElementAt(i) >= '0')
                {
                    isNumExist = true;
                }
                if (password.ElementAt(i) <= 'Z' && password.ElementAt(i) >= 'A')
                {
                    isBigKeyExist = true;
                }
                if (password.ElementAt(i) <= 'z' && password.ElementAt(i) >= 'a')
                {
                    isSmallKeyExist = true;
                }
                if (i < password.Length - 2 && (password.ElementAt(i).Equals(password.ElementAt(i + 1)) && password.ElementAt(i).Equals(password.ElementAt(i + 2))))
                {
                    isKeyRepeting3Times = true;
                }
                if (!(isNumExist && isSmallKeyExist && isBigKeyExist && !isKeyRepeting3Times))
                {
                    Console.WriteLine("password isnt strong enough");
                    return false;
                }
            }
            // check if the the email is in a correct format
            int index = email.IndexOf("@");
            if (index < 0 || index == email.Length - 1)
            {
                Console.WriteLine("error in email format");
                return false;
            }
            //  send configuration email to the super user's 
            sendmail(email);

            //adding the user
            SuperUser superUser = new SuperUser();
            superUser._email = email;
            superUser._password = password;
            superUser._userName = userName;
            superUsers.Add(superUser);

            Console.WriteLine("the system was initialized successully");
            return true;
        }

        private void sendmail(string email)
        {
            String ourEmail = "ourEmail@gmail.com";
            MailMessage mail = new MailMessage(ourEmail, email);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "please configure your account";
            mail.Body = "please configure your account";
            client.Send(mail);

        }

        //<<<<<<< HEAD
        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules)
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

        //=======

        public Boolean addSubForum(SubForum subForum)
        {
            foreach (SubForum sf in subForums)
            {
                if (sf.name == subForum.name)
                    return false;
            }
            subForums.Add(subForum);
            return true;
        }
        //>>>>>>> origin/registerToForum_and_createSF_nominateMod
    }
}
