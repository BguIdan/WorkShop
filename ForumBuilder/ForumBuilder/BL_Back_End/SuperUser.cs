using System;
using ForumBuilder.Systems;

namespace ForumBuilder.BL_Back_End
{
    public class SuperUser:User
    {
        //private ForumSystem _system;


        public SuperUser(string email, string password, string userName): base(userName, password, email)
        {
            //forumSystem = new ForumSystem();
        }
        /*
        internal ForumSystem forumSystem
        {
            get{ return _system;}
            set{ _system = value;}
        }
        */
        
    }
}
