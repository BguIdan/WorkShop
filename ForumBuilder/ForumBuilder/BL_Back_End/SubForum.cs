using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.BL_Back_End
{
    public class SubForum
    {
        private String _name;
        private List<String> _moderators;
        private List<Int32> _threads;

        public SubForum(String name)
        {
            _name = name;
            _moderators = new List<string>();
            _threads = new List<int>();
        }

        public String name
        {
            get { return _name; }
            set { _name = value; }
        }

        public List<String> moderators
        {
            get { return _moderators; }
            set { _moderators = value; }
        }

        public List<Int32> threads
        {
            get { return _threads; }
            set { _threads = value; }
        }
    }
}
