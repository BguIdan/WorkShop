using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forum;

namespace ForumBuilder.Users
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
        Boolean changePolicy(String newPolicy, IForum forum);
        Boolean isAdmin();
        Boolean isMember();
        Boolean createThread();
        Boolean dismissModerator(IUser dismissedModerator);
        Boolean deleteThread(IThread toDelete);

    }
}
