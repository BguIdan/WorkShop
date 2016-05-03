using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    class UserManagerClient : ClientBase<IUserManager>, IUserManager
    {
        Boolean addFriend(String userName, String friendToAdd)
        {
            return Channel.addFriend(userName, friendToAdd);
        }

        Boolean deleteFriend(String userName, String deletedFriend)
        {
            return Channel.deleteFriend(userName, deletedFriend);
        }

        Boolean sendPrivateMessage(String fromUserName, String toUserName, String content)
        {
            return Channel.sendPrivateMessage(fromUserName, toUserName, content);
        }
    }
}
