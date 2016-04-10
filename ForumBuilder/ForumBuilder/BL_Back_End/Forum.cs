using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.Controllers
{
    public class Forum
    {
        private String _forumName;
        private String _description;
        private String _forumPolicy;
        private String _forumRules;
        private List<string> _administrators;
        private List<string> _members;
        private List<string> _subForums;



        public Forum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            _forumName = forumName;
            _description = descrption;
            _forumPolicy = forumPolicy;
            _forumRules = forumRules;
            _administrators = administrators;
        }

        public String forumPolicy
        {
            get { return _forumPolicy; }
            set { _forumPolicy = value; }
        }

        public String forumRules
        {
            get { return _forumRules; }
            set { _forumRules = value; }
        }

        public String description
        {
            get { return _description; }
            set { _description = value; }
        }

        public String forumName
        {
            get { return _forumName; }
            set { _forumName = value; }
        }

        public List<String> administrators
        {
            get { return _administrators; }
            set { _administrators = value; }
        }

        public List<String> subForums
        {
            get { return _subForums; }
            set { _subForums = value; }
        }

        public List<string> members
        {
            get
            {
                return _members;
            }

            set
            {
                _members = value;
            }
        }
    }
}
