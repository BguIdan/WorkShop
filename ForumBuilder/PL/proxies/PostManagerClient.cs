﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;
using ForumBuilder.Common.DataContracts;

namespace PL.proxies
{
    public class PostManagerClient : ClientBase<IPostManager>, IPostManager
    {
        public PostManagerClient()
        {

        }

        public Boolean deletePost(Int32 postId, String deletingUser)
        {
            return Channel.deletePost(postId, deletingUser);
        }

        public Boolean addPost(String headLine, String content, String writerName, Int32 commentedPost)
        {
            return Channel.addPost(headLine, content, writerName, commentedPost);
        }

        public List<PostData> getAllPosts(String forumName, String subforumName)
        {
            return Channel.getAllPosts(forumName, subforumName);
        }

    }
}
