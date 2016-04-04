using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.Forums
{
    public interface ISubForum
    {
        Boolean dismissModerator(IUser dismissedModerator);
        Boolean createThread(String headLine, String Content);
        Boolean nominateModerator(IUser newModerator);
        Boolean deleteThread(IThread deleteThread);
        List<String> getModerators();
        List<IThread> getThreads();
    }
}
