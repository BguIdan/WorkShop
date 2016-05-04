using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace PL.proxies
{
    public class SubForumManagerClient : ClientBase<ISubForumManager>, ISubForumManager
    {
        public Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, String subForumName, String forumName)
        public SubForumManagerClient() 
        {

        }

        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, String subForumName, String forumName)
        {
            return Channel.dismissModerator(dismissedModerator, dismissByAdmin, subForumName, forumName);
        }

        public Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, String subForumName, String forumName)
        {
            return Channel.nominateModerator(newModerator, nominatorUser, date, subForumName, forumName);
        }

        public bool createThread(String headLine, String content, String writerName, String forumName, String subForumName)
        {
            return Channel.createThread(headLine, content, writerName, forumName, subForumName);
        }

        public bool deleteThread(int firstPostId, string removerName)
        {
            return Channel.deleteThread(firstPostId, removerName);
        }

        Boolean isModerator(string name, string subForumName, string forumName)
        {
            return Channel.isModerator(name, subForumName, forumName);
        }

    }
}
