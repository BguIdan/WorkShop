using System;
using System.Collections.Generic;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    class ForumController : IForumController
    {
        private static ForumController singleton;
        DemoDB demoDB = DemoDB.getInstance;

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
               
                if(!isAdnim(userNameAdmin, forumName)){
                    //maybe add error message to logger?! not an admin
                        return false;
                }
                Forum forum = demoDB.getForumByName(forumName);
                if (forum != null)
                {
                    forum.subForums.Add(name);
                    SubForum subForum = new SubForum(name, forumName);
                    demoDB.addSubForum(subForum);
                    
                    foreach(string s in moderators.Keys){
                        User mod= demoDB.getUser(s);
                        if (mod == null)
                        {
                            // maybe add error message to logger?! moderator not register to the forum
                            return false;
                        }
                    }
                    foreach(string s in moderators.Keys)
                    {
                        User mod = demoDB.getUser(s);
                        DateTime date;
                        moderators.TryGetValue(s, out date);
                        if (date > DateTime.Now) {
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

        public bool banMember(string bannedMember, string bannerUserName)
        {
            throw new NotImplementedException();
        }

        public bool changePoliciy(string newPolicy, string changerName)
        {
            throw new NotImplementedException();
        }

        public bool dismissAdmin(string adminToDismissed, string dismissingUserName)
        {
            throw new NotImplementedException();
        }

        public bool dismissMember(string userName, string dismissingUserName)
        {
            throw new NotImplementedException();
        }

        public bool nominateAdmin(string newAdmin, string nominatorName)
        {
            throw new NotImplementedException();
        }

        public bool registerUser(string userName, string password, string mail)
        {
            if (userName.Length > 0 && password.Length > 0 && mail.Length > 0)
            {
                User newUser = new User(userName, password, mail);
                if (!demoDB.addUser(newUser))
                    return false;
                return true;
            }
            else
            {
                // maybe add error message to logger?!
                return false;
            }
        }
    }
}
