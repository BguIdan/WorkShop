using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.Forums
{
    public class Forum : IForum
    {
        private String forumName;
        private String description;
        private String forumPolicy;
        private String forumRules;
        private List<string> administrators;
        private List<ISubForum> _subForums;

        public String _forumPolicy
        {
            get
            {
                return forumPolicy;
            }

            set
            {
                forumPolicy = value;
            }
        }

        public String _forumRules
        {
            get
            {
                return forumRules;
            }

            set
            {
                forumRules = value;
            }
        }

        public String _description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public String _forumName
        {
            get
            {
                return forumName;
            }

            set
            {
                forumName = value;
            }
        }

        public List<String> _administrators
        {
            get
            {
                return administrators;
            }

            set
            {
                administrators = value;
            }
        }

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
        public Boolean dismissAdmin(IUser userismissedAdmin)
        {
            return true;
        }
        public Boolean banMember(IUser bannedMember)
        {
            return true;
        }
        public Boolean nominateAdmin(IUser newAdmin)
        {
            return true;
        }
        public Boolean registerUser(IUser newUser)
        {

            return true;
        }
        public ISubForum createSubForum(String name, List<String> moderators)
        {
            SubForum subF = new SubForum(name);

            _subForums.Add(subF);
            return subF;
        }
        public Boolean changePoliciy(String newPolicy)
        {
            return true;
        }
        public Boolean isAdmin(String userName)
        {
            return true;
        }
        public Boolean isMember(String userName)
        {
            return true;
        }
        public Boolean dismissMember(IUser userName)
        {
            return true;
        }

        public String getPolicy()
        {
            return null;
        }
    }
}
