using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Services;

namespace ForumBuilder.Systems
{
    class ForumSystem : ISystem
    {
        private IService service;

        public ForumSystem()
        {
            service = Service.getInstance;
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
            return service.initialize(userName, password, email);
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
