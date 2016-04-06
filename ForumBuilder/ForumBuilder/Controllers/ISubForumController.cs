using System;

namespace ForumBuilder.BL_Back_End
{
    public interface ISubForumController
    {
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin);
        Boolean createThread(Thread thread, String forum, String subForum);/*add thread to sub forum list*/
        Boolean nominateModerator(String newModerator, String nominatorUser);
        Boolean deleteThread(Int32 firstPostId, String deleteUserName);/*remove thread from sub forum*/
    }
}
