using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;
using ForumBuilder.Services;
using ForumBuilder.Systems;

namespace ForumBuilder.Controllers
{
    class SuperUserController
    {
        private SuperUser _superUser;

        public Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators)
        {
            return _superUser._forumSystem.createForum(forumName, descrption, forumPolicy, forumRules, administrators);
        }

        public Boolean nominateAdmin(User newAdmin, Forum forum)
        {
            return true;
        }
        public Boolean deleteAdmin(User admin, Forum forum)
        {
            return true;
        }
    }
}
