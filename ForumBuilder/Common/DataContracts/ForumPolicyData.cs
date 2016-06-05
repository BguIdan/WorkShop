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
    public class ForumPolicyData
    {
        [DataMember]
        private String _policy;

        [DataMember]
        private bool _isQuestionIdentifying;

        [DataMember]
        private int _seniorityInForum;

        [DataMember]
        private bool _deletePostByModerator;

        [DataMember]
        private int _timeToPassExpiration;

        [DataMember]
        private int _minNumOfModerator;

        [DataMember]
        private bool _hasCapitalInPassword;

        [DataMember]
        private bool _hasNumberInPassword;

        [DataMember]
        private int _minLengthOfPassword;

        public ForumPolicyData()
        {
            _policy = " ";
            _seniorityInForum = 0;
            _timeToPassExpiration = 30;
            _minNumOfModerator = 1;
            _minLengthOfPassword = 5;

        }

        public ForumPolicyData(String policy, bool isQuestionIdentifying, int seniorityInForum, bool deletePostByMderator, int timeToPassExpiration,
                                int minNumOfModerator, bool hasCapitalInPassword, bool hasNumberInPassword, int minLengthOfPassword)
        {
            _policy = policy;
            _isQuestionIdentifying = isQuestionIdentifying;
            _seniorityInForum = seniorityInForum;
            _deletePostByModerator = deletePostByMderator;
            _timeToPassExpiration = timeToPassExpiration;
            _minNumOfModerator = minNumOfModerator;
            _hasCapitalInPassword = hasCapitalInPassword;
            _hasNumberInPassword = hasCapitalInPassword;
            _minLengthOfPassword = minLengthOfPassword;
        }

        public Boolean isQuestionIdentifying
        {
            get { return _isQuestionIdentifying; }
            set { _isQuestionIdentifying = value; }
        }

        public Boolean deletePostByModerator
        {
            get { return _deletePostByModerator; }
            set { _deletePostByModerator = value; }
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

        public int minNumOfModerator
        {
            get { return _minNumOfModerator; }
            set { _minNumOfModerator = value; }
        }

        public bool hasCapitalInPassword
        {
            get { return _hasCapitalInPassword; }
            set { _hasCapitalInPassword = value; }
        }

        public bool hasNumberInPassword
        {
            get { return _hasNumberInPassword; }
            set { _hasNumberInPassword = value; }
        }

        public int minLengthOfPassword
        {
            get { return _minLengthOfPassword; }
            set { _minLengthOfPassword = value; }
        }

    }
}
