using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ForumBuilder.Common.DataContracts
{
    [DataContract]
    public class SubForum
    {
        [DataMember]
        private String name { get; set; }

        [DataMember]
        private Dictionary<String, DateTime> moderators { get; set; }

        [DataMember]
        private List<Int32> threads { get; set; }

        [DataMember]
        private String forum { get; set; }

        public SubForum(String name,String forumName)
        {
            this.name = name;
            this.moderators = new Dictionary<String, DateTime>();
            this.threads = new List<int>();
            this.forum = forumName;
        }
    }
}
