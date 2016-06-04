using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using BL_Back_End;
using Database;

namespace DataBase
{
    public class Cache
    {
        private Dictionary<string, User> users;
        private Dictionary<string, User> superUsers;
        private Dictionary<string, Forum> forums;
        private List<SubForum> subForums;
        private Dictionary<int, Thread> threads;
        private Dictionary<int, Post> posts;
        private Dictionary<int, Message> messages;
        private static Cache singleton;


        private Cache()
        {
          users = new Dictionary<string, User>() ;
          forums = new Dictionary<string, Forum>() ;
          subForums = new  List<SubForum>() ;
          threads = new  Dictionary<int, Thread>() ;
          posts = new  Dictionary<int, Post>();
          messages = new  Dictionary<int, Message>() ;
        }

        public static Cache getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new Cache();
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

        public int getMaxIntOfPost()
        {
            int maxPost = 0;
            foreach (int p in posts.Keys)
            {
                if (p > maxPost)
                {
                    maxPost = p;
                }
            }
            return maxPost;
        }

        public int numOfForums()
        {
            return forums.Count;
        }

        public bool dismissModerator(string dismissedModerator, string subForumName, string forumName)
        {
            return getSubForum(subForumName, forumName).moderators.Remove(dismissedModerator);
        }

        public SubForum getSubForum(string subForumName, string forumName)
        {
            foreach (SubForum sf in subForums)
            {
                if (sf.forum.Equals(forumName) && sf.name.Equals(subForumName))
                    return sf;
            }
            return null;
        }

        public bool addSuperUser(string email, string password, string userName)
        {
            User su = new User(userName, password, email);
            users.Add(userName, su);
            return true;
        }

        public bool nominateModerator(string newModerator, DateTime endDate, string subForumName, string forumName, String nominator)
        {
            SubForum sf = getSubForum(subForumName, forumName);
            sf.moderators.Add(newModerator, endDate);
            return true;
        }

        public Forum getforumByName(string forumName)
        {
            return forums[forumName];
        }

        public List<string> getsubForumsNamesOfForum(string forumName)
        {
            Forum f = forums[forumName];
            return f.subForums;
        }

        public List<string> getForums()
        {
            return new List<string>(forums.Keys);
        }

        //public List<String> getModertorsReport(String forumName)
        //{
        //   return null;
        //}

        //public bool addMessage(string sender, string reciver, string content)
        //{
        //   return true;
        //}

        public List<string> getUserFriends(string userName)
        {
            return users[userName].friends;  
        }

        public bool banMember(string bannedMember, string forumName)
        {
            forums[forumName].members.Remove(bannedMember);
            return true;
        }

        public bool changePolicy(string forumName, string policy, bool isQuestionIdentifying, int seniorityInForum,
        bool deletePostByModerator, int timeToPassExpiration, int minNumOfModerators, bool hasCapitalInPassword,
        bool hasNumberInPassword, int minLengthOfPassword)
        {
            forums[forumName].forumPolicy.policy = policy;
            forums[forumName].forumPolicy.isQuestionIdentifying = isQuestionIdentifying;
            forums[forumName].forumPolicy.seniorityInForum = seniorityInForum;
            forums[forumName].forumPolicy.deletePostByModerator = deletePostByModerator;
            forums[forumName].forumPolicy.timeToPassExpiration = timeToPassExpiration;
            forums[forumName].forumPolicy.minNumOfModerators = minNumOfModerators;
            forums[forumName].forumPolicy.hasCapitalInPassword = hasCapitalInPassword;
            forums[forumName].forumPolicy.hasNumberInPassword = hasNumberInPassword;
            forums[forumName].forumPolicy.minLengthOfPassword = minLengthOfPassword;
            return true;
        }
        
        public bool nominateAdmin(string newAdmin, string forumName)
        {
            forums[forumName].administrators.Add(newAdmin);
            return true;
        }

        public User getSuperUser(string userName)
        {
            return superUsers[userName];
        }

        public bool dismissAdmin(string adminToDismissed, string forumName)
        {
            return forums[forumName].administrators.Remove(adminToDismissed);
        }

        public User getUser(string userName)
        {
            return users[userName];
        }

        public Boolean addUser(string userName, string password, string email)
        {
            User us = new User(userName, password, email);
            users.Add(userName, us);
            return true;
        }

        public Boolean addMemberToForum(string userName, string forumName) 
        {
            forums[forumName].members.Add(userName);
            return true;
        }

        public List<string> getMembersOfForum(string forumName)
        {
            return forums[forumName].members;
        }

        public List<string> getSimularForumsOf2users(string userName1, string userName2)
        {
            List<string> simularForum = new List<string>();
            foreach (Forum f in forums.Values)
            {
                if (f.members.Contains(userName1) && f.members.Contains(userName2))
                    simularForum.Add(f.forumName);
            }
            return simularForum;
        }

        public Boolean createForum(string forumName, string description, ForumPolicy fp)
        {
            List<string> administrators = new  List<string>();
            Forum f = new Forum(forumName, description, fp, administrators);
            forums.Add(forumName, f);
            return true;
        }

        public Boolean setForumPreferences(String forumName, String newDescription, ForumPolicy fp)
        {
            forums[forumName].description = newDescription;
            forums[forumName].forumPolicy = fp;
            return true;
        }

        public bool addFriendToUser(string userName, string friendToAddName)
        {
            users[userName].friends.Add(friendToAddName);
            return true;
        }


        public bool removeFriendOfUser(string userName, string deletedFriendName)
        {
            users[userName].friends.Remove(deletedFriendName);
            return true;
        }

        public List<Message> getMessages()
        {
            return new List<Message>(messages.Values);
        }
        public Boolean addSubForum(String subForumName, String forumName)
        {
            SubForum sf = new SubForum(subForumName, forumName);
            subForums.Add(sf);
            return true;
        }
        
        public Post getPost(int postId)
        {
            return posts[postId];
        }

        public List<Post> getRelatedPosts(int postId)
        {
            List<Post> relatedPosts = new List<Post>();
            foreach (Post p in posts.Values)
            {
                if (p.parentId == postId)
                {
                    relatedPosts.Add(p);
                }
            }
            return relatedPosts;
        }
        /*public int getAvilableIntOfPost()
        {
            return 0;
        }*/

        public SubForum getSubforumByThreadFirstPostId(int id)
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

        public Thread getThreadByFirstPostId(int postId)
        {
            foreach (Thread t in threads.Values)
            {
                if (t.firstPost.id == postId)
                    return t;
            }
            return null;
        }

        public bool addThread(string forumName, string subForumName, int firstMessageId)
        {
            Thread thread = new Thread(getPost(firstMessageId));
            threads.Add(firstMessageId, thread);
            getSubForum(subForumName, forumName).threads.Add(firstMessageId);
            return true;
        }
        public bool addPost(String writerUserName, Int32 postID, String headLine, String content, Int32 parentId, DateTime timePublished, String forumName) 
        {
            Post post = new Post(writerUserName, postID, headLine, content, parentId, timePublished, forumName);
            posts.Add(postID, post);
            if (parentId != -1)
            {
                getPost(parentId).commentsIds.Add(postID);
            }
            return true;
        }

        public int numOfPostInForum(String forumName)
        {
            int count = 0;
            foreach (Post p in posts.Values)
            {
                if (p.forumName.Equals(forumName))
                    count++;
            }
            return count;
        }

        public bool removeThread(int id)
        {
            threads.Remove(id);
            getSubforumByThreadFirstPostId(id).threads.Remove(id);
            return true;
        }
        public bool removePost(int id)
        {
            posts.Remove(id);
            return true;
        }

        public Boolean updatePost(int postID, String title, String content)
        {
            posts[postID].title = title;
            posts[postID].content = content;
            return true;
        }

        public List<Post> getMemberPosts(String memberName, String forumName)
        {
            List<Post> memPost = new List<Post>();
            foreach (Post p in posts.Values)
            {
                if (p.forumName.Equals(forumName) && p.writerUserName.Equals(memberName))
                    memPost.Add(p);
            }
            return memPost;
        }
     //   public List<String> getSuperUserReportOfMembers()
      //  {
      //  }





    }
}
