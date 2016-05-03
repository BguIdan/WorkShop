using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    class SubForumManagerClient : ClientBase<ISubForumManager>, ISubForumManager
    {
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, String subForumName, String forumName)
        {
            return Channel.dismissModerator(dismissedModerator, dismissByAdmin, subForumName, forumName);
        }

        Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, String subForumName, String forumName)
        {
            return Channel.nominateModerator(newModerator, nominatorUser, date, subForumName, forumName);
        }

        bool createThread(String headLine, String content, String writerName, String forumName, String subForumName)
        {
            return Channel.createThread(headLine, content, writerName, forumName, subForumName);
        }

        bool deleteThread(int firstPostId, string removerName)
        {
            return Channel.deleteThread(firstPostId, removerName);
        }
    }
}
