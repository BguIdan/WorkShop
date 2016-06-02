using System;
using System.Collections.Generic;
using BL_Back_End;

namespace ForumBuilder.Controllers
{
    public interface ISuperUserController : IUserController
    {
        Boolean createForum(String forumName, String descrption, ForumPolicy fp, List<String> administrators, String superUserName);
        Boolean login(String newUser, String forumName, string email);
        Boolean isSuperUser(string userName);
        int SuperUserReportNumOfForums(string superUserName);
        List<String> getSuperUserReportOfMembers(string superUserName);
        Boolean addUser(string userName, string password, string mail, string superUserName);
    }
}
