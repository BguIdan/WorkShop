using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.DataContracts;
using ForumBuilder.Common.ClientServiceContracts;

namespace ForumBuilder.Common.ServiceContracts
{
    [ServiceContract(CallbackContract = typeof(IUserNotificationsService))]
    public interface IForumManager
    {
        [OperationContract]
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName);

        [OperationContract]
        Boolean banMember(String bannedMember, String bannerUserName, String forumName);

        [OperationContract]
        Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName);

        [OperationContract]
        Boolean registerUser(String newUser, String password, String mail, String ans1, String ans2, String forumName);

        [OperationContract]
        Boolean login(String newUser, String forumName,string password);

        [OperationContract]
        Boolean logout(String newUser, String forumName);

        [OperationContract]
        Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin);

        [OperationContract]
        Boolean isAdmin(String userName, String forumName);

        [OperationContract]
        Boolean isMember(String userName, String forumName);

        [OperationContract]
        Boolean setForumPreferences(String forumName, String newDescription, ForumPolicyData fpd, String setterUserName);

        [OperationContract]
        String getForumPolicy(String forumName);

        [OperationContract]
        String getForumDescription(String forumName);

        [OperationContract]
        ForumData getForum(String forumName);

        [OperationContract]
        List<String> getForums();

        [OperationContract]
        int getAdminReportNumOfPOst(String AdminName, String forumName);

        [OperationContract]
        List<PostData> getAdminReportPostOfmember(String AdminName, String forumName, String memberName);

        [OperationContract]
        List<String> getAdminReport(String AdminName, String forumName);
    }
}
