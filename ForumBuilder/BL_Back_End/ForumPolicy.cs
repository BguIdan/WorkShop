using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Back_End
{
    public class ForumPolicy
    {
        private String _policy;
        private bool _isQuestionIdentifying;
        private int _seniorityInForum;
        private bool _deletePostByMderator;
        private int _timeToPassExpiration;

        public ForumPolicy()
        {

        }

        public Boolean isQuestionIdentifying
        {
            get { return _isQuestionIdentifying; }
            set { _isQuestionIdentifying = value; }
        }

        public Boolean deletePostByMderator
        {
            get { return _deletePostByMderator; }
            set { _deletePostByMderator = value; }
        }

        public int seniorityInForum
        {
            get { return _seniorityInForum; }
            set { _seniorityInForum = value; }
        }

        public int timeToPassExpiration
        {
            get { return _timeToPassExpiration; }
            set { _timeToPassExpiration = value; }
        }

        public string policy
        {
            get { return _policy; }
            set { _policy = value; }
        }

    }
}
