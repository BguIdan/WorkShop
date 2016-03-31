using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forum;

namespace ForumBuilder.User
{
    class User : IUser
    {
        public User()
        {

        }
        String getUserName()
        {
            return "";
        }
        Boolean addFriend(IUser newFriend)
        {
            return true;
        }
        Boolean deleteFriend(IUser deletedFriend)
        {
            return true;
        }
        Boolean notifyUserViaMail()
        {
            return true;
        }
        Boolean sendPrivateMessage(String userName, String content)
        {
            return true;
        }
        Boolean banMember(String userName, String forumName)
        {
            return true;
        }
        Boolean createSubForum(String subForumName, IForum forum, List<String> moderators)
        {
            return true;
        }
        Boolean changePolicy(String newPolicy, IForum forum)
        {
            return true;
        }
        Boolean isAdmin()
        {
            return true;
        }
        Boolean isMember()
        {
            return true;
        }
        Boolean createThread()
        {
            return true;
        }
        Boolean dismissModerator(IUser dismissedModerator)
        {
            return true;
        }
        Boolean deleteThread(IThread toDelete)
        {
            return true;
        }
    }
}
