﻿using System;
using System.Collections.Generic;


namespace ForumBuilder.Controllers
{
    public interface IForumController
    {
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, string forumName);
        Boolean banMember(String bannedMember, String bannerUserName, string forumName);
        Boolean nominateAdmin(String newAdmin, String nominatorName, string forumName);
        Boolean registerUser(String newUser, String password, String mail);
        Boolean addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string userNameAdmin);
        Boolean isAdmin(String userName, String forumName);
        Boolean isMember(String userName, String forumName);
        Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules);
        String getForumPolicy(String forumName);
        String getForumDescription(String forumName);
        String getForumRules(String forumName);

    }
}
