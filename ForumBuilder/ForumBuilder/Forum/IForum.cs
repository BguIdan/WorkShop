﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.User;


namespace ForumBuilder.Forum
{
    public interface IForum
    {
        Boolean dismissAdmin(IUser userismissedAdmin);
        Boolean banMember(IUser bannedMember);
        Boolean nominateAdmin(IUser newAdmin);
        Boolean registerUser(IUser newUser);
        ISubForum createSubForum(String name, List<IUser> moderators);
        Boolean changePoliciy(String newPolicy);
        Boolean isAdmin(String userName);
        Boolean isMember(String userName);
        Boolean dismissMember(IUser userName);
        String getPolicy();
    }
}
