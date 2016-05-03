using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    public class ForumManagerClient : ClientBase<IForumManager>, IForumManager
    {
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName)
        {
            return Channel.dismissAdmin(adminToDismissed, dismissingUserName, forumName);
        }

        Boolean banMember(String bannedMember, String bannerUserName, String forumName)
        {
            return Channel.banMember(bannedMember, bannerUserName, forumName);
        }

        Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName)
        {
            return Channel.nominateAdmin(newAdmin, nominatorName, forumName);
        }

        Boolean registerUser(String newUser, String password, String mail, string forumName)
        {
            return Channel.registerUser(newUser, password, mail, forumName);
        }

        Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin)
        {
            return Channel.addSubForum(forumName, name, moderators, userNameAdmin);
        }

        Boolean isAdmin(String userName, String forumName)
        {
            return Channel.isAdmin(userName, forumName);
        }

        Boolean isMember(String userName, String forumName)
        {
            return Channel.isMember(userName, forumName);
        }

        Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, String setterUserName)
        {
            return Channel.setForumPreferences(forumName, newDescription, newForumPolicy, newForumRules, setterUserName);
        }

        String getForumPolicy(String forumName)
        {
            return Channel.getForumPolicy(forumName);
        }

        String getForumDescription(String forumName)
        {
            return Channel.getForumDescription(forumName);
        }

        String getForumRules(String forumName)
        {
            return Channel.getForumRules(forumName);
        }
    }
}
