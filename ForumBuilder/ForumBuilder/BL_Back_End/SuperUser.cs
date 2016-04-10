using System;
using ForumBuilder.Systems;

namespace ForumBuilder.BL_Back_End
{
    public class SuperUser : User
    {
        private ForumSystem _system;

        public SuperUser(string email, string password, string userName) :base(email, password,userName)
        {
            forumSystem = ForumSystem.initialize(userName, password, email);
        }

        internal ForumSystem forumSystem
        {
            get{ return _system;}
            set{ _system = value;}
        }

        
    }
}
