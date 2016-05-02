using System;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace ForumBuilder.Common.DataContracts
{
    [DataContract]
    public class ThreadData
    {
        [DataMember]
        private PostData _firstPost;

        [DataMember]
        private String _title;

        public ThreadData(PostData post)
        {
            this._firstPost = post;
            this._title = post.title;
        }
    }
}
