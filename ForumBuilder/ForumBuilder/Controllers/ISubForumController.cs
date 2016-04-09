using System;
using ForumBuilder.BL_Back_End;
namespace ForumBuilder.Controllers
{
    public interface ISubForumController
    {
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, SubForum subForum);
        //Boolean createThread(Thread thread, String forum, String subForum);/*add thread to sub forum list*/
        Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, SubForum subForum);
        //Boolean deleteThread(Int32 firstPostId);/*remove thread from sub forum*/
    }
}
