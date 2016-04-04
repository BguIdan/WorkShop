using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forums;

namespace ForumBuilder.Users
{
    public class User : IUser
    {
        public User()
        {

        }
        public String getUserName()
        {
            return "";
        }
        public Boolean addFriend(IUser newFriend)
        {
            return true;
        }
        public Boolean deleteFriend(IUser deletedFriend)
        {
            return true;
        }
        public Boolean notifyUserViaMail()
        {
            return true;
        }
        public Boolean sendPrivateMessage(String userName, String content)
        {
            return true;
        }
        public Boolean banMember(String userName, String forumName)
        {
            return true;
        }
        public Boolean createSubForum(String subForumName, IForum forum, List<String> moderators)
        {
            return true;
        }
        public Boolean changePolicy(String newPolicy, IForum forum)
        {
            return true;
        }
        public Boolean isAdmin()
        {
            return true;
        }
        public Boolean isMember()
        {
            return true;
        }
        public Boolean createThread()
        {
            return true;
        }
        public Boolean dismissModerator(IUser dismissedModerator)
        {
            return true;
        }
        public Boolean deleteThread(IThread toDelete)
        {
            return true;
        }
    }
}
