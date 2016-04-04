using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forums;


namespace ForumBuilder.System
{
    class System : ISystem
    {

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
            try
            {
                if (forumName.Equals("") || descrption.Equals("") || forumPolicy.Equals("") || forumRules.Equals("") || administrators == null)
                {
                    Console.WriteLine("one of the fields was empty");
                    return false;
                }
                IForum newForum = new Forum(forumName, descrption, forumPolicy, forumRules, administrators);
                _forums.Add(newForum);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int Main(string[] args)
        {
            return -1;
        }
    }
}
