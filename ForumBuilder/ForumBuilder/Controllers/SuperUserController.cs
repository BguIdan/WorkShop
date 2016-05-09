using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using BL_Back_End;

namespace ForumBuilder.Controllers
{
    public class SuperUserController : UserController, ISuperUserController
    {
        private static SuperUserController singleton;
        ForumController forumController = ForumController.getInstance;
        DBClass DB = DBClass.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        String loggedInSuperUser = "";


        public static SuperUserController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SuperUserController();
                    Systems.Logger.getInstance.logPrint("Super user contoller created");
                }
                return singleton;
            }
        }

        public Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName)
        {
            if (forumName.Equals("") || descrption.Equals("") || forumPolicy.Equals("") || forumRules.Equals("") || administrators == null)
            {
                logger.logPrint("cannot create new forum because one or more of the fields is empty");
                return false;
            }
            if (DB.getSuperUser(superUserName) == null)
            {
                logger.logPrint("create forum fail " + superUserName + " is not super user");
                return false;
            }                
            else if (DB.createForum(forumName, descrption, forumPolicy, forumRules, new List<string>()))
            {
                this.forumController.addForum(forumName);
                foreach(String admin in administrators)
                {
                    ForumController.getInstance.nominateAdmin(admin, superUserName,forumName);
                }
                logger.logPrint("Forum " + forumName + " creation success");
                return true;
            }
            return false;
        }

        public bool addSuperUser(string email, string password, string userName)
        {
            if (userName.Equals("") || password.Equals("") || email.Equals(""))
            {
                logger.logPrint("one or more of the fields is missing");
                return false;
            }
            // check if the password is strong enough
            bool isNumExist = false;
            bool isSmallKeyExist = false;
            bool isBigKeyExist = false;
            bool isKeyRepeting3Times = false;
            for (int i = 0; i < password.Length; i++)
            {
                if (password.ElementAt(i) <= '9' && password.ElementAt(i) >= '0')
                {
                    isNumExist = true;
                }
                if (password.ElementAt(i) <= 'Z' && password.ElementAt(i) >= 'A')
                {
                    isBigKeyExist = true;
                }
                if (password.ElementAt(i) <= 'z' && password.ElementAt(i) >= 'a')
                {
                    isSmallKeyExist = true;
                }
                if (i < password.Length - 2 && (password.ElementAt(i).Equals(password.ElementAt(i + 1)) && password.ElementAt(i).Equals(password.ElementAt(i + 2))))
                {
                    isKeyRepeting3Times = true;
                }
            }
            if (!(isNumExist && isSmallKeyExist && isBigKeyExist && !isKeyRepeting3Times))
            {
                logger.logPrint("password isnt strong enough");
                return false;
            }
            // check if the the email is in a correct format
            int index = email.IndexOf("@");
            if (index < 0 || index == email.Length - 1)
            {
                logger.logPrint("error in email format");
                return false;
            }
            return DB.addSuperUser(email, password, userName);
        }

        public Boolean isSuperUser(string user)
        {
            if (DB.getSuperUser(user) == null)
            {
                return false;
            }
            return true;
        }
        public bool addUser(string userName, string password, string mail, string superUserName)
        {
            if (!isSuperUser(superUserName))
            {
                return false;
            }
            if (userName.Length > 0 && password.Length > 0 && mail.Length > 0)
            {
                if (DB.getUser(userName) != null)
                {
                    logger.logPrint("Register user faild, " + userName + " is already taken");
                    return false;
                }
                return DB.addUser(userName, password, mail);
            }
            logger.logPrint("Register user faild, password not strong enough");
            return false;
        }
        public Boolean login(String user, String password, string email)
        {
            User superUser = DB.getSuperUser(user);
            if (superUser != null && superUser.password.Equals(password) && superUser.email.Equals(email))
            {
                loggedInSuperUser = user;
                return true;
            }
            else
            {
                logger.logPrint("could not login, wrong cerdintals");
                return false;
            }
        }
        public int SuperUserReportNumOfForums(string superUserName)
        {
            if (isSuperUser(superUserName))
                return DB.numOfForums();
            else
                return -1;
        }
        public List<String> getSuperUserReportOfMembers(string superUserName)
        {
            if (isSuperUser(superUserName))
                return DB.getSuperUserReportOfMembers();
            else
                return null;
        }
    }
}
