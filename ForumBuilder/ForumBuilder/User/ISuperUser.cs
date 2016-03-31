using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.User
{
    interface ISuperUser : IUser
    {
        Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators);
    }
}
