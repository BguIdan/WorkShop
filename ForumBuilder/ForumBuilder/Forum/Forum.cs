using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.User;

namespace ForumBuilder.Forum
{
    public class Forum : IForum
    {

        public Forum()
        {

        }
        public Boolean dismissAdmin(IUser userismissedAdmin)
        {
            return true;
        }
        public Boolean banMember(IUser bannedMember)
        {
            return true;
        }
        public Boolean nominateAdmin(IUser newAdmin)
        {
            return true;
        }
        public Boolean registerUser(IUser newUser)
        {

            return true;
        }
        public ISubForum createSubForum(String name, List<IUser> moderators)
        {
            return null;
        }
        public Boolean changePoliciy(String newPolicy)
        {
            return true;
        }
        public Boolean isAdmin(String userName)
        {
            return true;
        }
        public Boolean isMember(String userName)
        {
            return true;
        }
        public Boolean dismissMember(IUser userName)
        {
            return true;
        }

        public String getPolicy()
        {
            return null;
        }
    }
}
