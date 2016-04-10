using System;
using ForumBuilder.Controllers;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Controllers
{
    public class SubForumController : ISubForumController
    {
        private static SubForumController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        ForumController forumController = ForumController.getInstance;
        public static SubForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SubForumController();
                    Systems.Logger.getInstance.logPrint("Sub-forum contoller created");
                }
                return singleton;
            }
        }
        public bool createThread(Thread thread, String forum, String subForum)
        {
            //add check if forum exist
            return demoDB.addThreadToSubForum(thread, forum, subForum);
        }
        
        public bool deleteThread(int firstPostId)
        {
            return demoDB.deleteThreadFromSubforum(firstPostId);
        }

        public bool dismissModerator(string dismissedModerator, string dismissByAdmin, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (subForum == null)
            {
                logger.logPrint("Dismiss moderator failed, sub-forum does not exist");
                return false;
            }
            else if (!forumController.isAdmin(dismissByAdmin, forumName))
            {
                logger.logPrint("Dismiss moderator failed, "+ dismissByAdmin+" is not an admin");
                return false;
            }
            else if( !isModerator(dismissedModerator,subForumName, forumName))
            {
                logger.logPrint("Dismiss moderator failed, " + dismissedModerator + " is not a moderator");
                return false;
            }
            else
            {
                return demoDB.dismissModerator(dismissedModerator, dismissByAdmin, subForum);
            }
        }

        public bool isModerator(string name, string subForumName, string forumName)
        {
            return getSubForum(subForumName, forumName).moderators.ContainsKey(name);
        }

        public bool nominateModerator(string newModerator, string nominatorUser, DateTime date, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (forumController.isAdmin(nominatorUser, forumName) && forumController.isMember(newModerator, forumName))
            {
                if (DateTime.Now.CompareTo(date) > 0)
                {
                    logger.logPrint("the date in nominate moderator already past");
                    return false;
                }
                if (subForum.moderators.ContainsKey(newModerator))
                {
                    logger.logPrint("Nominate moderator failed, user is alraedy exist");
                    return false;
                }
                if (demoDB.nominateModerator(newModerator, nominatorUser, date, subForum))
                {
                    logger.logPrint("nominate moderator " + newModerator + "success");
                    return true;
                }
            }
            if(!forumController.isAdmin(nominatorUser, forumName))
                logger.logPrint("To "+nominatorUser+" has no permission to nominate moderator");
            if(!forumController.isMember(newModerator, forumName))
                logger.logPrint("To " + newModerator + " has no permission to be moderator, he is not a member");
            return false;
        }
        public SubForum getSubForum(string subForumName, string forumName)
        {
            return demoDB.getSubForum(subForumName, forumName);
        }
    }
}
