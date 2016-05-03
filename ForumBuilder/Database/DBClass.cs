using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using BL_Back_End;

namespace Database
{
    public class DBClass
    {
        private static List<int> avilabelPostIDs = new List<int>();
        private static int maxNotAvailable = -1;
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
            catch 
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
            catch
            {
            }
        }
        public void OpenConnectionDB()
        {
            try
            {
                connection.Open();
            }
            catch 
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

        public bool dismissModerator(string dismissedModerator, string subForumName, string forumName)
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
            catch
            {
                return false;
            }
            
        }
        public bool addSuperUser(string email, string password, string userName)
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
            catch 
            {
                return false;
            }
            
        }
        public bool nominateModerator(string newModerator, DateTime date, string subForumName, string forumName)
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
            catch
            {
                return false;
            }
        }
        public Forum getforumByName(string forumName)
        {
            Forum forum=null;
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
                    reader = command.ExecuteReader();
                    reader.Read();
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
                    return forum;
                }
                else
                {
                    //not exist
                    closeConnectionDB();
                    return forum;
                }
            }
            catch
            {
                closeConnectionDB();
                return forum;
            }
        }
        public Forum getForumByMember(string userName)
        {
            Forum forum = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from forums where users.userName='" + userName + "' and users.forumName=forums.forumName";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                String forumName="";
                while (reader.Read())
                {
                    count++;
                    forumName = reader.GetString(0);
                }
                if (count == 1)
                {
                    reader = command.ExecuteReader();
                    reader.Read();
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
                    return forum;
                }
                else
                {
                    //not exist
                    closeConnectionDB();
                    return forum;
                }
            }
            catch 
            {
                closeConnectionDB();
                return forum;
            }
        }
        public bool addMessage(string sender, string reciver, string content)
        {
            try
            {
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into subForumModerators (sender,reciver,content)" +
                    "values(" + sender + "," + reciver + "," + content + ")";
                command.ExecuteNonQuery();
                //added
                closeConnectionDB();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<string> getUserFriends(string userName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from friendOf where friendOf.userName='" + userName+ "'";
                OleDbDataReader reader = command.ExecuteReader();
                List<String> friends = new List<String>();
                while (reader.Read())
                {
                    friends.Add(reader.GetString(1));
                }
                closeConnectionDB();
                return friends;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool banMember(string bannedMember, string bannerUserName, string forumName)
        {/*
            Forum forum = this.getforumByName(forumName);
            forum.members.Remove(bannedMember);*/
            return true;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool changePolicy(string newPolicy, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE forums SET forumPolicy="+newPolicy+" where forumName='"+forumName+"' )";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool nominateAdmin(string newAdmin, string nominatorName, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into forumAdministrators (forumName,administratorName)" +
                        "values(" + forumName + "," + newAdmin + ")";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public User getSuperUser(string userName)
        {
            User user = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from superUsers where superUserName='" + userName + "'";
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
                    command2.CommandText = "Select * from users where userName='" + userName + "'";
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    user = new User(reader2.GetString(0), reader2.GetString(1), reader2.GetString(2));
                    closeConnectionDB();
                    return user;
                }
                else
                {
                    //not exist
                    closeConnectionDB();
                    return user;
                }
            }
            catch
            {
                closeConnectionDB();
                return user;
            }
            return null;
        }
        public bool dismissAdmin(string adminToDismissed, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "delete from forumAdministrators where subForumModerators.forumName='"
                + forumName + "'and forumAdministrators.AdministratorName='" + adminToDismissed+ "'";
                command.ExecuteNonQuery();
                //admin removed
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public User getUser(string userName)
        {
            User user = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "Select * from users where userName='" + userName + "'";
                OleDbDataReader reader2 = command2.ExecuteReader();
                user = new User(reader2.GetString(0), reader2.GetString(1), reader2.GetString(2));
                closeConnectionDB();
                return user;
            }
            catch
            {
                closeConnectionDB();
                return user;
            }
        }
        public Boolean addUser(string userName, string password, string email)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into users (userName,password,email) " +
                        "values(" + userName+","+password + "," +email + ")";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public Boolean createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into forums (forumName, descrption, forumPolicy, forumRules) " +
                        "values(" + forumName+","+ descrption + "," + forumPolicy + "," + forumRules +")";
                command.ExecuteNonQuery();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                foreach (string admin in administrators)
                {
                    command2.CommandText = "insert into forumadminstrators (forumName, administratorsName)" +
                            "values(" + forumName + "," + admin+ ")";
                    command2.ExecuteNonQuery();
                }
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE forums SET forumPolicy=" + newForumPolicy + ", description = " + newDescription + ", forumRules = " + newForumRules +" where forumName='" + forumName + "' )";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool addFriendToUser(string userName, string friendToAddName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into friendOf (userName, friendName)" +
                        "values(" + userName+ "," + friendToAddName + ")";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool removeFriendOfUser(string userName, string deletedFriendName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "delete from friendOf where userName='"+userName+
                    ", friendName='"+ deletedFriendName + "')";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public SubForum getSubForum(string subForumName, string forumName)
        {
            SubForum subForum = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from subForums where subForums.forumName='" + forumName + "' and "+
                    "subForums.subForumName = '" + subForumName+ "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "Select * from forums where subForumModerators.forumName='" + forumName + "' and "+
                    "subForumModerators.subForumName='" + subForumName + "'";
                OleDbDataReader reader2 = command2.ExecuteReader();
                subForum = new SubForum(reader.GetString(0), reader.GetString(1));
                while (reader2.Read())
                {
                    subForum.moderators.Add(reader2.GetString(2), DateTime.Parse(reader2.GetDateTime(3).ToString("dd MM yyyy")));
                }
                closeConnectionDB();
                return subForum;
            }
            catch
            {
                closeConnectionDB();
                return subForum;
            }
        }
        public List<Message> getMessages()
        {
            try
            {
                List<Message> messages = new List<Message>();
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from messages";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Message message = new Message(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                    messages.Add(message);
                }
                closeConnectionDB();
                return messages;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        public Boolean addSubForum(String  subForumName,String forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into subForums (subForumName,forumName) " +
                        "values("+subForumName +","+ forumName + ")";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public Post getPost(int postId)
        {
            Post post = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from posts where postID='" + postId + "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                post = new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")));
                closeConnectionDB();
                return post;
            }
            catch
            {
                closeConnectionDB();
                return post;
            }
        }
        public List<Post> getRelatedPosts(int postId)
        {
            if (postId == 0)
            {
                return null;
            }
            List<Post> curPost = new List<Post>();
            Post post = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from posts where parentPostID='" + postId + "'";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    post = new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")));
                    curPost.Add(post);
                }
                closeConnectionDB();
                return curPost;
            }
            catch
            {
                closeConnectionDB();
                return curPost;
            }
        }
        public int getAvilableIntOfPost()
        {
            int res = -2;
            foreach (int p in avilabelPostIDs)
            {
                res = p;
                break;
            }
            if (res != -2)
            {
                avilabelPostIDs.Remove(res);
                return res;
            }
            maxNotAvailable++;
            return maxNotAvailable;
        }
        public SubForum getSubforumByThreadFirstPostId(int id)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from threads where firstMessageId='" + id + "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                String sfName = reader.GetString(1);
                String fName = reader.GetString(2);
                closeConnectionDB();
                return getSubForum(sfName,fName);
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        public Thread getThreadByFirstPostId(int postId)
        {
            Thread thread = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "Select * from posts where postID='" + postId + "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                thread = new Thread( new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy"))));
                closeConnectionDB();
                return thread;
            }
            catch
            {
                closeConnectionDB();
                return thread;
            }
        }

        public bool addThread(string forumName, string subForumName, int firstMessageId)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into threads (firstMessageId,subForumName, forumName)" +
                        "values(" + firstMessageId + "," + subForumName + "," + forumName + ")";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool addPost(String writerUserName, Int32 postID, String headLine, String content, Int32 parentId, DateTime timePublished)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "insert into posts (postID,writerUserName, title,content,parentPostID,publishTime)" +
                        "values(" + postID + "," + writerUserName + "," + headLine + "," + content + "," + parentId +"," +
                        timePublished.Day + "/" + timePublished.Month + "/" + timePublished.Year + ")";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }

        public bool removeThread(int id)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "delete from theards where firstMessageId='" + id + "'";
                command.ExecuteNonQuery();
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool removePost(int id)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "delete from posts where PostID='"+ id+ "'";
                command.ExecuteNonQuery();
                closeConnectionDB();
                if (maxNotAvailable == id)
                    maxNotAvailable--;
                else
                {
                    avilabelPostIDs.Remove(id);
                }
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }

        public void clear()
        {
            try
            {
                OpenConnectionDB();
                List<String> commands = new List<string>();
                commands.Add("delete from forumadministrators");
                commands.Add("delete from messages");
                commands.Add("delete from friendOf");
                commands.Add("delete from subForumModerators");
                commands.Add("delete from theards");
                commands.Add("delete from posts");
                commands.Add("delete from subForums");
                commands.Add("delete from superUsers");
                commands.Add("delete from Users");
                commands.Add("delete from forums");

                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                foreach (string commandTXT in commands)
                {
                    command.CommandText = commandTXT;
                    command.ExecuteNonQuery();
                }
                closeConnectionDB();
                maxNotAvailable = -1;
                avilabelPostIDs = new List<int>();
            }
            catch
            {
                closeConnectionDB();
            }
        }
    }
}
