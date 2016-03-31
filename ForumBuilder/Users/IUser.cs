﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users
{
    public interface IUser
    {
        String getUserName();
        Boolean addFriend(IUser newFriend);
        Boolean deleteFriend(IUser deletedFriend);
        Boolean notifyUserViaMail();
        Boolean sendPrivateMessage(String userName, String content);
        Boolean banMember(String userName, String forumName);
        Boolean createSubForum(String subForumName, IForum forum, List<String> moderators);
        change

    }
}
