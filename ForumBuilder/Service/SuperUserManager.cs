using ForumBuilder.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName)
        {
            return superUserController.createForum(forumName, descrption, forumPolicy, forumRules, administrators, superUserName);
        }

        public static int Main(string[] args)
        {
            //ForumSystem.initialize("name", "pass", "email");
            return -1;
        }
    }
}
