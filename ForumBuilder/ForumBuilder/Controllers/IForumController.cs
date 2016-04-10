using System;
using System.Collections.Generic;


namespace ForumBuilder.BL_Back_End
{
    public interface IForumController
    {
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, string forumName);
        Boolean banMember(String bannedMember, String bannerUserName, string forumName);
        Boolean nominateAdmin(String newAdmin, String nominatorName, string forumName);
        Boolean registerUser(String newUser, String password, String mail);
        Boolean addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string userNameAdmin);
        Boolean changePoliciy(String newPolicy, String changerName, string forumName);
        Boolean isAdmin(String userName, String forumName);
        Boolean isMember(String userName, String forumName);
        //        Boolean dismissMember(String userName, String dismissingUserName, String forumName);

    }
}
