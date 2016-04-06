using System;
using System.Collections.Generic;

namespace ForumBuilder.BL_Back_End
{
    public class SubForum
    {
        private String _name;
        private List<String> _moderators;
        private List<Int32> _threads;
        private String _forum;

        public SubForum(String name,String forumName)
        {
            _name = name;
            _moderators = new List<string>();
            _threads = new List<int>();
            _forum = forumName;
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
        public String forum
        {
            get { return _forum; }
            set { _forum = value; }
        }
    }
}
