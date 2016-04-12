using System;
using System.Collections.Generic;

namespace ForumBuilder.Controllers
{
    public interface IUserController
    {
        Boolean addFriend(String userName, String friendToAdd);
        Boolean deleteFriend(String userName, String deletedFriend);
        Boolean sendPrivateMessage(string fromUserName, string toUserName, string content);
        List<String> getFriendList(String userName);
    }
}
