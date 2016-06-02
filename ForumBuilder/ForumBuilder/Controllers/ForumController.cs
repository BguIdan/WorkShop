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
        Dictionary<String, IUserNotificationsService> channelsByLoggedInUsers= new Dictionary<String, IUserNotificationsService>();

        public static ForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ForumController();
                    Systems.Logger.getInstance.logPrint("Forum contoller created");
                }
                return singleton;
            }
        }

        public bool addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string creatorName)
        {
            if (DB.getSuperUser(creatorName)!=null||isAdmin(creatorName, forumName))
            {
                Forum forum = DB.getforumByName(forumName);
                if (forum != null)
                {
                    DB.addSubForum(name, forumName);
                    foreach (string s in moderators.Keys)
                    {
                        if (DB.getUser(s) == null)
                        {
                            logger.logPrint("Add sub-forum failed, the moderator " + s + " is not member of forum"); 
                            return false;
                        }
                    }
                    foreach (string s in moderators.Keys)
                    {
                        DateTime date;
                        moderators.TryGetValue(s, out date);
                        if (date > DateTime.Now)
                        {
                            DB.nominateModerator(s, date, name, forumName,creatorName);
                        }
                        else
                        {
                            logger.logPrint("Add sub-forum failed, date is allready passed"); 
                            return false;
                        }
                    }
                }
            }
            else
            {
                logger.logPrint("Add sub-forum failed, "+ creatorName + " is not allowed"); 
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
            if(DB.getSimularForumsOf2users(friendToAdd,userName)!=null&&
                DB.getSimularForumsOf2users(friendToAdd,userName).Count>0)  
            {
                return true;
            }
            return false;
        }

        public bool banMember(string bannedMember, string bannerUserName, string forumName)
        {
            if (!isMember(bannedMember, forumName))
            {
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a member");
                return false;
            }
            else if(!isAdmin(bannerUserName, forumName)&& DB.getSuperUser(bannerUserName)==null)
            {
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a admin or super user");
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
                logger.logPrint("Dismiss admin failed, " + adminToDismissed + " is not a admin");
                return false;
            }
            else if(DB.getSuperUser(dismissingUserName)==null)
            {
                logger.logPrint("Ban Member failed, " + dismissingUserName + " is not a super user");
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
            List<string> users= DB.getMembersOfForum(forumName);
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
                logger.logPrint("nominate admin fail, " + newAdmin + "is already admin");
                return false;
            }
            if (DB.getUser(newAdmin) == null)
                return false;
            if ((DB.getSuperUser(nominatorName) != null|| DB.getforumByName(forumName).administrators.Contains(nominatorName)))
            {
                bool isMem = isMember(newAdmin, forumName);
                if (!isMem)
                {
                    isMem=isMem|| DB.addMemberToForum(newAdmin, forumName);
                }
                if (isMem&& DB.nominateAdmin(newAdmin, forumName))
                {
                    logger.logPrint("admin nominated successfully");
                    if (DB.getforumByName(forumName).administrators.Contains(nominatorName))
                        DB.dismissAdmin(nominatorName, forumName);
                    return true;
                }
                return false;
            }
            logger.logPrint("nominate admin fail " + nominatorName + " is not super user");
            return false;
        }            
        
        public bool registerUser(string userName, string password, string mail, string forumName)
        {
            if (DB.getforumByName(forumName) == null)
            {
                logger.logPrint("Register user faild, the forum, "+ forumName+" does not exist");
                return false;
            }
            if (userName.Length > 0 && password.Length > 0 && mail.Length > 0)
            {
                User user = DB.getUser(userName);
                if (user !=null)
                {
                    if(user.userName.Equals(userName)&&user.password.Equals(password))
                    {
                        return DB.addMemberToForum(userName, forumName);
                    }
                    logger.logPrint("Register user failed, "+userName+" is already taken");
                    return false;
                }
                if (DB.addUser(userName, password, mail))
                {
                    DB.addMemberToForum(userName,forumName);
                    return true;
                }
                return false;
            }
            logger.logPrint("Register user failed, password not strong enough");
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

        public Boolean login(String user, String forumName, string pass)
        {
            if (!loggedInUsersByForum.ContainsKey(forumName))
            {
                loggedInUsersByForum.Add(forumName, new List<string>());
            }
            User usr = DB.getUser(user);
            Forum temp = DB.getforumByName(forumName);
            if (usr != null && usr.password.Equals(pass) &&  temp!= null)
            {
                this.loggedInUsersByForum[forumName].Add(user);
                this.channelsByLoggedInUsers[user] = OperationContext.Current.GetCallbackChannel<IUserNotificationsService>();
                return true;
            }
            else
            {
                logger.logPrint("could not login, wrong cerdintals");
                return false;
            }
        }

        public Boolean logout(String user, String forumName)
        {
            if (!this.loggedInUsersByForum.ContainsKey(forumName))
                return false;
            if (!this.loggedInUsersByForum[forumName].Contains(user))
                return false;
            this.loggedInUsersByForum[forumName].Remove(user);
            this.channelsByLoggedInUsers[user] = null;
            return true;
        }

        public Boolean sendThreadCreationNotification(String headLine, String content, String publisherName, String forumName, String subForumName)
        {
            if (loggedInUsersByForum == null)
                this.loggedInUsersByForum = new Dictionary<String,List<String>>();
            List<String> loggedInUsers = this.loggedInUsersByForum[forumName];
            if (loggedInUsers == null)
                return false;
            foreach (String userName in loggedInUsers)
            {
                if (channelsByLoggedInUsers[userName] != null)
                    channelsByLoggedInUsers[userName].applyPostPublishedInForumNotification(forumName, subForumName, publisherName);
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
                    channelsByLoggedInUsers[userName].applyPostModificationNotification(forumName, publisherName, title, content);
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
                    channelsByLoggedInUsers[userName].applyPostDelitionNotification(forumName, publisherName);
            }
            return true;
        }

        public Boolean setForumPreferences(String forumName, String newDescription, string policy, bool isQuestionIdentifying,
            int seniorityInForum, bool deletePostByModerator, int timeToPassExpiration, int minNumOfModerators,
            bool hasCapitalInPassword, bool hasNumberInPassword, int minLengthOfPassword, string setterUserName)
        {
            bool hasSucceed = false;
            if (DB.getforumByName(forumName) == null)
            {
                logger.logPrint("Set forum preferences failed, Forum" + forumName + " do not exist");
            }
            else if (!isAdmin(setterUserName, forumName))
            {
                logger.logPrint("Set forum preferences failed, " + setterUserName + " is not an admin");
            }
            else if (forumName==null|newDescription==null| setterUserName==null)
            {
                logger.logPrint("Set forum preferences failed, one or more of the arguments is null");
            }
            else if (DB.setForumPreferences(forumName, newDescription, policy, isQuestionIdentifying, seniorityInForum, deletePostByModerator,
                    timeToPassExpiration, minNumOfModerators, hasCapitalInPassword, hasNumberInPassword, minLengthOfPassword)) {
                logger.logPrint(forumName + "preferences had changed successfully");
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

        public int getAdminReportNumOfPOst(String AdminName,String forumName)
        {
            if(isAdmin(AdminName, forumName))
                return DB.numOfPostInForum(forumName);
            return -1;
        }
        public List<Post> getAdminReportPostOfmember(String AdminName, String forumName,String memberName)
        {
            if (isAdmin(AdminName, forumName))
                return DB.getMemberPosts(memberName,forumName);
            return null;
        }
        public List<String> getAdminReport(String AdminName, String forumName)
        {
            if (isAdmin(AdminName, forumName))
                return DB.getModertorsReport(forumName);
            return null;
            
        }
    }
}
