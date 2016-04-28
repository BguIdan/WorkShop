﻿using System;
using BL_Back_End;
namespace ForumBuilder.Controllers
{
    public interface ISubForumController
    {
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, string subForumName, string forumName);
        Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, string subForumName, string forumName);
        Boolean isModerator(string name, string subForumName, string forumName);
        SubForum getSubForum(string subForumName, string forumName);
        bool createThread(String headLine, String content, String writerName, String forumName, String subForumName);
        bool deleteThread(int firstPostId, string removerName);
    }
}
