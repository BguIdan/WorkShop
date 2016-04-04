using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.Forums
{
    public class SubForum : ISubForum
    {
        private String _name;
        private List<String> _moderators;

        public SubForum(String name)
        {
            _name = name;
        }
        public Boolean dismissModerator(IUser dismissedModerator)
        {
            return true;
        }

        public Boolean createThread(String headLine, String Content)
        {
            return true;
        }
        public Boolean nominateModerator(IUser newModerator)
        {
            return true;
        }

        public Boolean deleteThread(IThread deleteThread)
        {
            throw new NotImplementedException();
        }

        public List<String> getModerators()
        {
            throw new NotImplementedException();
        }

        public List<IThread> getThreads()
        {
            throw new NotImplementedException();
        }
    }
}
