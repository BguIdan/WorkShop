using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Users;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    class ForumController
    {
        private Forum _forum;

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
        public Boolean addSubForum(SubForum subF)
        {
            _forum.subForums.Add(subF.Name);
            return true;
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
    }
}
