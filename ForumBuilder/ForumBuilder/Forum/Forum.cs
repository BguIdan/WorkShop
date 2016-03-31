using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.User;

namespace ForumBuilder.Forum
{
    class Forum : IForum
    {

        public Forum()
        {

        }
        Boolean dismissAdmin(IUser userismissedAdmin)
        {
            return true;
        }
        Boolean banMember(IUser bannedMember)
        {
            return true;
        }
        Boolean nominateAdmin(IUser newAdmin)
        {
            return true;
        }
        Boolean registerUser(IUser newUser)
        {
            return true;
        }
        ISubForum createSubForum(String name, List<IUser> moderators)
        {
            return null;
        }
        Boolean changePoliciy(String newPolicy)
        {
            return true;
        }
        Boolean isAdmin(String userName)
        {
            return true;
        }
        Boolean isMember(String userName)
        {
            return true;
        }
        Boolean deleteMember(String userName)
        {
            return true;
        }
    }
}
