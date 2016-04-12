using System;
using System.Collections.Generic;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    public class ForumController : IForumController
    {
        private static ForumController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
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

        public bool addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string userNameAdmin)
        {
            if (demoDB.getUser(userNameAdmin) != null)
            {
                if (!isAdmin(userNameAdmin, forumName))
                {
                    logger.logPrint("Add sub-forum failed, "+userNameAdmin+" is not an admin");
                    return false;
                }
                Forum forum = demoDB.getforumByName(forumName);
                if (forum != null)
                {
                    forum.subForums.Add(name);
                    SubForum subForum = new SubForum(name, forumName);
                    demoDB.addSubForum(subForum);
                    foreach (string s in moderators.Keys)
                    {
                        if (demoDB.getUser(s) == null)
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
                            subForum.moderators.Add(s, date);
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
                logger.logPrint("Add sub-forum failed, "+ userNameAdmin + " is not a user"); 
                return false;
            }
            return true;
        }

        internal bool isMembersOfSameForum(string friendToAdd, string userName)
        {
            if(demoDB.getForumByMember(friendToAdd)!=null&& demoDB.getForumByMember(userName) != null 
                && demoDB.getForumByMember(friendToAdd).forumName.Equals(demoDB.getForumByMember(userName).forumName))
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
            else if(!isAdmin(bannerUserName, forumName)&& demoDB.getSuperUser(bannerUserName)==null)
            {
                logger.logPrint("Ban Member failed, " + bannedMember + " is not a admin or super user");
                return false;
            }
            else 
            {
                return demoDB.banMember(bannedMember, bannerUserName, forumName);
            }
        }

        public bool dismissAdmin(string adminToDismissed, string dismissingUserName, string forumName)
        {
            if (!isAdmin(adminToDismissed, forumName))
            {
                logger.logPrint("Dismiss admin failed, " + adminToDismissed + " is not a admin");
                return false;
            }
            else if(demoDB.getSuperUser(dismissingUserName)==null)
            {
                logger.logPrint("Ban Member failed, " + dismissingUserName + " is not a super user");
                return false;
            }
            else 
            {
                return demoDB.dismissAdmin(adminToDismissed, forumName);
            }           
        }

        public bool isAdmin(string userName, string forumName)
        {
            Forum forum = demoDB.getforumByName(forumName);
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
            Forum forum = demoDB.getforumByName(forumName);
            if (forum.members.Contains(userName))
            {
                return true;
            }
            return false;
        }

        public bool nominateAdmin(string newAdmin, string nominatorName, string forumName)
        {
            if (demoDB.getforumByName(forumName).administrators.Contains(newAdmin))
            {
                logger.logPrint("nominate admin fail, " + newAdmin + "is already admin");
                return false;
            }
            if (demoDB.getSuperUser(nominatorName) != null|| demoDB.getforumByName(forumName).administrators.Contains(nominatorName))
            {
                if (this.isMember(newAdmin, forumName))
                {
                    if (demoDB.nominateAdmin(newAdmin, nominatorName, forumName))
                    {
                        logger.logPrint("admin nominated successfully");
                        if (demoDB.getforumByName(forumName).administrators.Contains(nominatorName))
                            demoDB.dismissAdmin(nominatorName, forumName);
                        return true;
                    }
                    return false;
                }
                logger.logPrint("nominate admin fail, " + newAdmin + "is not member");
                return false;
            }
            logger.logPrint("nominate admin fail " + nominatorName + " is not super user");
            return false;
        }            
        
        public bool registerUser(string userName, string password, string mail, string forumName)
        {
            Forum forum = demoDB.getforumByName(forumName);
            if (forum == null)
            {
                logger.logPrint("Register user faild, the forum, "+ forumName+" does not exist");
                return false;
            }
            if (userName.Length > 0 && password.Length > 0 && mail.Length > 0)
            {
                if(demoDB.getUser(userName) !=null)
                {
                    logger.logPrint("Register user faild, "+userName+" is already taken");
                    return false;
                }
                if (demoDB.addUser(userName, password, mail))
                {
                    forum.members.Add(userName);
                    return true;
                }
                return false;
            }
            logger.logPrint("Register user faild, password not strong enough");
            return false;
        }

        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, string setterUserName)
        {
            bool hasSucceed = false;
            if (demoDB.getforumByName(forumName) == null)
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
            else if (demoDB.setForumPreferences(forumName, newDescription, newForumPolicy, newForumRules)) {
                logger.logPrint(forumName + "preferences had changed successfully");
                hasSucceed = true;
            }
            return hasSucceed;
        }

        public String getForumPolicy(String forumName)
        {
            return demoDB.getforumByName(forumName).forumPolicy;
        }

        public String getForumDescription(String forumName)
        {
            return demoDB.getforumByName(forumName).description;
        }

        public String getForumRules(String forumName)
        {
            return demoDB.getforumByName(forumName).forumRules;
        }
    }
}
