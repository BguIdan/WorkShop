using ForumBuilder.Controllers;
using ForumBuilder.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_Back_End;
using ForumBuilder.Common.ServiceContracts;

namespace Service
{
    public class SuperUserManager :ISuperUserManager
    {
        private static SuperUserManager singleton;
        private ISuperUserController superUserController;

        private SuperUserManager()
        {
            superUserController = SuperUserController.getInstance;
        }

        public static SuperUserManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SuperUserManager();
                }
                return singleton;
            }
        }

        public Boolean createForum(String forumName, String descrption, string policy, bool isQuestionIdentifying,
            int seniorityInForum, bool deletePostByModerator, int timeToPassExpiration, int minNumOfModerators,
            bool hasCapitalInPassword, bool hasNumberInPassword, int minLengthOfPassword, List<String> administrators,
            String superUserName)
        {
            return superUserController.createForum(forumName, descrption, new ForumPolicy(policy, isQuestionIdentifying, seniorityInForum, deletePostByModerator,
                    timeToPassExpiration, minNumOfModerators, hasCapitalInPassword, hasNumberInPassword, minLengthOfPassword), administrators, superUserName);
        }

        public Boolean initialize(String name, String password, String email)
        {
            ForumSystem.initialize(name, password, email);
            return true;
        }
        public Boolean login(String user, String forumName, string email)
        {
            return superUserController.login(user, forumName, email);
        }

        public Boolean isSuperUser(string userName)
        {
            return superUserController.isSuperUser(userName);
        }
        public int SuperUserReportNumOfForums(string superUserName)
        {
            return superUserController.SuperUserReportNumOfForums(superUserName);
        }
        public List<String> getSuperUserReportOfMembers(string superUserName)
        {
            return superUserController.getSuperUserReportOfMembers(superUserName);
        }

        public Boolean addUser(string userName, string password, string mail, string superUserName)
        {
            return superUserController.addUser(userName,  password,  mail,  superUserName);
        }
    }
}
