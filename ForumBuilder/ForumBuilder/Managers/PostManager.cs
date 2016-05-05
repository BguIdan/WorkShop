﻿using ForumBuilder.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Common.ServiceContracts;
using ForumBuilder.Common.DataContracts;

namespace Service
{
    public class PostManager :IPostManager
    {
        private static PostManager singleton;
        private IPostController postController;


        private PostManager()
        {
            postController = PostController.getInstance;
        }

        public static PostManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new PostManager();
                }
                return singleton;
            }
        }

        public Boolean deletePost(Int32 postId, String deletingUser)
        {
            return postController.removeComment(postId, deletingUser);
        }
        public Boolean addPost(String headLine, String content, String writerName, Int32 commentedPost)
        {
            return postController.addComment(headLine, content, writerName, commentedPost);
        }
        public List<PostData> getAllPosts(String forumName, String subforumName)
        {
            //TODO: complete the function
            return null;
        }
    }
}