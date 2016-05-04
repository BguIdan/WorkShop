using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    public class UserManagerClient : ClientBase<IUserManager>, IUserManager
    {
        public UserManagerClient()
        {

        }

        public Boolean addFriend(String userName, String friendToAdd)
        {
            return Channel.addFriend(userName, friendToAdd);
        }

        public Boolean deleteFriend(String userName, String deletedFriend)
        {
            return Channel.deleteFriend(userName, deletedFriend);
        }

        public Boolean sendPrivateMessage(String fromUserName, String toUserName, String content)
        {
            return Channel.sendPrivateMessage(fromUserName, toUserName, content);
        }

        public List<String> getFriendList(String userName)
        {
            return Channel.getFriendList(userName);
        }

    }
}
