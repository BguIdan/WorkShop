using System;
using System.Collections.Generic;
using ForumBuilder.Users;

namespace ForumBuilder.Controllers
{
    class SuperUserController : UserController, ISuperUserController
    {
        private SuperUser _superUser;

        public Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName)
        {
            return true;
        }

        public Boolean nominateAdmin(String newAdminName, String forumName, String superUserName)
        {
            return true;
        }
        public Boolean deleteAdmin(String deletedAdminName, String forumName, String superUserName)
        {
            return true;
        }
    }
}
