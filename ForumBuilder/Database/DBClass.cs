using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using BL_Back_End;
using System.Data;
using DataBase;

namespace Database
{
    public class DBClass
    {
        private static List<int> avilabelPostIDs = new List<int>();
        private static int maxNotAvailable = -1;
        private static DBClass singleton;
        private List<Forum> forums = new List<Forum>();
        private List<SubForum> subForums =new List<SubForum>();
        Cache cache;
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
                int x = -1;
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  Max(postID) FROM  posts";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    x = reader.GetInt32(0);
                }
                closeConnectionDB();
                return x;
            }
            catch
            {
                closeConnectionDB();
                return -1;
            }
        }

        private Boolean initializeDB()
        {
            try
            {
                connection = new OleDbConnection();
                string s =System.IO.Directory.GetCurrentDirectory();
                s = s.Substring(0, s.IndexOf("ForumBuilder"))+ "forumDB.mdb; Persist Security Info = False;";
                s = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + s;
                connection.ConnectionString = @s;

                connection.Open();
                connection.Close();
                forums = getForumsForInit();
                subForums = getSubForumsForInit();
                cache= Cache.getInstance;
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
            return forums.Count;

            /*try
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
            }*/
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
                    foreach(SubForum sf in subForums)
                    {
                        if (sf.name.Equals(subForumName) && sf.forum.Equals(forumName))
                        {
                            sf.moderators.Remove(dismissedModerator);
                        }
                    }
                    return cache.dismissModerator(dismissedModerator, subForumName, forumName);
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
                password = enc(password);
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
                    command2.CommandText = "INSERT INTO users ([userName],[password],[email],[dateRegisterd],[ans1],[ans2]) VALUES (?,?,?,?,?,?)";
                    command2.Parameters.AddWithValue("userName", userName);
                    command2.Parameters.AddWithValue("password", password);
                    command2.Parameters.AddWithValue("email", email);
                    command2.Parameters.AddWithValue("dateRegisterd", DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
                    command2.Parameters.AddWithValue("ans1", "its me");
                    command2.Parameters.AddWithValue("ans2", "the super user");

                    command2.ExecuteNonQuery();
                    OleDbCommand command3 = new OleDbCommand();
                    command3.Connection = connection;
                    command3.CommandText = "INSERT INTO superUsers ([superUserName]) values (?)";
                    command3.Parameters.AddWithValue("superUserName", userName);
                    command3.ExecuteNonQuery();
                    //added
                    closeConnectionDB();
                    return cache.addSuperUser(email, password, userName);
                    //return true;
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
        public bool nominateModerator(string newModerator, DateTime endDate, string subForumName, string forumName, String nominator)
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
                command2.Parameters.AddWithValue("endTermOfOffice", endDate.Day + "/" + endDate.Month + "/" + endDate.Year);
                command2.Parameters.AddWithValue("nominator", nominator);
                command2.Parameters.AddWithValue("dateAdded", DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
                command2.ExecuteNonQuery();
                //added
                closeConnectionDB();
                foreach (SubForum sf in subForums)
                {
                    if (sf.name.Equals(subForumName) && sf.forum.Equals(forumName))
                    {
                        sf.moderators.Add(newModerator,new Moderator(newModerator, endDate,DateTime.Today,nominator));
                    }
                }
                //return true;
                return cache.nominateModerator(newModerator, endDate, subForumName, forumName, nominator);
            }
            catch
            {
                return false;
            }
        }
        public Forum getforumByName(string forumName)
        {
            foreach (Forum f in forums)
            {
                if (f.forumName.Equals(forumName))
                {
                    return f;
                }
            }
            return null;
            /*
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
                forum = new Forum(reader.GetString(0), reader.GetString(1), administrators);
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
            }*/
        }

        public List<string> getsubForumsNamesOfForum(string forumName)
        {
            foreach (Forum f in forums)
            {
                if (f.forumName.Equals(forumName))
                {
                    return f.subForums;
                }
            }
            return new List<String>();
            /*try
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
            }*/
        }

        public List<String> getForums()
        {
            List<string> fss = new List<string>();
            foreach(Forum fs in forums)
            {
                fss.Add(fs.forumName);
            }
            return fss;
            /*try
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
            }*/
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

        public List<string> getAnswers(string userName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  ans1 ans2 FROM  users where users.userName='" + userName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                List<String> answers = new List<String>();
                answers.Add(reader.GetString(4));
                answers.Add(reader.GetString(5));
                closeConnectionDB();
                return answers;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }

        public string getPassword(string userName)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  passwod FROM  users where users.userName='" + userName + "'";
                OleDbDataReader reader = command.ExecuteReader();
                string password = reader.GetString(1);
                closeConnectionDB();
                return password;
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
                foreach(Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.members.Remove(bannedMember);
                    }
                }
                //return true;
                return cache.banMember( bannedMember, forumName);
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public bool changePolicy(string forumName, string policy, bool isQuestionIdentifying, int seniorityInForum,
         bool deletePostByModerator, int timeToPassExpiration, int minNumOfModerators, bool hasCapitalInPassword,
         bool hasNumberInPassword, int minLengthOfPassword)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE policies SET "+
                    "policy='" + policy + "' ,"+
                    "isQuestionIdentifying=" + isQuestionIdentifying + " ," +
                    "seniorityInForum=" + seniorityInForum + " ," +
                    "deletePostByModerator=" + deletePostByModerator + " ," +
                    "timeToPassExpiration=" + timeToPassExpiration + " ," +
                    "minNumOfModerators=" + minNumOfModerators + " ," +
                    "hasCapitalInPassword=" + hasCapitalInPassword + " ," +
                    "hasNumberInPassword=" + hasNumberInPassword + " ," +
                    "minLengthOfPassword=" + minLengthOfPassword  +
                    " where forumName='" + forumName + "'";
                command.ExecuteNonQuery();
                closeConnectionDB();
                foreach (Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.forumPolicy.policy = policy;
                        f.forumPolicy.isQuestionIdentifying = isQuestionIdentifying;
                        f.forumPolicy.seniorityInForum = seniorityInForum;
                        f.forumPolicy.deletePostByModerator = deletePostByModerator;
                        f.forumPolicy.timeToPassExpiration = timeToPassExpiration;
                        f.forumPolicy.minNumOfModerators = minNumOfModerators;
                        f.forumPolicy.hasCapitalInPassword = hasCapitalInPassword;
                        f.forumPolicy.hasNumberInPassword = hasNumberInPassword;
                        f.forumPolicy.minLengthOfPassword = minLengthOfPassword;
                    }
                }
                //return true;
                return cache.changePolicy(forumName, policy, isQuestionIdentifying, seniorityInForum,
                    deletePostByModerator, timeToPassExpiration, minNumOfModerators, hasCapitalInPassword,
                    hasNumberInPassword, minLengthOfPassword);
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
                foreach (Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.administrators.Add(newAdmin);
                    }
                }
                //return true;
                return cache.nominateAdmin(newAdmin, forumName);
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
                    user = new User(reader2.GetString(0), dec(reader2.GetString(1)), reader2.GetString(2), DateTime.Parse(reader2.GetDateTime(3).ToString("dd MM yyyy")));
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
                foreach (Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.administrators.Remove(adminToDismissed);
                    }
                }
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
                user = new User(reader2.GetString(0), dec(reader2.GetString(1)), reader2.GetString(2), DateTime.Parse(reader2.GetDateTime(3).ToString("dd MM yyyy")));
                closeConnectionDB();
                return user;
            }
            catch
            {
                closeConnectionDB();
                return user;
            }
        }

        public Boolean addUser(string userName, string password, string email, string ans1, string ans2)
        {
            try
            {
                password = enc(password);
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO users ([userName],[password],[email],[dateRegisterd],[ans1],[ans2]) " +
                        "values(?,?,?,?,?,?)";
                command.Parameters.AddWithValue("userName", userName);
                command.Parameters.AddWithValue("password", password);
                command.Parameters.AddWithValue("email", email);
                command.Parameters.AddWithValue("dateRegisterd", DateTime.Today.Day + "/" + DateTime.Today.Month + "/" + DateTime.Today.Year);
                command.Parameters.AddWithValue("ans1", ans1);
                command.Parameters.AddWithValue("ans2", ans2);
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
                foreach (Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.members.Add(userName);
                    }
                }
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
            foreach (Forum f in forums)
            {
                if (f.forumName.Equals(forumName))
                {
                    return f.members;
                }
            }
            return new List<string>();
            /*List<string> users = new List<string>();
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
            }*/
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
        public Boolean createForum(string forumName, string description, ForumPolicy fp)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "INSERT INTO forums ([forumName],[description]) " +
                        "VALUES (?,?)";
                command.Parameters.AddWithValue("forumName", forumName);
                command.Parameters.AddWithValue("description", description);
                command.ExecuteNonQuery();

                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "INSERT INTO policies ([forumName],[policy],[isQuestionIdentifying],[seniorityInForum],"+
                    "[deletePostByModerator],[timeToPassExpiration],[minNumOfModerators],[hasCapitalInPassword],"+
                    "[hasNumberInPassword],[minLengthOfPassword]) " +
                        "VALUES (?,?,?,?,?,?,?,?,?,?)";
                command2.Parameters.AddWithValue("forumName", forumName);
                command2.Parameters.AddWithValue("policy", fp.policy);
                command2.Parameters.AddWithValue("isQuestionIdentifying", fp.isQuestionIdentifying);
                command2.Parameters.AddWithValue("seniorityInForum", fp.seniorityInForum);
                command2.Parameters.AddWithValue("deletePostByModerator", fp.deletePostByModerator);
                command2.Parameters.AddWithValue("timeToPassExpiration", fp.timeToPassExpiration);
                command2.Parameters.AddWithValue("minNumOfModerators", fp.minNumOfModerators);
                command2.Parameters.AddWithValue("hasCapitalInPassword", fp.hasCapitalInPassword);
                command2.Parameters.AddWithValue("hasNumberInPassword", fp.hasNumberInPassword);
                command2.Parameters.AddWithValue("minLengthOfPassword", fp.minLengthOfPassword);
                command2.ExecuteNonQuery();
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
                forums.Add(new Forum(forumName, description,fp,new List<string>()));
                return true;
            }
            catch
            {
                closeConnectionDB();
                return false;
            }
        }
        public Boolean setForumPreferences(String forumName, String newDescription, ForumPolicy fp)
        {
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "UPDATE forums SET description = '" + newDescription + "' where forumName='" + forumName + "'";
                command.ExecuteNonQuery();
                closeConnectionDB();
                changePolicy(forumName,fp.policy, fp.isQuestionIdentifying, fp.seniorityInForum, fp.deletePostByModerator,
                    fp.timeToPassExpiration, fp.minNumOfModerators, fp.hasCapitalInPassword, fp.hasNumberInPassword, fp.minLengthOfPassword);
                foreach (Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.description = newDescription;
                    }
                }
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
            foreach (SubForum sf in subForums)
            {

                if (sf.name.Equals(subForumName)&& sf.forum.Equals(forumName))
                {
                    return sf;
                }
            }
            return null;
            /*
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
            */
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
                SubForum sf = new SubForum(subForumName, forumName);
                subForums.Add(sf);
                foreach (Forum f in forums)
                {
                    if (f.forumName.Equals(forumName))
                    {
                        f.subForums.Add(subForumName);
                    }
                }
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
            foreach(SubForum sf in subForums)
            {
                if (sf.threads.Contains(id))
                {
                    return sf;
                }
            }
            return null;
            /*
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
            */
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
                foreach (SubForum sf in subForums)
                {
                    if (sf.name.Equals(subForumName)&&sf.forum.Equals(forumName))
                    {
                        sf.threads.Add(firstMessageId);
                    }
                }
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
                foreach (SubForum sf in subForums)
                {
                    if (sf.threads.Contains(id))
                    {
                        sf.threads.Remove(id);
                    }
                }
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
        private string enc(string password)
        {
            char[] passArray = password.ToArray();
            string res = "";
            for (int i = 0; i < passArray.Length; i++)
            {
                passArray[i] = (char)(((int)passArray[i]) + i%5 +1);
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
                passArray[i] = (char)(((int)passArray[i]) - i%5 -1);
            }
            for (int i = 0; i < passArray.Length; i++)
            {
                res = res + passArray[i];
            }
            return res;
        }
        public void clear()
        {
            try
            {
                OpenConnectionDB();
                List<String> commands = new List<string>();
                forums = new List<Forum>();
                subForums = new List<SubForum>();
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
                commands.Add("DELETE  from policies");
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
        private List<Forum> getForumsForInit()
        {
            try
            {
                OpenConnectionDB();
                List<Forum> forums1 = new List<Forum>();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  forumName FROM  forums";
                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count++;
                    Forum forum = getfbyN(reader.GetString(0));
                    forums1.Add(forum);

                    /*OpenConnectionDB();
                    reader = command.ExecuteReader();
                    for(int i=0; i<count; i++)
                    {
                        reader.Read();
                    }
                    */
                }
                closeConnectionDB();
                return forums;
            }
            catch(Exception e)
            {
                closeConnectionDB();
                return null; ;
            }

        }
        private Forum getfbyN(string name)
        {
            Forum forum = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                command2.CommandText = "SELECT  * FROM  forums where forums.forumName='" + name + "'";
                OleDbDataReader reader2 = command2.ExecuteReader();
                reader2.Read();
                OleDbCommand command3 = new OleDbCommand();
                command3.Connection = connection;
                command3.CommandText = "SELECT  * FROM  forumAdministrators where forumAdministrators.forumName='" + name + "'";
                OleDbDataReader reader3 = command3.ExecuteReader();
                List<String> administrators = new List<String>();
                while (reader3.Read())
                {
                    administrators.Add(reader3.GetString(1));
                }
                OleDbCommand command4 = new OleDbCommand();
                command4.Connection = connection;
                command4.CommandText = "SELECT  * FROM  policies where forumName='" + name + "'";
                OleDbDataReader reader4 = command4.ExecuteReader();
                reader4.Read();
                ForumPolicy policy = new ForumPolicy(reader4.GetString(1), reader4.GetBoolean(2), reader4.GetInt32(3),
                    reader4.GetBoolean(4), reader4.GetInt32(5), reader4.GetInt32(6), reader4.GetBoolean(7),
                    reader4.GetBoolean(8), reader4.GetInt32(9));
                forum = new Forum(reader2.GetString(0), reader2.GetString(1), policy, administrators);
                closeConnectionDB();
                List<String> members = getMembersOfForumOld(name);
                forum.members = members;
                List<String> subForums = getsubForumsNamesOfForumOld(name);
                forum.subForums = subForums;
                return forum;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }

        private List<string> getsubForumsNamesOfForumOld(string forumName)
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
            catch 
            {
                closeConnectionDB();
                return null;
            }
        }

        private List<string> getMembersOfForumOld(string forumName)
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

        private List<SubForum> getSubForumsForInit()
        {
            List<SubForum> sfs = new List<SubForum>();
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  subForumName,forumName FROM  subForums";
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sfs.Add(getSubforumOld(reader.GetString(0), reader.GetString(1)));
                }
                closeConnectionDB();
                return sfs;
            }
            catch 
            {
                closeConnectionDB();
                return null;
            }
        }

        private SubForum getSubforumOld(string subForumName, string forumName)
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
                    subForum.moderators.Add(reader2.GetString(2), getModertor(reader2.GetString(2)));
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

        private Moderator getModertor(string v)
        {
            Moderator mod = null;
            try
            {
                OpenConnectionDB();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "SELECT  * FROM subForumModerators where moderatorName='"+v+"'";
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    mod=new Moderator(reader.GetString(2), DateTime.Parse(reader.GetDateTime(3).ToString("dd MM yyyy")), DateTime.Parse(reader.GetDateTime(5).ToString("dd MM yyyy")), reader.GetString(4));
                }
                closeConnectionDB();
                return mod;
            }
            catch
            {
                closeConnectionDB();
                return null;
            }
        }
    }
}
