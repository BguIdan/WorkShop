using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.User
{
    class SuperUser : ISuperUser
    {
        public SuperUser()
        {

        }

        Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators)
        {
            return true;
        }
    }
}
