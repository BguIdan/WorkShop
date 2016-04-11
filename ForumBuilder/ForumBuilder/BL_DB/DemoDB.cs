using System;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.Controllers;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.BL_DB
{
    public class DemoDB
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
            superUsers = new List<SuperUser>();
            messages= new List<Message>();
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
        public void clear()
        {
            if (this.forums != null)
                this.forums.Clear();
            if (this.subForums != null)
                this.subForums.Clear();
            if (this.threads != null)
                this.threads.Clear();
            if (this.posts != null)
                this.posts.Clear();
            if (this.users != null)
                this.users.Clear();
            if (this.messages != null)
                this.messages.Clear();
        }
        
        internal bool dismissModerator(string dismissedModerator, string subForumName,string forumName)
        {
            getSubForum(subForumName,forumName).moderators.Remove(dismissedModerator);
            return true;
        }
        internal bool addSuperUser(string email, string password, string userName)
        {
            SuperUser su = new SuperUser(email, password, userName);
            superUsers.Add(su);
            users.Add(su);
            return true;
        }
        internal bool nominateModerator(string newModerator, string nominatorUser, DateTime date, SubForum subForum)
        {
            foreach(string s in subForum.moderators.Keys)
            {
                if (s.Equals(newModerator))
                {
                    return false;
                }
            }
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
        internal Forum getForumByMember(string userName)
        {
            foreach(Forum f in forums)
            {
                if (f.members.Contains(userName))
                    return f;
            }
            return null;
        }
        internal bool addMessage(int id, string sender, string reciver, string content)
        {
            messages.Add(new Message(id, sender, reciver, content));
            return true;
        }
        internal List<string> getUserFriends(string userName)
        {
            return getUser(userName).friends;
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
        internal bool nominateAdmin(string newAdmin, string nominatorName, string forumName)
        {
            Forum forum = getforumByName(forumName);
            forum.administrators.Add(newAdmin);
            return true;
        }
        public SuperUser getSuperUser(string userName)
        {
            foreach (SuperUser superUser in superUsers)
            {
                if (superUser.userName.Equals(userName))
                    return superUser;
            }
            return null;
        }
        internal bool dismissAdmin(string adminToDismissed, string forumName)
        {
            Forum forum = getforumByName(forumName);
            forum.administrators.Remove(adminToDismissed);
            return true;
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
        public Boolean addUser(string userName, string password, string mail)
        {
            foreach (User u in users)
            {
                if (u.userName.Equals(userName))
                    return false;
            }
            users.Add(new User(userName, password, mail));
            return true;
        }
        public Boolean createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            Forum newForum = new Forum(forumName, descrption, forumPolicy, forumRules, administrators);
            forums.Add(newForum);
            return true;

        }
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
                    isChanged = true;
                }
            }
            return isChanged;
        }
        internal bool addFriendToUser(string userName, string friendToAddName)
        {
            User user = getUser(userName);
            if (user.friends.Contains(friendToAddName))
                return false;
            user.friends.Add(friendToAddName);
            return true;
        }
        internal bool removeFriendOfUser(string userName, string deletedFriendName)
        {
            User user = getUser(userName);
            user.friends.Remove(deletedFriendName);
            return true;
        }
        internal SubForum getSubForum(string subForumName, string forumName)
        {
            foreach (Forum f in forums)
            {
                if (f.forumName.Equals(forumName))
                {
                    foreach (SubForum sf in subForums)
                    {
                        if (sf.name.Equals(subForumName))
                            return sf;
                    }
                }
            }
            return null;
        }
        public List<Message> Messages
        {
            get { return messages; }
        }
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


        internal Post getPost(int postId)
        {
            foreach (Post p in posts)
            {
                if (p.id == postId)
                    return p;
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
        public int getAvilableIntOfPost()
        {
            int max = 0;
            foreach (Post p in posts)
            {
                if (p.id >= max)
                    max = p.id + 1;
            }
            return max;
        }
        internal SubForum getSubforumByThreadFirstPostId(int id)
        {
            foreach (SubForum sf in subForums)
            {
                if (sf.threads.Contains(id))
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

        internal bool addThread(string headLine, string content, string writerName, string forum, string subForum, int id, DateTime timePublished)
        {
            Thread thread = new Thread(getPost(id));
            threads.Add(thread);
            getSubForum(subForum, forum).threads.Add(id);
            return true;
        }
        internal bool addPost(String writerUserName, Int32 id, String headLine, String content, Int32 parentId, DateTime timePublished)
        {
            Post post = new Post(writerUserName, id, headLine, content, parentId, timePublished);
            posts.Add(post);
            if (parentId != -1)
            {
                getPost(parentId).commentsIds.Add(id);
            }
            return true;
        }

        internal bool removeThread(int id,string subForumName,string forumName)
        {
            Thread thread = getThreadByFirstPostId(id);
            threads.Remove(thread);
            getSubForum(subForumName, forumName).threads.Remove(id);
            return true;
        }
        internal bool removePost(int id)
        {
            Post post = getPost(id);
            posts.Remove(post);
            if (post.parentId != -1)
            {
                getPost(post.parentId).commentsIds.Remove(id);
            }
            return true;
        }

    }
}
