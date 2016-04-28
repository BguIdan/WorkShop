using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Database
{
    public class DBClass
    {
        private static DBClass singleton;
        OleDbConnection connection;
        static void Main(string[] args)
        {
            //Program DB = new Program();
            //DB.initializeDB();
        }
        public static DBClass getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new DBClass();
                    singleton.initializeDB();
                }
                return singleton;
            }
        }
        public Boolean initializeDB()
        {
            try
            {
                connection = new OleDbConnection();
                connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Documents\sadna\forumDB.accdb;
                                                Persist Security Info=False;";
                connection.Open();             
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void closeConnectionDB()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {
            }
        }
        public void OpenConnectionDB()
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
            }
        }
        public Boolean MemberConnect(String userName, String password, String forumName)
        {
            OpenConnectionDB();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "Select * from forumMembers,users where users.userName='"
                +userName+"' and user.userName=forumMembers.memberName and users.password='"
                +password+"' and forumMembers.forumName='"+forumName+ "' and forumMembers.isConnected=False";
            OleDbDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            if (count == 1)
            {
                //set user connected
                //user is connected
                closeConnectionDB();
                return true;
            }
            else
            {
                //user is already connected or there was a problem with one or more of the args
                closeConnectionDB();
                return false;
            }
        }

        internal bool dismissModerator(string dismissedModerator, string subForumName, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from subForumModerators where subForumModerators.forumName='"
                    + forumName + "' and subForumModerators.subForumName='" + subForumName + "'and subForumModerators.moderatorName='"
                    + dismissedModerator + "'";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection;
                    command2.CommandText = "delete from subForumModerators where subForumModerators.forumName='"
                    + forumName + "' and subForumModerators.subForumName='" + subForumName + "'and subForumModerators.moderatorName='"
                    + dismissedModerator + "'";
                    command.ExecuteNonQuery();
                    //moderator removed
                    closeConnectionDB();
                    return true;
                }
                else
                {
                    //moderator does not exist
                    closeConnectionDB();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        internal bool addSuperUser(string email, string password, string userName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from users where users.userName='" + userName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count == 0)
                {
                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection;
                    command2.CommandText = "insert into users (userName,password,email) values(" + userName + "," + password + "," + email + ")";
                    command2.ExecuteNonQuery();
                    OleDbCommand command3 = new OleDbCommand();
                    command3.Connection = connection;
                    command3.CommandText = "insert into superUsers (superUserName) values(" + userName + ")";
                    command3.ExecuteNonQuery();
                    //added
                    closeConnectionDB();
                    return true;
                }
                else
                {
                    //alredy exist
                    closeConnectionDB();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
        internal bool nominateModerator(string newModerator, string nominatorUser, DateTime date, string subForumName, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from users where subForumModerators.subForumName='" + subForumName + "'" +
                    " and subForumModerators.forumName='" + forumName + "'" +
                    " and subForumModerators.moderatorName = '" + newModerator + "'";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count == 0)
                {
                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection;
                    command2.CommandText = "insert into subForumModerators (subForumName,forumName,moderatorName,endTermOfOffice)"+
                        "values(" + subForumName + "," + forumName + "," + newModerator+","+ date.Day + "/" + date.Month + "/" + date.Year + ")";
                    command2.ExecuteNonQuery();
                    //added
                    closeConnectionDB();
                    return true;
                }
                else
                {
                    //alredy exist
                    closeConnectionDB();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        internal Forum getforumByName(string forumName)
        {
            Forum forum;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from forums where forums.forumName='" + forumName+ "'";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                }
                if (count == 1)
                {
                    OleDbCommand command2 = new OleDbCommand();
                    command2.Connection = connection;
                    command2.CommandText = "Select * from forums where forumAdministartors.forumName='" + forumName + "'";
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    List<String> administrators = new List<String>();
                    while (reader2.Read())
                    {
                        administrators.Add(reader2.GetString(1));
                    }
                    forum = new Forum(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), administrators);
                    return true;
                }
                else
                {
                    //not exist
                    closeConnectionDB();
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return null;
        }
        internal Forum getForumByMember(string userName)
        {
            foreach (Forum f in forums)
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

        internal int getNextFreeMessageId()
        {
            int id = 0;
            foreach (Message m in messages)
            {
                if (id <= m.id)
                {
                    id = m.id + 1;
                }
            }
            return id;
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
        public User getSuperUser(string userName)
        {
            foreach (User superUser in superUsers)
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

        internal bool removeThread(int id, string subForumName, string forumName)
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
