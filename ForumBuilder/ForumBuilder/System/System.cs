using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumBuilder.System
{
    class System : ISystem
    {
        public bool initialize(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            throw new NotImplementedException();
        }

        public static int Main(string[] args)
        {
            return -1;
        }
    }
}
