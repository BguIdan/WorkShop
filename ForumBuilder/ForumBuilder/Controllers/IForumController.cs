using System;
using System.Collections.Generic;
using BL_Back_End;


namespace ForumBuilder.Controllers
{
    public interface IForumController
    {
        Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, string forumName);
        Boolean banMember(String bannedMember, String bannerUserName, string forumName);
        String nominateAdmin(String newAdmin, String nominatorName, string forumName);
        String registerUser(String newUser, String password, String mail, string ans1, string ans2, string forumName);
        Boolean addForum(String forumName);
        int login(String newUser, String forumName, string pass);
        String loginBySessionKey(int sessionKey, String user, String forumName);
        Boolean logout(String user, String forumName);
        Boolean sendThreadCreationNotification(String headLine, String content, String publisherName, String forumName, String subForumName);
        Boolean sendPostModificationNotification(String forumName, String publisherName, String title, String content);
        Boolean sendPostDelitionNotification(String forumName, String publisherName);
        String addSubForum(string forumName, string name, Dictionary<String, DateTime> moderators, string userNameAdmin);
        Boolean isAdmin(String userName, String forumName);
        Boolean isMember(String userName, String forumName);
        Boolean setForumPreferences(String forumName, String newDescription, ForumPolicy fp, string setterUserName);
        String getForumPolicy(String forumName);
        String getForumDescription(String forumName);
        Forum getForum(String forumName);
        List<String> getForums();
        int getAdminReportNumOfPOst(String AdminName, String forumName);
        List<Post> getAdminReportPostOfmember(String AdminName, String forumName, String memberName);
        List<String> getAdminReport(String AdminName, String forumName);
        void notifyUserOnNewPrivateMessage(String forumName, String sender, String addressee, String content);
    }
}
