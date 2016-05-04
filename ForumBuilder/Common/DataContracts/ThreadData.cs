using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ForumBuilder.Common.DataContracts
{
    [DataContract]
    public class ThreadData
    {
        [DataMember]
        private Post _firstPost;

        [DataMember]
        private String _title;

        public ThreadData(Post post)
        {
            _firstPost = post;
            _title = post.title;
        }

        public Post firstPost
        {
            get { return _firstPost; }
            set { _firstPost = value; }
        }
    }
}
