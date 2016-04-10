using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Controllers;
using ForumBuilder.Systems;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.BL_Back_End
{
    public class SuperUser
    {
        private ForumSystem _system;
        private String userName;
        private String password;
        private String email;

        public SuperUser()
        {
            forumSystem = new ForumSystem();
        }

        internal ForumSystem forumSystem
        {
            get{ return _system;}
            set{ _system = value;}
        }

        public string _email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }

        public string _password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string _userName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }
    }
}
