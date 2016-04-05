using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Users;

namespace ForumBuilder.BL_Back_End
{
    public interface ISubForum
    {
        Boolean dismissModerator(IUser dismissedModerator);
        Boolean createThread(String headLine, String Content);
        Boolean nominateModerator(IUser newModerator);
        Boolean deleteThread(IThread deleteThread);
    }
}
