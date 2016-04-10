using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Controllers;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.BL_Back_End
{
    public class User
    {
        private String _userName;
        private String _password;
        private String _email;
        private List<String> _friends; 

        public User(String userName, String password, String email)
        {
            _userName = userName;
            _password = password;
            _email = email;
            _friends = new List<String>();
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

        public List<String> friends
        {
            get { return _friends; }
            set { _friends = value; }
        }

    }
}
