﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ForumBuilder.Common.ServiceContracts
{
    [ServiceContract]
    public interface ISubForumManager
    {
        [OperationContract]
        Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, String subForumName, String forumName);

        [OperationContract]
        Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, String subForumName, String forumName);

        [OperationContract]
        bool createThread(String headLine, String content, String writerName, String forumName, String subForumName);

        [OperationContract]
        bool deleteThread(int firstPostId, string removerName);
    }
}