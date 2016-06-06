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
        private static Cache singleton;

        private Cache()
        {
          users = new Dictionary<string, User>() ;
          forums = new Dictionary<string, Forum>() ;
          subForums = new  List<SubForum>() ;
          threads = new  Dictionary<int, Thread>() ;
          posts = new  Dictionary<int, Post>();
          superUsers = new Dictionary<string, User>();
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
            if (this.superUsers != null)
                this.superUsers.Clear();

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
            try
            {
                return forums.Count;
            }
            catch
            {
                return 0;
            }

        }

        public bool dismissModerator(string dismissedModerator, string subForumName, string forumName)
        {
            try
            {
                return getSubForum(subForumName, forumName).moderators.Remove(dismissedModerator);
            }
            catch
            {
                return false;
            }
        }

        public SubForum getSubForum(string subForumName, string forumName)
        {
            try
            {
                foreach (SubForum sf in subForums)
                {
                    if (sf.forum.Equals(forumName) && sf.name.Equals(subForumName))
                        return sf;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool addSuperUser(string email, string password, string userName)
        {
            try
            {
                User su = new User(userName, password, email, DateTime.Today);
                users.Add(userName, su);
                superUsers.Add(userName, su);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool nominateModerator(string newModerator, DateTime endDate, string subForumName, string forumName, String nominator)
        {
            try
            {
                SubForum sf = getSubForum(subForumName, forumName);
                sf.moderators.Add(newModerator, new Moderator(newModerator, endDate, DateTime.Today, nominator));
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Forum getforumByName(string forumName)
        {
            try
            {
                return forums[forumName];
            }
            catch
            {
                return null;
            }
            
        }

        public List<string> getsubForumsNamesOfForum(string forumName)
        {
            try
            {
                Forum f = forums[forumName];
                return f.subForums;
            }
            catch
            {
                return null;
            }
        }

        public List<string> getForums()
        {
            try
            {
                return forums.Keys.ToList();
            }
            catch
            {
                return null;
            }
        }

        public List<string> getModertorsReport(String forumName)
        {
            try
            {
                List<string> report = new List<string>();
                List<string> subforum = getsubForumsNamesOfForum(forumName);
                foreach (string sf in subforum)
                {
                    string ans = "";
                    SubForum subf = getSubForum(sf, forumName);
                    foreach (Moderator m in subf.moderators.Values)
                    {
                        ans += "subForum: " + sf + ", \t moderator: " + m.userName + ", \t nominator: " + m.nominatorName + ",\t DateAdded:" + m.dateAdded.ToString("dd MM yyyy") + "added posts:";
                        foreach (Post p in posts.Values)
                        {
                            if (p.writerUserName.Equals(m.userName))
                            {
                                ans += " \n\t post title: " + p.title + " \n\t post content:" + p.content;
                            }
                        }                    
                    }
                    report.Add(ans);
                }
                return report;
            }
            catch
            {
                return null;
            }
        }

        public List<String> getSuperUserReportOfMembers()
        {
            try
            {
                List<string> emails = new List<string>();
                foreach(User u in users.Values)
                {
                    if (!emails.Contains(u.email))
                    {
                        emails.Add(u.email);
                    }
                }
                List<string> report = new List<string>();
                foreach (string e in emails)
                {
                    foreach (User u in users.Values)
                    {
                        string ans = "";
                        if (u.email.Equals(e))
                        {
                            foreach (Forum f in forums.Values)
                            {
                                if (f.members.Contains(u.userName))
                                {
                                    ans = "Email : " + e + "  UserName : " + u.userName + "  In forum : " + f.forumName;
                                    break;
                                }
                            }
                        }
                        report.Add(ans);
                    }
                }
                return report;
            }
            catch
            {
                return null;
            }
        }

        public List<string> getUserFriends(string userName)
        {
            try
            {
                return users[userName].friends;
            }
            catch
            {
                return null;
            }
        }

        public string getPassword(string userName)
        {
            try
            {
                return users[userName].password;
            }
            catch
            {
                return null;
            }
        }

        public bool banMember(string bannedMember, string forumName)
        {
            try
            {
                forums[forumName].members.Remove(bannedMember);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool changePolicy(string forumName, string policy, bool isQuestionIdentifying, int seniorityInForum,
        bool deletePostByModerator, int timeToPassExpiration, int minNumOfModerators, bool hasCapitalInPassword,
        bool hasNumberInPassword, int minLengthOfPassword)
        {
            try
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
            catch
            {
                return false;
            }

        }
        
        public bool nominateAdmin(string newAdmin, string forumName)
        {
            try
            {
                forums[forumName].administrators.Add(newAdmin);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User getSuperUser(string userName)
        {
            try
            {
                return superUsers[userName];
            }
            catch
            {
                return null;
            }
        }

        public bool dismissAdmin(string adminToDismissed, string forumName)
        {
            try
            {
                return forums[forumName].administrators.Remove(adminToDismissed);
            }
            catch
            {
                return false;
            }
        }

        public User getUser(string userName)
        {
            try
            {
                return users[userName];
            }
            catch
            {
                return null;
            }
        }

        public Boolean addUser(string userName, string password, string email)
        {
            try
            {
                User us = new User(userName, password, email, DateTime.Today);
                users.Add(userName, us);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Boolean addMemberToForum(string userName, string forumName) 
        {
            try
            {
                forums[forumName].members.Add(userName);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<string> getMembersOfForum(string forumName)
        {
            try
            {
                return forums[forumName].members;

            }
            catch
            {
                return null;
            }
        }

        public List<string> getSimularForumsOf2users(string userName1, string userName2)
        {
            try
            {
                List<string> simularForum = new List<string>();
                foreach (Forum f in forums.Values)
                {
                    if (f.members.Contains(userName1) && f.members.Contains(userName2))
                        simularForum.Add(f.forumName);
                }
                return simularForum;
            }
            catch
            {
                return null;
            }

        }

        public Boolean createForum(string forumName, string description, ForumPolicy fp)
        {
            try
            {
                List<string> administrators = new List<string>();
                Forum f = new Forum(forumName, description, fp, administrators);
                forums.Add(forumName, f);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Boolean setForumPreferences(String forumName, String newDescription, ForumPolicy fp)
        {
            try
            {
                forums[forumName].description = newDescription;
                forums[forumName].forumPolicy = fp;
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool addFriendToUser(string userName, string friendToAddName)
        {
            try
            {
                users[userName].friends.Add(friendToAddName);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool removeFriendOfUser(string userName, string deletedFriendName)
        {
            try
            {
                users[userName].friends.Remove(deletedFriendName);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public Boolean addSubForum(String subForumName, String forumName)
        {
            try
            {
                SubForum sf = new SubForum(subForumName, forumName);
                subForums.Add(sf);
                forums[forumName].subForums.Add(subForumName);
                return true;
            }
            catch
            {
                return false;
            }

        }
        
        public Post getPost(int postId)
        {
            try
            {
                return posts[postId];
            }
            catch
            {
                return null;
            }
        }

        public List<Post> getRelatedPosts(int postId)
        {
            try
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
            catch
            {
                return null;
            }

        }

        public SubForum getSubforumByThreadFirstPostId(int id)
        {
            try
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
            catch
            {
                return null;
            }
        }

        public Thread getThreadByFirstPostId(int postId)
        {
            try
            {
                foreach (Thread t in threads.Values)
                {
                    if (t.firstPost.id == postId)
                        return t;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool addThread(string forumName, string subForumName, int firstMessageId)
        {
            try
            {
                Thread thread = new Thread(getPost(firstMessageId));
                threads.Add(firstMessageId, thread);
                getSubForum(subForumName, forumName).threads.Add(firstMessageId);
                return true;
            }
            catch
            {
                return false;
            }

        }
        
        public bool addPost(String writerUserName, Int32 postID, String headLine, String content, Int32 parentId, DateTime timePublished, String forumName) 
        {
            try
            {
                Post post = new Post(writerUserName, postID, headLine, content, parentId, timePublished, forumName);
                posts.Add(postID, post);
                if (parentId != -1)
                {
                    getPost(parentId).commentsIds.Add(postID);
                }
                return true;
            }
            catch
            {
                return false;
            }

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
            try
            {
                threads.Remove(id);
                getSubforumByThreadFirstPostId(id).threads.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool removePost(int id)
        {
            try
            {
                posts.Remove(id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Boolean updatePost(int postID, String title, String content)
        {
            try
            {
                posts[postID].title = title;
                posts[postID].content = content;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Post> getMemberPosts(String memberName, String forumName)
        {
            try
            {
                List<Post> memPost = new List<Post>();
                foreach (Post p in posts.Values)
                {
                    if (p.forumName.Equals(forumName) && p.writerUserName.Equals(memberName))
                        memPost.Add(p);
                }
                return memPost;
            }
            catch
            {
                return null;
            }

        }

        private string enc(string password)
        {
            char[] passArray = password.ToArray();
            string res = "";
            for (int i = 0; i < passArray.Length; i++)
            {
                passArray[i] = (char)(((int)passArray[i]) + i % 5 + 1);
            }
            for (int i = 0; i < passArray.Length; i++)
            {
                res = res + passArray[i];
            }
            return res;
        }

        private string dec(string password)
        {
            char[] passArray = password.ToArray();
            string res = "";
            for (int i = 0; i < passArray.Length; i++)
            {
                passArray[i] = (char)(((int)passArray[i]) - i % 5 - 1);
            }
            for (int i = 0; i < passArray.Length; i++)
            {
                res = res + passArray[i];
            }
            return res;
        }

        internal bool changePassword(string userName, string newPaswword)
        {
            try
            {
                users[userName].password = newPaswword;
                users[userName].lastTimeUpdatePassword = DateTime.Today;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*public int getAvilableIntOfPost()
        {
            return 0;
        }*/


    }
}
