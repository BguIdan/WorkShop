using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    class SuperUserManagerClient : ClientBase<ISuperUserManager>, ISuperUserManager
    {
        public Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName)
        {
            return Channel.createForum(forumName, descrption, forumPolicy, forumRules, administrators, superUserName);
        }

        public Boolean initialize(String name, String password, String email)
        {
            return Channel.initialize(name, password, email);
        }
    }
}
