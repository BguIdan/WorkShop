using System;
using System.Collections.Generic;


namespace ForumBuilder.BL_Back_End
{
    public interface IForumController
    {
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName);
        Boolean banMember(String bannedMember, String bannerUserName);
        Boolean nominateAdmin(String newAdmin, String nominatorName);
        Boolean registerUser(String newUser, String password, String mail);
        Boolean addSubForum(String name, List<String> moderators);
        Boolean changePoliciy(String newPolicy, String changerName);
        Boolean isAdmin(String userName);
        Boolean isMember(String userName,String forumName );
        Boolean dismissMember(String userName, String dismissingUserName, String forumName);
    }
}
