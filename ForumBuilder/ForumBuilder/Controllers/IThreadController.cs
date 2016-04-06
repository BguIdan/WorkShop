using System;


namespace ForumBuilder.BL_Back_End
{
    public interface IThreadController
    {
        Boolean addFirstPost(String headLine, String Content, String writerName, DateTime timePublished);/*calls to subforumcontroller, add thread to dal*/
        Boolean deleteThread(Int32 firstPostToDelete,String deleteUser);/*delete thread from dal call delete from subforum*/
    }
}
