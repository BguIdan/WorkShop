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
        public ForumManagerClient()
        {
 
        }

        public Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName)
        {
            return Channel.dismissAdmin(adminToDismissed, dismissingUserName, forumName);
        }

        public Boolean banMember(String bannedMember, String bannerUserName, String forumName)
        {
            return Channel.banMember(bannedMember, bannerUserName, forumName);
        }

        public Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName)
        {
            return Channel.nominateAdmin(newAdmin, nominatorName, forumName);
        }

        public Boolean registerUser(String newUser, String password, String mail, string forumName)
        {
            return Channel.registerUser(newUser, password, mail, forumName);
        }

        public Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin)
        {
            return Channel.addSubForum(forumName, name, moderators, userNameAdmin);
        }

        public Boolean isAdmin(String userName, String forumName)
        {
            return Channel.isAdmin(userName, forumName);
        }

        public Boolean isMember(String userName, String forumName)
        {
            return Channel.isMember(userName, forumName);
        }

        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, String setterUserName)
        {
            return Channel.setForumPreferences(forumName, newDescription, newForumPolicy, newForumRules, setterUserName);
        }

        public String getForumPolicy(String forumName)
        {
            return Channel.getForumPolicy(forumName);
        }

        public String getForumDescription(String forumName)
        {
            return Channel.getForumDescription(forumName);
        }

        public String getForumRules(String forumName)
        {
            return Channel.getForumRules(forumName);
        }
    }
}
