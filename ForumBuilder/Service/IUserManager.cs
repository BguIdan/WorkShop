using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserManager
    {
        Boolean addFriend(String userName, String friendToAdd);
        Boolean deleteFriend(String userName, String deletedFriend);
        Boolean sendPrivateMessage(String fromUserName, String toUserName, String content);
    }
}
