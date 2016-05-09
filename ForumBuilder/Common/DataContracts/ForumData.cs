using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ForumBuilder.Common.DataContracts
{
    [DataContract]
    public class ForumData
    {
        [DataMember]
        private String _forumName;

        [DataMember]
        private String _description;

        [DataMember]
        private String _forumPolicy;

        [DataMember]
        private String _forumRules;

        [DataMember]
        private List<string> _subForums;
        
        [DataMember]
        private List<string> _members;

        public ForumData(string forumName, string descrption, string forumPolicy, string forumRules, List<String> subForums)
        {
            _forumName = forumName;
            _description = descrption;
            _forumPolicy = forumPolicy;
            _forumRules = forumRules;
            _subForums = subForums;
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

        public List<String> subForums
        {
            get { return _subForums; }
            set { _subForums = value; }
        }

        public List<string> members
        {
            get { return _members; }
            set { _members = value;}
        }
    }
}
