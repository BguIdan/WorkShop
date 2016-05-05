﻿using System;
using System.Collections.Generic;
using BL_Back_End;
using Database;
namespace ForumBuilder.Controllers
{
    public class ForumController : IForumController
    {
        private static ForumController singleton;
        DBClass DB = DBClass.getInstance;
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
            if (DB.getUser(userNameAdmin) != null)
            {
                if (!isAdmin(userNameAdmin, forumName))
                {
                    logger.logPrint("Add sub-forum failed, "+userNameAdmin+" is not an admin");
                    return false;
                }
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
                            DB.nominateModerator(s, date, name, forumName);
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
                    logger.logPrint("Register user faild, "+userName+" is already taken");
                    return false;
                }
                if (DB.addUser(userName, password, mail))
                {
                    DB.addMemberToForum(userName,forumName);
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
            else if (DB.setForumPreferences(forumName, newDescription, newForumPolicy, newForumRules)) {
                logger.logPrint(forumName + "preferences had changed successfully");
                hasSucceed = true;
            }
            return hasSucceed;
        }

        public String getForumPolicy(String forumName)
        {
            return DB.getforumByName(forumName).forumPolicy;
        }

        public String getForumDescription(String forumName)
        {
            return DB.getforumByName(forumName).description;
        }

        public String getForumRules(String forumName)
        {
            return DB.getforumByName(forumName).forumRules;
        }
        public Forum getForum(String forumName) 
        {
            return DB.getforumByName(forumName);
        }
    }
}
