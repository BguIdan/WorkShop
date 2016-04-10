﻿using System;
using System.Collections.Generic;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    public class SuperUserController : UserController, ISuperUserController
    {
        private static SuperUserController singleton;
        

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
            if (DemoDB.getInstance.isSuperUser(superUserName))
            {
                return DemoDB.getInstance.createForum(forumName, descrption, forumPolicy, forumRules, administrators);
            }
            return false;
        }
    }
}
