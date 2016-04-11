using System;

namespace ForumBuilder.Controllers
{
    public interface IPostController
    {
        Boolean removeComment(int postId, String removerName);
        Boolean addComment(String headLine, String content, String writerName, int commentedPost/*if new thread, -1*/);
    }
}
