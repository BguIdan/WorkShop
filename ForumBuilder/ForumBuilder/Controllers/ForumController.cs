using System;
using System.Collections.Generic;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Controllers
{
    class ForumController : IForumController
    {
        private static ForumController singleton;

        public static ForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ForumController();
                }
                return singleton;
            }

        }

        public bool addSubForum(string name, List<string> moderators)
        {
            throw new NotImplementedException();
        }

        public bool banMember(string bannedMember, string bannerUserName)
        {
            throw new NotImplementedException();
        }

        public bool changePoliciy(string newPolicy, string changerName)
        {
            throw new NotImplementedException();
        }

        public bool dismissAdmin(string adminToDismissed, string dismissingUserName)
        {
            throw new NotImplementedException();
        }

        public bool dismissMember(string userName, string dismissingUserName)
        {
            throw new NotImplementedException();
        }

        public bool nominateAdmin(string newAdmin, string nominatorName)
        {
            throw new NotImplementedException();
        }

        public bool registerUser(string newUser, string password, string mail)
        {
            throw new NotImplementedException();
        }
    }
}
