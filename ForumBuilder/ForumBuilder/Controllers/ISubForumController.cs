using System;

namespace ForumBuilder.BL_Back_End
{
    public interface ISubForumController
    {
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin);
        Boolean createThread(String headLine, String Content, String userName);/*add thread to sub forum list*/
        Boolean nominateModerator(String newModerator, String nominatorUser);
        Boolean deleteThread(Int32 firstPostId, String deleteUserName);/*remove thread from sub forum*/
    }
}
