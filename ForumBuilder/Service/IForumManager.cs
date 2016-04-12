using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IForumManager
    {
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName);
        Boolean banMember(String bannedMember, String bannerUserName, String forumName);
        Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName);
        Boolean registerUser(String newUser, String password, String mail, string forumName);
        Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin);
        Boolean isAdmin(String userName, String forumName);
        Boolean isMember(String userName, String forumName);
        Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, String setterUserName);
        String getForumPolicy(String forumName);
        String getForumDescription(String forumName);
        String getForumRules(String forumName);

    }
}
