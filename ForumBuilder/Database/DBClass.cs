using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using BL_Back_End;
using System.Data;

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
            //DBClass db = DBClass.getInstance;
            //db.initializeDB();
            //db.clear();
            /* 
             DBClass db = DBClass.getInstance;
             db.initializeDB();
             db.clear();
             db.addSuperUser("guy@gmail.com", "mypassword", "super1");
             db.addUser("admin1", "mypassword2", "guy2@gmail.com");
             db.addUser("admin2", "mypassword3", "guy3@gmail.com");
             db.addUser("admin3", "mypassword3", "guy3@gmail.com");
             db.addUser("user1", "mypassword4", "guy4@gmail.com");
             db.addUser("user2", "mypassword4", "guy4@gmail.com");
             db.addUser("user3", "mypassword4", "guy4@gmail.com");
             db.addUser("user4", "mypassword4", "guy4@gmail.com");
             db.addUser("user5", "mypassword4", "guy4@gmail.com");
             List<String> list= new List<string>();
             list.Add("admin1");
             list.Add("admin2");
             db.createForum("forum1", "is", "the", "best",list );
             db.nominateAdmin("admin3", "forum1");
             db.addSubForum("subForum1", "forum1");
             db.nominateModerator("user1", DateTime.Today, "subForum1", "forum1","admin3");
             db.nominateModerator("user5", DateTime.Today, "subForum1", "forum1","admin3");
             db.addMemberToForum("user2", "forum1");
             db.addMemberToForum("user3", "forum1");
             db.addMemberToForum("user4", "forum1");
             db.addMemberToForum("user5", "forum1");
             db.addFriendToUser("user2", "user3");
             db.addFriendToUser("user2", "user1");
             db.addMessage("user2", "user4", "hello its me");
             int id = db.getAvilableIntOfPost();
             db.addPost("user2",id, "hello", "my name is", -1,DateTime.Today,"forum1");
             db.addThread("forum1", "subForum1", id);
             int id2 = db.getAvilableIntOfPost();
             db.addPost("user3", id2, "what?", "your name is",id, DateTime.Today, "forum1");
             db.addPost("user2", db.getAvilableIntOfPost(), "what?", "my name is", id2, DateTime.Today, "forum1");
             db.addPost("user3", db.getAvilableIntOfPost(), "what?", "your name is", id, DateTime.Today, "forum1");
             db.getsubForumsNamesOfForum("forum1");

             Forum forum1=db.getforumByName("forum1");
             SubForum subForum1=db.getSubForum("subForum1","forum1");
             Post p=db.getPost(id);
             List<String> members= db.getMembersOfForum("forum1");
             List<Message> messages = db.getMessages();
             List<Post> posts=db.getRelatedPosts(0);
             SubForum subForum2=db.getSubforumByThreadFirstPostId(id);
             User u1= db.getSuperUser("super1");
             User u2 = db.getUser("user2");
             Thread thread = db.getThreadByFirstPostId(0);
             List<String> friends=db.getUserFriends("user2");
             db.dismissModerator("user5", "subForum1", "forum1");
             db.dismissAdmin("admin1", "forum1");
             db.banMember("user2", "forum1");

             db.setForumPreferences("forum1", "desc","pol","rul");
             db.changePolicy("change", "forum1");
             db.removeFriendOfUser("user2", "user3");
             db.removePost(1);
             db.removeThread(0);

             //getsimu

             //db.clear();
             //Program DB = new Program();
             //DB.initializeDB();
          */
        }
        public static DBClass getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new DBClass();
                    singleton.initializeDB();
                    maxNotAvailable = Math.Max(singleton.getMaxIntOfPost(), -1);
                }
                return singleton;
            }
        }

        private int getMaxIntOfPost()
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  Max(postID) FROM  posts";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int x = reader.GetInt32(0);
                    closeConnectionDB();
                    return x;
                }
                else
                {
                    closeConnectionDB();
                    return -1;
                }
            }
            catch
            {
                closeConnectionDB();
                return -1;
            }
        }

        public Boolean initializeDB()
        {
            try
            {
                connection = new OleDbConnection();
                //connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Idan\Workshop\WorkShop.git\forumDB.mdb;
                //                                Persist Security Info=False;";
                //connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\User\Desktop\WorkShop\forumDB.mdb;
                //                                Persist Security Info=False;";

                connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\User\Desktop\WorkShop\forumDB.mdb;
                Persist Security Info=False;";
                //connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\gal\Desktop\wsep\New Folder\project\forumDB.mdb;
                //Persist Security Info=False;";
                //connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\gal\Desktop\wsep\New Folder\project\forumDB.mdb;
                //                                Persist Security Info=False;";

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
        /*public Boolean MemberConnect(String userName, String password, String forumName)
        {
            OpenConnectionDB();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT  * FROM  members,users where users.userName='"
                + userName+ "' and user.userName=members.memberName and users.password='"
                + password+ "' and members.forumName='" + forumName+ "' and members.isConnected=False";
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
        }*/

        public int numOfForums()
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  Count(forumName) FROM  forums";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                closeConnectionDB();
                return reader.GetInt32(0);
            }
            catch
            {
                closeConnectionDB();
                return -1;
            }
        }
        public bool dismissModerator(string dismissedModerator, string subForumName, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  subForumModerators where subForumModerators.forumName='"
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
                    command2.CommandText = "DELETE  FROM  subForumModerators where subForumModerators.forumName='"
                    + forumName + "' and subForumModerators.subForumName='" + subForumName + "'and subForumModerators.moderatorName='"
                    + dismissedModerator + "'";
                    command2.ExecuteNonQuery();
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
                command.CommandText = "SELECT  * FROM  users where users.userName='" + userName + "'";
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
                    command2.CommandText = "INSERT INTO users ([userName],[password],[email]) VALUES (?,?,?)";
                    command2.Parameters.AddWithValue("userName", userName);
                    command2.Parameters.AddWithValue("password", password);
                    command2.Parameters.AddWithValue("email", email);
                    command2.ExecuteNonQuery();
                    OleDbCommand command3 = new OleDbCommand();
                    command3.Connection = connection;
                    command3.CommandText = "INSERT INTO superUsers ([superUserName]) values (?)";
                    command3.Parameters.AddWithValue("superUserName", userName);
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
        public bool nominateModerator(string newModerator, DateTime date, string subForumName, string forumName, String nominator)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "INSERT INTO subForumModerators " +
                    "([subForumName],[forumName],[moderatorName],[endTermOfOffice],[nominator],[dateAdded]) " +
                        "values (?,?,?,?,?,?)";
                command2.Parameters.AddWithValue("subForumName", subForumName);
                command2.Parameters.AddWithValue("forumName", forumName);
                command2.Parameters.AddWithValue("moderatorName", newModerator);
                command2.Parameters.AddWithValue("endTermOfOffice", date.Day + "/" + date.Month + "/" + date.Year);
                command2.Parameters.AddWithValue("nominator", nominator);
                command2.Parameters.AddWithValue("dateAdded", DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
                command2.ExecuteNonQuery();
                //added
                closeConnectionDB();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Forum getforumByName(string forumName)
        {
            Forum forum = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  forums where forums.forumName='" + forumName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "SELECT  * FROM  forumAdministrators where forumAdministrators.forumName='" + forumName + "'";
                OleDbDataReader reader2 = command2.ExecuteReader();
                List<String> administrators = new List<String>();
                while (reader2.Read())
                {
                    administrators.Add(reader2.GetString(1));
                }
                forum = new Forum(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), administrators);
                closeConnectionDB();
                List<String> members = getMembersOfForum(forumName);
                forum.members = members;
                List<String> subForums = getsubForumsNamesOfForum(forumName);
                forum.subForums = subForums;
                return forum;
            }
            catch
            {
                closeConnectionDB();
                return forum;
            }
        }

        public List<string> getsubForumsNamesOfForum(string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  subForums where forumName='" + forumName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                List<String> subForums = new List<String>();
                while (reader.Read())
                {
                    subForums.Add(reader.GetString(0));
                }
                closeConnectionDB();
                return subForums;
            }
            catch (Exception e)
            {
                closeConnectionDB();
                return null;
            }
        }

        public List<String> getForums()
        {
            try
            {
                OpenConnectionDB();
                List<String> forums = new List<String>();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  forumName FROM  forums";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    forums.Add(reader.GetString(0));
                }
                closeConnectionDB();
                return forums;
            }
            catch
            {
                closeConnectionDB();
                return null; ;
            }
        }
        public List<String> getModertorsReport(String forumName)
        {
            try
            {
                OpenConnectionDB();
                List<String> modertorsReport = new List<String>();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT subForumModerators.subForumName," +
                    "subForumModerators.moderatorName,subForumModerators.nominator," +
                    "subForumModerators.dateAdded,posts.title,posts.content FROM" +
                    "subForumModerators,posts where subForumModerators.forumName='" +
                    forumName + "' and posts.forumName=subForumModerators.forumName" +
                    "and posts.writerUserName=subForumModerators.moderatorName";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    modertorsReport.Add(reader.GetString(0) + "," + reader.GetString(1) + "," +
                        reader.GetString(2) + "," +
                        reader.GetDateTime(3).ToString("dd MM yyyy") + "," +
                        reader.GetString(4) + "," + reader.GetString(5));
                }
                closeConnectionDB();
                return modertorsReport;
            }
            catch
            {
                closeConnectionDB();
                return null; ;
            }
        }
        /*public Forum getForumByMember(string userName)
        {
            Forum forum = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  forums where users.userName='" + userName + "' and users.forumName=forums.forumName";
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
                    command2.CommandText = "SELECT  * FROM  forums where forumAdministartors.forumName='" + forumName + "'";
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
        }*/
        public bool addMessage(string sender, string reciver, string content)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO messages ([sender],[reciver],[content]) " +
                    "values (?,?,?)";
                command.Parameters.AddWithValue("sender", sender);
                command.Parameters.AddWithValue("reciver", reciver);
                command.Parameters.AddWithValue("content", content);
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
                command.CommandText = "SELECT  * FROM  friendOf where friendOf.userName='" + userName + "'";
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
        public bool banMember(string bannedMember, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "DELETE  FROM  members where userName='" + bannedMember +
                    "' and forumName='" + forumName + "'";
                command.ExecuteNonQuery();
                //member removed
                closeConnectionDB();
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool changePolicy(string newPolicy, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE forums SET forumPolicy='" + newPolicy + "' where forumName='" + forumName + "'";
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
        public bool nominateAdmin(string newAdmin, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO forumAdministrators ([forumName],[administratorName])" +
                        "values (?,?)";
                command.Parameters.AddWithValue("forumName", forumName);
                command.Parameters.AddWithValue("administratorName", newAdmin);
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
                command.CommandText = "SELECT  * FROM  superUsers where superUserName='" + userName + "'";
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
                    command2.CommandText = "SELECT  * FROM  users where userName='" + userName + "'";
                    OleDbDataReader reader2 = command2.ExecuteReader();
                    reader2.Read();
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
        }
        public bool dismissAdmin(string adminToDismissed, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "DELETE  FROM  forumAdministrators where forumName='"
                + forumName + "' and AdministratorName='" + adminToDismissed + "'";
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
                command2.CommandText = "SELECT  * FROM  users where userName='" + userName + "'";
                OleDbDataReader reader2 = command2.ExecuteReader();
                reader2.Read();
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
                command.CommandText = "INSERT INTO users ([userName],[password],[email]) " +
                        "values(?,?,?)";
                command.Parameters.AddWithValue("userName", userName);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("email", email);
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
        public Boolean addMemberToForum(string userName, string forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO members ([userName],[forumName]) " +
                        "values(?,?)";
                command.Parameters.AddWithValue("userName", userName);
                command.Parameters.AddWithValue("forumName", forumName);
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
        public List<string> getMembersOfForum(string forumName)
        {
            List<string> users = new List<string>();
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  members where forumName='" + forumName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(reader.GetString(0));
                }
                closeConnectionDB();
                return users;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        public List<string> getSimularForumsOf2users(string userName1, string userName2)
        {
            List<string> forums = new List<string>();
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  members AS m1,members AS m2 where m1.forumName=m2.forumName" +
                    " and m1.userName='" + userName1 + "' and m2.userName='" + userName2 + "'";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    forums.Add(reader.GetString(1));
                }
                closeConnectionDB();
                return forums;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        public Boolean createForum(string forumName, string description, string forumPolicy, string forumRules, List<string> administrators)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO forums ([forumName],[description],[forumPolicy],[forumRules]) " +
                        "VALUES (?,?,?,?)";
                command.Parameters.AddWithValue("forumName", forumName);
                command.Parameters.AddWithValue("description", description);
                command.Parameters.AddWithValue("forumPolicy", forumPolicy);
                command.Parameters.AddWithValue("forumRules", forumRules);
                command.ExecuteNonQuery();

                /* foreach (string admin in administrators)
                 {
                     OleDbCommand command2 = new OleDbCommand();
                     command2.Connection = connection;
                     command2.CommandText = "INSERT INTO forumAdministrators ([forumName], [administratorName]) " +
                             "VALUES (?,?)";
                     command2.Parameters.AddWithValue("forumName", forumName);
                     command2.Parameters.AddWithValue("administratorName", admin);
                     command2.ExecuteNonQuery();
                 }*/
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
                command.CommandText = "UPDATE forums SET forumPolicy='" + newForumPolicy + "', description = '" + newDescription + "', forumRules = '" + newForumRules + "' where forumName='" + forumName + "'";
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
                command.CommandText = "INSERT INTO friendOf ([userName],[friendName]) " +
                        "values (?,?)";
                command.Parameters.AddWithValue("userName", userName);
                command.Parameters.AddWithValue("friendName", friendToAddName);
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
                command.CommandText = "DELETE  from friendOf where userName='" + userName +
                    "' and friendName='" + deletedFriendName + "'";
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
                command.CommandText = "SELECT  * FROM  subForums where subForums.forumName='" + forumName + "' and " +
                    "subForums.subForumName = '" + subForumName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                    subForum = new SubForum(reader.GetString(0), reader.GetString(1));
                else
                {
                    return null;
                }
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "SELECT  * FROM  subForumModerators where subForumModerators.forumName='" + forumName + "' and " +
                    "subForumModerators.subForumName='" + subForumName + "'";

                OleDbDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    subForum.moderators.Add(reader2.GetString(2), DateTime.Parse(reader2.GetDateTime(3).ToString("dd MM yyyy")));
                }
                OleDbCommand command3 = new OleDbCommand();
                command3.Connection = connection;
                command3.CommandText = "SELECT  * FROM  threads where forumName='" + forumName + "' and " +
                    "subForumName='" + subForumName + "'";
                OleDbDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    subForum.threads.Add(reader3.GetInt32(0));
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
                command.CommandText = "SELECT  * FROM  messages";
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
        public Boolean addSubForum(String subForumName, String forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO subForums ([subForumName],[forumName]) " +
                        "values (?,?)";
                command.Parameters.AddWithValue("subForumName", subForumName);
                command.Parameters.AddWithValue("forumName", forumName);
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
                command.CommandText = "SELECT  * FROM  posts where postID=" + postId + "";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                post = new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")), reader.GetString(6));
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
            if (postId < 0)
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
                command.CommandText = "SELECT  * FROM  posts where parentPostID=" + postId + "";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    post = new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")), reader.GetString(6));
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
                command.CommandText = "SELECT  * FROM  threads where firstMessageId=" + id + "";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                String sfName = reader.GetString(1);
                String fName = reader.GetString(2);
                closeConnectionDB();
                return getSubForum(sfName, fName);
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
                command.CommandText = "SELECT  * FROM  posts where postID=" + postId + "";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                thread = new Thread(new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")), reader.GetString(6)));
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
                command.CommandText = "INSERT INTO threads ([firstMessageId],[subForumName],[forumName])" +
                        " values (?,?,?)";
                command.Parameters.AddWithValue("firstMessageId", firstMessageId);
                command.Parameters.AddWithValue("subForumName", subForumName);
                command.Parameters.AddWithValue("forumName", forumName);
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
        public bool addPost(String writerUserName, Int32 postID, String headLine, String content, Int32 parentId, DateTime timePublished, String forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO posts ([postID],[writerUserName],[title],[content],[parentPostID],[publishTime],[forumName])" +
                        " values (?,?,?,?,?,?,?)";
                command.Parameters.AddWithValue("postID", postID);
                command.Parameters.AddWithValue("writerUserName", writerUserName);
                command.Parameters.AddWithValue("title", headLine);
                command.Parameters.AddWithValue("content", content);
                command.Parameters.AddWithValue("parentPostID", parentId);
                command.Parameters.AddWithValue("publishTime", timePublished.Day + "/" + timePublished.Month + "/" + timePublished.Year);
                command.Parameters.AddWithValue("forumName", forumName);
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
        public int numOfPostInForum(String forumName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  Count(postID) FROM  posts where forumName='" + forumName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                closeConnectionDB();
                return reader.GetInt32(0);
            }
            catch
            {
                closeConnectionDB();
                return -1;
            }
        }
        public bool removeThread(int id)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "DELETE  FROM  threads where firstMessageId=" + id + "";
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
                command.CommandText = "DELETE  from posts where PostID=" + id + "";
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
        public Boolean updatePost(int postID, String title, String content)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE posts SET title='" + title + "' , content='" + content + "' where postID=" + postID + "";
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
        public List<Post> getMemberPosts(String memberName, String forumName)
        {
            List<Post> posts = new List<Post>();
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM  posts where forumName='" + forumName + "'" +
                    " and writerUserName='" + memberName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Post post = new Post(reader.GetString(1), reader.GetInt32(0), reader.GetString(2), reader.GetString(3), reader.GetInt32(4), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")), reader.GetString(6));
                    posts.Add(post);
                }
                closeConnectionDB();
                return posts;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        public List<String> getSuperUserReportOfMembers()
        {
            List<String> users = new List<String>();
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  (users.email,users.userName,members.forumName) FROM  users,members where users.username=members.userName" +
                    " ORDER BY users.email";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    String user = "email: " + reader.GetString(0) + " user Name: " + reader.GetString(1) + " in forum: " + reader.GetString(2);
                    users.Add(user);
                }
                closeConnectionDB();
                return users;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
        public void clear()
        {
            try
            {
                OpenConnectionDB();
                List<String> commands = new List<string>();
                commands.Add("DELETE  from members");
                commands.Add("DELETE  from forumadministrators");
                commands.Add("DELETE  from messages");
                commands.Add("DELETE  from friendOf");
                commands.Add("DELETE  from subForumModerators");
                commands.Add("DELETE  from threads");
                commands.Add("DELETE  from posts");
                commands.Add("DELETE  from subForums");
                commands.Add("DELETE  from superUsers");
                commands.Add("DELETE  from Users");
                commands.Add("DELETE  from forums");

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
