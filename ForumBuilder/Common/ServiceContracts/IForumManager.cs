using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ForumBuilder.Common.ServiceContracts
{
    [ServiceContract]
    public interface IForumManager
    {
        [OperationContract]
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName);

        [OperationContract]
        Boolean banMember(String bannedMember, String bannerUserName, String forumName);

        [OperationContract]
        Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName);

        [OperationContract]
        Boolean registerUser(String newUser, String password, String mail, String forumName);

        [OperationContract]
        Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin);

        [OperationContract]
        Boolean isAdmin(String userName, String forumName);

        [OperationContract]
        Boolean isMember(String userName, String forumName);

        [OperationContract]
        Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, String setterUserName);

        [OperationContract]
        String getForumPolicy(String forumName);

        [OperationContract]
        String getForumDescription(String forumName);

        [OperationContract]
        String getForumRules(String forumName);

    }
}
