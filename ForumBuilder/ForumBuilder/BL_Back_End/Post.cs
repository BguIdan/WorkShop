using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.BL_Back_End
{
    public class Post : IPost
    {
        private int id;
        private string title;
        private string content;
        private int parentId;

        public Post(int id, string title, string content, int parentId)
        {
            this.id = id;
            this.title = title;
            this.content = content;
            this.parentId = parentId;
        }
        
        public Int32 _id
        {
            get { return id; }
            set { id = value; }
        }

        public Int32 _parentId
        {
            get { return parentId; }
            set { parentId = value; }
        }

        public String _title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
