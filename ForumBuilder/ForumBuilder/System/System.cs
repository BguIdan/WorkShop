using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forums;
using ForumBuilder.Services;

namespace ForumBuilder.System
{
    class System : ISystem
    {
        private IService service;

        public System()
        {
            service = Service.getInstance();
        }

        private List<IForum> forums;

        public List<IForum> _forums
        {
            get
            {
                return forums;
            }

            set
            {
                forums = value;
            }
        }

        public bool initialize(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public bool createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            return service.createForum(forumName, descrption, forumPolicy, forumRules, administrators);
        }

        public static int Main(string[] args)
        {
            return -1;
        }
    }
}
