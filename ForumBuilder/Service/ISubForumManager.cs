using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface ISubForumManager
    {
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, String subForumName, String forumName);
        Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, String subForumName, String forumName);
        bool createThread(String headLine, String content, String writerName, String forumName, String subForumName);
        bool deleteThread(int firstPostId, string removerName);
    }
}
