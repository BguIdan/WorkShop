using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.BL_Back_End
{
    public class Forum : IForum
    {
        private String forumName;
        private String description;
        private String forumPolicy;
        private String forumRules;
        private List<string> _administrators;
        private List<string> _members;
        private List<string> _subForums;

        public Forum()
        {

        }

        public Forum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            _forumName = forumName;
            _description = descrption;
            _forumPolicy = forumPolicy;
            _forumRules = forumRules;
            _administrators = administrators;
        }

        public String _forumPolicy
        {
            get { return forumPolicy; }
            set { forumPolicy = value; }
        }

        public String _forumRules
        {
            get { return forumRules; }
            set { forumRules = value; }
        }

        public String _description
        {
            get { return description; }
            set { description = value; }
        }

        public String _forumName
        {
            get { return forumName; }
            set { forumName = value; }
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

    }
}
