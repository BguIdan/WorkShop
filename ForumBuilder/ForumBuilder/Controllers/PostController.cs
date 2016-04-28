﻿using System;
using System.Collections.Generic;

using System.Linq;
using BL_Back_End;
using Database;
namespace ForumBuilder.Controllers
{
    public class PostController :IPostController
    {
        private static PostController singleton;
        DBClass DB = DBClass.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        public static PostController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new PostController();
                    Systems.Logger.getInstance.logPrint("Post contoller created");
                }
                return singleton;
            }
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
            Thread t= DB.getThreadByFirstPostId(p.id);
            return DB.getSubforumByThreadFirstPostId(p.id);
        }
        private Post getPost(int postId)
        {
            return DB.getPost(postId);
        }

        public Boolean addComment(String headLine, String content, String writerName, int commentedPost/*if new thread, -1*/)
        {
            SubForum sf= getSubforumByPost(commentedPost);
            if (getPost(commentedPost) == null)
            {
                logger.logPrint("Add comment failed, there is no post to comment at");
                return false;
            }
            if (headLine.Equals("") && content.Equals(""))
            {
                logger.logPrint("Add comment failed, there is no head or content in tread");
                return false;
            }
            else if (DB.getUser(writerName) == null)
            {
                logger.logPrint("Add comment failed, user does not exist");
                return false;
            }
            else if(sf==null|| !ForumController.getInstance.isMember(writerName, sf.forum))
            {
                logger.logPrint("Add comment failed, user is not a member in forum");
                return false;
            }
            else
            {
                int id = DB.getAvilableIntOfPost();
                logger.logPrint("Create comment "+ id+" to "+commentedPost);
                return DB.addPost(writerName, id, headLine, content, commentedPost, DateTime.Now);
            }
        }
        public Boolean removeComment(int postId, String removerName)
        {
            if (getPost(postId) == null)
            {
                logger.logPrint("Delete comment failed, there is no post with that id");
                return false;
            }
            else if (getPost(postId).parentId == -1)
            {
                //logger.logPrint("Delete comment failed, this is not a comment");
                //return false;
                return SubForumController.getInstance.deleteThread(postId, removerName);
            }
            SubForum sf = getSubforumByPost(postId);
            if ((!DB.getPost(postId).writerUserName.Equals(removerName))
                && (DB.getSuperUser(removerName)==null)
                && (!ForumController.getInstance.isAdmin(removerName, sf.forum)
                && (!SubForumController.getInstance.isModerator(removerName, sf.name, sf.forum))))
            {
                logger.logPrint("Delete thread comment, there is no permission to that user");
                return false;
            }

            //find the posts that have to delete
            List<Post> donePosts = new List<Post>();
            List<Post> undonePosts = new List<Post>();
            undonePosts.Add(DB.getPost(postId));
            while (undonePosts.Count != 0)
            {
                Post post = undonePosts.ElementAt(0);
                undonePosts.RemoveAt(0);
                List<Post> related = DB.getRelatedPosts(post.id);
                while (related != null && related.Count != 0)
                {
                    undonePosts.Add(related.ElementAt(0));
                    related.RemoveAt(0);
                }
                donePosts.Add(post);
            }
            bool hasSucceed = true;
            for (int i = donePosts.Count - 1; i >= 0; i--)
            {
                hasSucceed = hasSucceed && DB.removePost(donePosts.ElementAt(i).id);
                logger.logPrint("Remove post " +donePosts.ElementAt(i).id);
            }
            return hasSucceed;
        }
        public List<Post> getAllPosts(String forumName, String subforumName)
        {
            List <Post> list = new List<Post>();
            SubForum sf= SubForumController.getInstance.getSubForum(subforumName, forumName);
            foreach(int t in sf.threads)
            {
                List<Post> donePosts = new List<Post>();
                List<Post> undonePosts = new List<Post>();
                undonePosts.Add(DB.getPost(t));
                while (undonePosts.Count != 0)
                {
                    Post post = undonePosts.ElementAt(0);
                    undonePosts.RemoveAt(0);
                    List<Post> related = DB.getRelatedPosts(post.id);
                    while (related != null && related.Count != 0)
                    {
                        undonePosts.Add(related.ElementAt(0));
                        related.RemoveAt(0);
                    }
                    donePosts.Add(post);
                }
                foreach (Post p in donePosts)
                {
                    list.Add(p);
                }
            }
            return list;
        }
    }
}
