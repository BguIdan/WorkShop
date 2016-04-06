using System;

namespace ForumBuilder.Users
{
    public interface IUserController
    {
        Boolean addFriend(String userName, String friendToAdd);
        Boolean deleteFriend(String userName, String deletedFriend);
        Boolean sendPrivateMessage(string fromUserName, string toUserName, string content, Int32 id);

        /*
        this should be in other controllers

        Boolean notifyUserViaMail();
        Boolean banMember(String userName, String forumName);
        Boolean createSubForum(String subForumName, String forumName, List<String> moderators);
        Boolean changePolicy(String newPolicy, String forumName);
        Boolean isAdmin(String forumName);
        Boolean isMember(String forumName);
        Boolean createThread(String threadName, String forumName, String subForumName);
        Boolean dismissModerator(String dismissedModerator, String forumName, String subForumName);
        //should add delete post?
        */
    }
}
