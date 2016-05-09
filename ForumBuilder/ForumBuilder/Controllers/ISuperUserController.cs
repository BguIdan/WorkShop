using System;
using System.Collections.Generic;

namespace ForumBuilder.Controllers
{
    public interface ISuperUserController : IUserController
    {
        Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName);
        Boolean addUser(string userName, string password, string mail);
        Boolean login(String newUser, String forumName, string email);
        Boolean isSuperUser(string userName);
        int SuperUserReportNumOfForums(string superUserName);
        List<String> getSuperUserReportOfMembers(string superUserName);
    }
}
