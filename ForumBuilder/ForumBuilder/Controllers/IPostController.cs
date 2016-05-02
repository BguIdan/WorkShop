using System;
using System.Collections.Generic;
using BL_Back_End;

namespace ForumBuilder.Controllers
{
    public interface IPostController
    {
        Boolean removeComment(int postId, String removerName);
        Boolean addComment(String headLine, String content, String writerName, int commentedPost/*if new thread, -1*/);
        List<Post> getAllPosts(String forumName, String subforumName);
    }
}
