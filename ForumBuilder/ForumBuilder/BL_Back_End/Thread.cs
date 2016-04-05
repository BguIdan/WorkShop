using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.BL_Back_End
{
    public class Thread : IThread
    {
        private Post _firstPost;
        private String _title;

        public Thread(Post post)
        {
            _firstPost = post;
            _title = post._title;
        }

        public Post FirstPost
        {
            get { return _firstPost; }
            set { _firstPost = value; }
        }
        
    }
}
