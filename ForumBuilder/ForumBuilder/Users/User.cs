using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Users
{
    public class User : IUser
    {
        private String _userName;
        private String _password;
        private String _email;
        private List<User> _friends; 

        public User()
        {

        }

        public String userName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public String password
        {
            get { return _password; }
            set { _password = value; }
        }

        public String email
        {
            get { return _email; }
            set { _email = value; }
        }

        public List<User> friends
        {
            get { return _friends; }
            set { _friends = value; }
        }

    }
}
