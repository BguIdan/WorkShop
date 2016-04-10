using System;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;
using System.Collections.Generic;
using System.Linq;

namespace ForumBuilder.Controllers
{
    class PostController :IPostController
    {
        private static PostController singleton;
        ThreadController threadController = ThreadController.getInstance;
        DemoDB demoDB = DemoDB.getInstance;
        public static PostController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new PostController();
                }
                return singleton;
            }

        }
        public Boolean deletePost(Int32 postId, String deletingUser)
        {
            SubForum sf= getSubforumByPost(postId);
            Forum f = demoDB.getforumByName(sf.forum);
            if (sf==null || f==null ||( !getPost(postId).writerUserName.Equals(deletingUser)
                && !sf.moderators.Contains(deletingUser) && !f.administrators.Contains(deletingUser)))
                return false;
            //find the posts that have to delete
            List<Post> donePosts = new List<Post>();
            List<Post> undonePosts = new List<Post>();
            undonePosts.Add(getPost(postId));
            while (undonePosts.Count != 0)
            {
                Post post = undonePosts.ElementAt(0);
                undonePosts.RemoveAt(0);
                List<Post> related = DemoDB.getInstance.getRelatedPosts(post.id);
                while (related != null && related.Count != 0)
                {
                    undonePosts.Add(related.ElementAt(0));
                    related.RemoveAt(0);
                }
                donePosts.Add(post);
            }
            //remove all post that in done post
            //check if first post is first in thread
            Post head = getPost(postId);
            if (head.parentId == -1)
            {
                if (!threadController.deleteThread(postId))
                    return false;
            }
            foreach(Post p in donePosts)
            {
                demoDB.removePost(p);
            }

            return true;
        }

        private SubForum getSubforumByPost(int postId)
        {
            Post p = getPost(postId);
            if (p == null)
                return null;
            while (p.parentId != -1)
            {
                p = getPost(p.parentId);
            }
            Thread t= demoDB.getThreadByFirstPostId(p.id);
            return demoDB.getSubforumByThread(t);
            
        }

        private Post getPost(int postId)
        {
            return demoDB.getPost(postId);
        }

        public Boolean addPost(String headLine, String content, String writerName, DateTime timePublished, Int32 commentedPost/*if new thread, -1*/, String forum, String subForum)
        {
            if (headLine.Length <= 0 && content.Length <= 0)
                return false;
            Post newPost = new Post(writerName, demoDB.getAvilableIntOfPost(), headLine, content, commentedPost, timePublished);
            if (!demoDB.addPost(newPost))
                return false;
            if (commentedPost == -1)
            {
                return threadController.addFirstPost(newPost, forum,subForum);
            }
            return true;
        }
    }
}
