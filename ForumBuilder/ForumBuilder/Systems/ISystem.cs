using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.Systems
{
    public interface ISystem
    {
        Boolean initialize(String userName, String password, String email);
        //Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators);
    }
}
