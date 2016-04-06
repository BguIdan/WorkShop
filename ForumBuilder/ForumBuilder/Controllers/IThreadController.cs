using System;


namespace ForumBuilder.BL_Back_End
{
    public interface IThreadController
    {
        Boolean addFirstPost(Post newPost, String forum, String subForum);/*calls to subforumcontroller, add thread to dal*/
        Boolean deleteThread(Int32 firstPostToDelete,String deleteUser);/*delete thread from dal call delete from subforum*/
    }
}
