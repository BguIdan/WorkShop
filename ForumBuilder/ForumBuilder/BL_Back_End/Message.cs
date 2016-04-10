using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.BL_Back_End
{
    public class Message
    {
        private Int32 _id;
        private String _sender;
        private String _reciver;
        private String _content;

        public Message(Int32 id, String sender, String reciver, String content)
        {
            _id = id;
            _sender = sender;
            _reciver = reciver;
            _content = content;
        }

        public Int32 id
        {
            get { return _id; }
            set { _id = value; }
        }

        public String sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public String reciver
        {
            get { return _reciver; }
            set { _reciver = value; }
        }
    }
}
