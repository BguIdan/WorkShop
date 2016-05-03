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

    }
}
