using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ForumBuilder.Common.ServiceContracts
{
    [ServiceContract]
    public interface ISuperUserManager
    {
        [OperationContract]
        Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName);

        [OperationContract]
        Boolean initialize(String name, String password, String email);

        [OperationContract]
        Boolean login(String newUser, String forumName, string email);

        [OperationContract]
        Boolean isSuperUser(String user);

        [OperationContract]
        int SuperUserReportNumOfForums(string superUserName);

        [OperationContract]
        List<String> getSuperUserReportOfMembers(string superUserName);

        [OperationContract]
        Boolean addUser(string userName, string password, string mail, string superUserName);
    }
}
