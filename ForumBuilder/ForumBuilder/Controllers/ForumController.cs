using System;
using System.Collections.Generic;
using ForumBuilder.BL_Back_End;
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
                }
                return singleton;
            }
        }

        public bool addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string userNameAdmin)
        {
            User user = demoDB.getUser(userNameAdmin);
            if (user != null)
            {

                if (!isAdmin(userNameAdmin, forumName))
                {
                    //maybe add error message to logger?! not an admin
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
                        User mod = demoDB.getUser(s);
                        if (mod == null)
                        {
                            // maybe add error message to logger?! moderator not register to the forum
                            return false;
                        }
                    }
                    foreach (string s in moderators.Keys)
                    {
                        User mod = demoDB.getUser(s);
                        DateTime date;
                        moderators.TryGetValue(s, out date);
                        if (date > DateTime.Now)
                        {
                            subForum.moderators.Add(s, date);
                        }
                        else
                        {
                            // maybe add error message to logger?! date is allready passe
                            return false;
                        }

                    }
                }
            }
            else
            {
                // maybe add error message to logger?! not an admin
                return false;
            }

            return true;
        }

        public bool banMember(string bannedMember, string bannerUserName, string forumName)
        {
            if (this.isMember(bannerUserName, forumName) && this.isMember(bannerUserName, forumName))
            {
                return demoDB.banMember(bannedMember, bannerUserName, forumName);
            }
            return false;
        }

        public bool dismissAdmin(string adminToDismissed, string dismissingUserName, string forumName)
        {
            if (this.isAdmin(dismissingUserName, forumName) && this.isMember(adminToDismissed, forumName))
            {
                return demoDB.dismissAdmin(adminToDismissed, forumName);
            }
            return false;
        }

        public bool isAdmin(string userName, string forumName)
        {
            Forum forum = demoDB.getforumByName(forumName);
            foreach (string s in forum.administrators)
            {
                if (s.Equals(userName))
                {
                    return true;
                }
            }
            logger.logPrint("User " +userName+ "is not administrator in "+ forumName);      
            return false;
        }

        public bool isMember(string userName, string forumName)
        {
            Forum forum = demoDB.getforumByName(forumName);
            foreach (string s in forum.members)
            {
                if (s.Equals(userName))
                {
                    return true;
                }
            }
            logger.logPrint("User " +userName+ "is not member in "+ forumName);      
            return false;
        }

        public bool nominateAdmin(string newAdmin, string nominatorName, string forumName)
        {
            if (demoDB.getSuperUser(nominatorName)!=null)
            {
                if (this.isMember(newAdmin, forumName))
                {
                    if (demoDB.nominateAdmin(newAdmin, nominatorName, forumName))
                    {
                        logger.logPrint("admin nominated successfully");
                        return true;
                    }
                }
                logger.logPrint("nominate admin fail, "+newAdmin + "is not member");
                return false;
            }
            else
            {
                logger.logPrint("nominate admin fail " + nominatorName + " is not super user");
                return false;
            }
            
        }

        public bool registerUser(string userName, string password, string mail)
        {
            if (userName.Length > 0 && password.Length > 0 && mail.Length > 0)
            {
                if(demoDB.getUser(userName) !=null)
                {
                    return false;
                }
                User newUser = new User(userName, password, mail);
                if (demoDB.addUser(newUser))
                {
                    return true;
                }
                return false;
            }
            else
            {
                // maybe add error message to logger?!
                return false;
            }
        }

        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, string setterUserName)
        {
            bool hasSucceed = false;
            if (demoDB.getforumByName(forumName) != null)
            {
                logger.logPrint("Forum" + forumName + "do not exist");
            }
            else if (!isAdmin(setterUserName, forumName))
            {
                logger.logPrint("Set forum preferences failed, " + setterUserName + " is not an admin");
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
