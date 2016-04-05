using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;
using ForumBuilder.Services;

namespace ForumBuilder.Controllers
{
    class UserController
    {
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
            ISubForum subF = forum.createSubForum(subForumName, moderators);
            return subF != null;
        }
        public Boolean changePolicy(String newPolicy, Forum forum)
        {
            return true;
        }
        public Boolean isAdmin(Forum forum)
        {
            return true;
        }
        public Boolean isMember(Forum forum)
        {
            return true;
        }
        // think about it what params should be
        public Boolean createThread(Forum forum)
        {
            return true;
        }
        public Boolean dismissModerator(User dismissedModerator, SubForum subFor)
        {
            return true;
        }
        
    }
}
