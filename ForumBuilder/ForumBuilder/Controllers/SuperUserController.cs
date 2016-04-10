using System;
using System.Collections.Generic;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    public class SuperUserController : UserController, ISuperUserController
    {
        private static SuperUserController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        public static SuperUserController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SuperUserController();
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
            if (demoDB.getSuperUser(superUserName) == null)
            {
                logger.logPrint("create forum fail " + superUserName + " is not super user");
                return false;
            }                
            else if (demoDB.createForum(forumName, descrption, forumPolicy, forumRules, administrators))
            {
                logger.logPrint("Forum " + forumName + " creation success");
                return true;
            }
            return false;
        }
    }
}
