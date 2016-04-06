using System;

namespace ForumBuilder.BL_Back_End
{
    public interface IPostController
    {
        Boolean deletePost(Int32 postId, String deletingUser);/*delete post and comments from dal, call delete from thread if neccary, delete post from parent list*/
        Boolean addPost(String headLine, String content, String writerName, DateTime timePublished, Int32 commentedPost/*if new thread, -1*/, String forum, String subForum);/*calls to threadcontroller, add post to dal*/
    }
}
