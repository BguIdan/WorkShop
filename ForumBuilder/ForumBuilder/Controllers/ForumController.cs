using System;
using System.Collections.Generic;
using BL_Back_End;
using Database;
using System.ServiceModel;
using ForumBuilder.Common.ClientServiceContracts;

namespace ForumBuilder.Controllers
{
    public class ForumController : IForumController
    {
        private static ForumController singleton;
        DBClass DB = DBClass.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        Dictionary<String, List<String>> loggedInUsersByForum = new Dictionary<String, List<String>>();
        Dictionary<String, List<IUserNotificationsService>> channelsByLoggedInUsers = new Dictionary<String, List<IUserNotificationsService>>();
        Dictionary<String, int> clientSessionKeyByUser = new Dictionary<String,int>();
        Dictionary<String, int> openConnectionsCounterByUser = new Dictionary<String,int>();

        public static ForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ForumController();
                    Systems.Logger.getInstance.logPrint("Forum contoller created", 0);
                    Systems.Logger.getInstance.logPrint("Forum contoller created", 1);
                }
                return singleton;
            }
        }

        public bool addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string creatorName)
        {
            if (DB.getSuperUser(creatorName) != null || isAdmin(creatorName, forumName))
            {
                Forum forum = DB.getforumByName(forumName);
                if (forum != null)
                {
                    DB.addSubForum(name, forumName);
                    if (forum.forumPolicy.minNumOfModerators > moderators.Count)
                    {
                        logger.logPrint("Add sub-forum failed, there is not enough moderators", 0);
                        logger.logPrint("Add sub-forum failed, there is not enough moderators", 2);
                        return false;
                    }
                    foreach (string s in moderators.Keys)
                    {
                        if (!isMember(s, forumName))
                        {
                            logger.logPrint("Add sub-forum failed, the moderator " + s + " is not member of forum", 0);
                            logger.logPrint("Add sub-forum failed, the moderator " + s + " is not member of forum", 2);
                            return false;
                        }
                        else if ((DateTime.Today - DB.getUser(s).date).Days < forum.forumPolicy.seniorityInForum)
                        {
                            logger.logPrint("Add sub-forum failed, the moderator " + s + " is not enough time in forum", 0);
                            logger.logPrint("Add sub-forum failed, the moderator " + s + " is not enough time in forum", 2);
                            return false;
                        }
                    }
                    foreach (string s in moderators.Keys)
                    {
                        DateTime date;
                        moderators.TryGetValue(s, out date);
                        if (date > DateTime.Now)
                        {
                            DB.nominateModerator(s, date, name, forumName, creatorName);
                        }
                        else
                        {
                            logger.logPrint("Add sub-forum failed, date is allready passed", 0);
                            logger.logPrint("Add sub-forum failed, date is allready passed", 2);
                            return false;
                        }
                    }
                }
            }
            else
            {
                logger.logPrint("Add sub-forum failed, " + creatorName + " is not allowed", 0);
                logger.logPrint("Add sub-forum failed, " + creatorName + " is not allowed", 2);
                return false;
            }
            return true;
        }
        public List<String> getForums()
        {
            return DB.getForums();
        }
        internal bool isMembersOfSameForum(string friendToAdd, string userName)
        {
            if (DB.getSimularForumsOf2users(friendToAdd, userName) != null &&
                DB.getSimularForumsOf2users(friendToAdd, userName).Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool banMember(string bannedMember, string bannerUserName, string forumName)
        {
            if (!isMember(bannedMember, forumName))
            {
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a member", 0);
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a member", 2);
                return false;
            }
            else if (!isAdmin(bannerUserName, forumName) && DB.getSuperUser(bannerUserName) == null)
            {
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a admin or super user", 0);
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a admin or super user", 2);
                return false;
            }
            else
            {
                return DB.banMember(bannedMember, forumName);
            }
        }

        public bool dismissAdmin(string adminToDismissed, string dismissingUserName, string forumName)
        {
            if (!isAdmin(adminToDismissed, forumName))
            {
                logger.logPrint("Dismiss admin failed, " + adminToDismissed + " is not a admin", 0);
                logger.logPrint("Dismiss admin failed, " + adminToDismissed + " is not a admin", 2);
                return false;
            }
            else if (DB.getSuperUser(dismissingUserName) == null)
            {
                logger.logPrint("Ban Member failed, " + dismissingUserName + " is not a super user", 0);
                logger.logPrint("Ban Member failed, " + dismissingUserName + " is not a super user", 2);
                return false;
            }
            else
            {
                return DB.dismissAdmin(adminToDismissed, forumName);
            }
        }

        public bool isAdmin(string userName, string forumName)
        {
            Forum forum = DB.getforumByName(forumName);
            if (forum == null)
                return false;
            foreach (string s in forum.administrators)
            {
                if (s.Equals(userName))
                {
                    return true;
                }
            }
            return false;
        }

        public bool isMember(string userName, string forumName)
        {
            Forum forum = DB.getforumByName(forumName);
            List<string> users = DB.getMembersOfForum(forumName);
            if (users == null)
            {
                return false;
            }
            if (users.Contains(userName))
            {
                return true;
            }
            return false;
        }

        public bool nominateAdmin(string newAdmin, string nominatorName, string forumName)
        {
            if (DB.getforumByName(forumName).administrators.Contains(newAdmin))
            {
                logger.logPrint("nominate admin fail, " + newAdmin + "is already admin", 0);
                logger.logPrint("nominate admin fail, " + newAdmin + "is already admin", 2);
                return false;
            }
            if (DB.getUser(newAdmin) == null)
                return false;
            if ((DB.getSuperUser(nominatorName) != null || DB.getforumByName(forumName).administrators.Contains(nominatorName)))
            {
                bool isMem = isMember(newAdmin, forumName);
                if (!isMem)
                {
                    isMem = isMem || DB.addMemberToForum(newAdmin, forumName);
                }
                if (isMem && DB.nominateAdmin(newAdmin, forumName))
                {
                    logger.logPrint("admin nominated successfully", 0);
                    logger.logPrint("admin nominated successfully", 1);
                    if (DB.getforumByName(forumName).administrators.Contains(nominatorName) && DB.getSuperUser(nominatorName) == null)
                        DB.dismissAdmin(nominatorName, forumName);
                    return true;
                }
                return false;
            }
            logger.logPrint("nominate admin fail " + nominatorName + " is not super user", 0);
            logger.logPrint("nominate admin fail " + nominatorName + " is not super user", 2);
            return false;
        }

        public bool registerUser(string userName, string password, string mail, string ans1, string ans2, string forumName)
        {
            Forum f = DB.getforumByName(forumName);
            if (f == null)
            {
                logger.logPrint("Register user faild, the forum, " + forumName + " does not exist", 0);
                logger.logPrint("Register user faild, the forum, " + forumName + " does not exist", 2);
                return false;
            }
            if (userName.Length > 0 && f.forumPolicy.minLengthOfPassword < password.Length && mail.Length > 0 &&
                (!f.forumPolicy.hasCapitalInPassword || (f.forumPolicy.hasCapitalInPassword && hasCapital(password))) &&
                (!f.forumPolicy.hasNumberInPassword || (f.forumPolicy.hasNumberInPassword && hasNumber(password))) &&
                (ans1 != null && ans2 != null)&&
                ((!f.forumPolicy.isQuestionIdentifying&&ans1.Equals("")&& ans2.Equals("")) || 
                (f.forumPolicy.isQuestionIdentifying && !ans1.Equals("") && !ans2.Equals(""))))
            {
                User user = DB.getUser(userName);
                /*
                if (user !=null)
                (!f.forumPolicy.hasNumberInPassword || (f.forumPolicy.hasNumberInPassword && hasNumber(password))))
            {
                User user = DB.getUser(userName);
                if (user != null)

                {
                    if (user.userName.Equals(userName) && user.password.Equals(password))
                    {
                        return DB.addMemberToForum(userName, forumName);
                    }
                    logger.logPrint("Register user failed, " + userName + " is already taken", 0);
                    logger.logPrint("Register user failed, " + userName + " is already taken", 2);
                    return false;
                }*/
                if (DB.addUser(userName, password, mail, ans1, ans2))
                {
                    DB.addMemberToForum(userName, forumName);
                    return true;
                }
                return false;
            }
            logger.logPrint("Register user failed, password not strong enough", 0);
            logger.logPrint("Register user failed, password not strong enough", 2);
            return false;
        }

        private bool hasNumber(string password)
        {
            char[] array = password.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] - '0' >= 0 && array[i] - '9' <= 0)
                    return true;
            }
            return false;
        }

        private bool hasCapital(string password)
        {
            char[] array = password.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] - 'A' >= 0 && array[i] - 'Z' <= 0)
                    return true;
            }
            return false;
        }

        public Boolean addForum(String forumName)
        {
            if (!this.loggedInUsersByForum.ContainsKey(forumName))
            {
                this.loggedInUsersByForum.Add(forumName, new List<String>());
                return true;
            }
            else
                return false;

        }

        public int login(String user, String forumName, string pass)
        {
            if (!loggedInUsersByForum.ContainsKey(forumName))
            {
                loggedInUsersByForum.Add(forumName, new List<string>());
            }
            User usr = DB.getUser(user);
            Forum loggedInForum = DB.getforumByName(forumName);
            if (usr != null && usr.password.Equals(pass) && loggedInForum != null)
            {
                if (needToChangePassword(user , forumName))
                    return -5;
                if (!this.loggedInUsersByForum[forumName].Contains(user))
                {
                    this.loggedInUsersByForum[forumName].Add(user);
                    this.channelsByLoggedInUsers[user] = new List<IUserNotificationsService>();
                    this.openConnectionsCounterByUser[user] = 1;
                }
                else
                {
                    return -3;//the error code for a login while a session key exists
                    //one the user is logged in with one or more client the future logins should be made only by the session key
                    //requirement 1-d in assignment 4 version 3 document
                }
                this.channelsByLoggedInUsers[user].Add(OperationContext.Current.GetCallbackChannel<IUserNotificationsService>());
                int sessionKey = generateRandomSessionKey();
                this.clientSessionKeyByUser[user] = sessionKey;
                return sessionKey;
            }
            else
            {//TODO apply error codes
                logger.logPrint("could not login, wrong credentials", 0);
                logger.logPrint("could not login, wrong credentials", 2);
                return -1;//TODO gal client session -1 means the login failed
            }
        }

        private bool needToChangePassword(string userName, string forumName)
        {
            Forum forum = DB.getforumByName(forumName);
            int time = forum.forumPolicy.timeToPassExpiration;
            User user = DB.getUser(userName);
            if ((DateTime.Today - user.lastTimeUpdatePassword).Days > time)
                return true;
            return false;
        }
        public String loginBySessionKey(int sessionKey, String user, String forumName)
        {
            User usr = DB.getUser(user);
            Forum loggedInForum = DB.getforumByName(forumName);
            if (usr != null && loggedInForum != null)
            {
                if (!loggedInUsersByForum.ContainsKey(forumName) || !this.loggedInUsersByForum[forumName].Contains(user))
                {
                    logger.logPrint("login error, the user was not logged in when using session key", 0);
                    logger.logPrint("login error, the user was not logged in when using session key", 2);
                    return "invalid session key: you logged out hence this session key is invalid";
                }
                if (sessionKey != this.clientSessionKeyByUser[user])
                {
                    logger.logPrint("login error, invalid session key", 0);
                    logger.logPrint("login error, invalid session key", 2);
                    return "invalid session key";
                }
                this.channelsByLoggedInUsers[user].Add(OperationContext.Current.GetCallbackChannel<IUserNotificationsService>());
                this.openConnectionsCounterByUser[user]++;
                return "success";
            }
            else
            {//TODO apply error codes
                logger.logPrint("could not login, wrong user name", 0);
                logger.logPrint("could not login, wrong user name", 2);
                return "wrong user name";
            }


        }

        public Boolean logout(String user, String forumName)
        {//TODO gal modify logout to support more than one active connection
            //TODO gal make sure the log outs works
            //TODO gal what about the open channels?
            if (!this.loggedInUsersByForum.ContainsKey(forumName))
                return false;
            if (!this.loggedInUsersByForum[forumName].Contains(user))
                return false;
            if (this.openConnectionsCounterByUser[user] == 1)
            {//last open connection, session key will be discarded
                this.loggedInUsersByForum[forumName].Remove(user);
                this.channelsByLoggedInUsers[user].Clear();
            }
            else
            {
                this.openConnectionsCounterByUser[user]--;
            }
            return true;
        }

        public Boolean sendThreadCreationNotification(String headLine, String content, String publisherName, String forumName, String subForumName)
        {
            if (loggedInUsersByForum == null)
                this.loggedInUsersByForum = new Dictionary<String, List<String>>();
            List<String> loggedInUsers = this.loggedInUsersByForum[forumName];
            if (loggedInUsers == null)
                return false;
            foreach (String userName in loggedInUsers)
            {
                if (channelsByLoggedInUsers[userName] != null)
                {
                    foreach (IUserNotificationsService channel in channelsByLoggedInUsers[userName])
                    {
                        channel.applyPostPublishedInForumNotification(forumName, subForumName, publisherName);
                    }
                }
            }
            return true;
        }

        public Boolean sendPostModificationNotification(String forumName, String publisherName, String title, String content)
        {
            if (loggedInUsersByForum == null)
                this.loggedInUsersByForum = new Dictionary<String, List<String>>();
            List<String> loggedInUsers = this.loggedInUsersByForum[forumName];
            if (loggedInUsers == null)
                return false;
            foreach (String userName in loggedInUsers)
            {
                if (channelsByLoggedInUsers[userName] != null)
                {
                    foreach (IUserNotificationsService channel in channelsByLoggedInUsers[userName])
                    {
                        channel.applyPostModificationNotification(forumName, publisherName, title, content);
                    }
                }
            }
            return true;
        }

        public Boolean sendPostDelitionNotification(String forumName, String publisherName)
        {
            if (loggedInUsersByForum == null)
                this.loggedInUsersByForum = new Dictionary<String, List<String>>();
            List<String> loggedInUsers = this.loggedInUsersByForum[forumName];
            if (loggedInUsers == null)
                return false;
            foreach (String userName in loggedInUsers)
            {
                if (channelsByLoggedInUsers[userName] != null)
                {
                    foreach (IUserNotificationsService channel in channelsByLoggedInUsers[userName])
                    {
                        channel.applyPostDelitionNotification(forumName, publisherName);
                    }
                }
            }
            return true;
        }

        public Boolean setForumPreferences(String forumName, String newDescription, ForumPolicy fp, string setterUserName)
        {
            bool hasSucceed = false;
            if (DB.getforumByName(forumName) == null)
            {
                logger.logPrint("Set forum preferences failed, Forum" + forumName + " do not exist", 0);
                logger.logPrint("Set forum preferences failed, Forum" + forumName + " do not exist", 2);
            }
            else if (!isAdmin(setterUserName, forumName))
            {
                logger.logPrint("Set forum preferences failed, " + setterUserName + " is not an admin", 0);
                logger.logPrint("Set forum preferences failed, " + setterUserName + " is not an admin", 2);
            }
            else if (forumName == null | newDescription == null | setterUserName == null)
            {
                logger.logPrint("Set forum preferences failed, one or more of the arguments is null", 0);
                logger.logPrint("Set forum preferences failed, one or more of the arguments is null", 2);
            }
            else if (DB.setForumPreferences(forumName, newDescription, fp))
            {
                logger.logPrint(forumName + "preferences had changed successfully", 0);
                logger.logPrint(forumName + "preferences had changed successfully", 1);
                hasSucceed = true;
            }
            return hasSucceed;
        }

        public String getForumPolicy(String forumName)
        {
            return DB.getforumByName(forumName).forumPolicy.policy;
        }

        public String getForumDescription(String forumName)
        {
            return DB.getforumByName(forumName).description;
        }

        public Forum getForum(String forumName)
        {
            return DB.getforumByName(forumName);
        }

        public int getAdminReportNumOfPOst(String AdminName, String forumName)
        {
            if (isAdmin(AdminName, forumName))
                return DB.numOfPostInForum(forumName);
            return -1;
        }
        public List<Post> getAdminReportPostOfmember(String AdminName, String forumName, String memberName)
        {
            if (isAdmin(AdminName, forumName))
                return DB.getMemberPosts(memberName, forumName);
            return null;
        }
        public List<String> getAdminReport(String AdminName, String forumName)
        {
            if (isAdmin(AdminName, forumName))
                return DB.getModertorsReport(forumName);
            return null;

        }


        private int generateRandomSessionKey()
        {
            Random random = new Random();
            int result = random.Next(0, 100000000);
            while (this.clientSessionKeyByUser.ContainsValue(result))
            {
                result = random.Next(1, 100000000);
            }
            return result;
        }


        public void notifyUserOnNewPrivateMessage(String forumName, String sender, String addressee, String content)
        {
            List<String> currentlyLoggedInUsers = this.loggedInUsersByForum[forumName];
            if (currentlyLoggedInUsers == null || !currentlyLoggedInUsers.Contains(addressee))
                return;
            List<IUserNotificationsService> addresseeChannels = this.channelsByLoggedInUsers[addressee];
            if (addresseeChannels != null)
            {
                foreach(IUserNotificationsService channel in addresseeChannels)
                {
                    channel.sendUserMessage(sender, content);
                }
            }
        }
    }
}
