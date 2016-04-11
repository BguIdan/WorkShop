using System;
using ForumBuilder.Controllers;
using ForumBuilder.BL_DB;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.Systems;

namespace ForumBuilder.Controllers
{
    public class SubForumController : ISubForumController
    {
        private static SubForumController singleton;

        DemoDB demoDB = DemoDB.getInstance;
        Logger logger = Logger.getInstance;
        //ForumController forumController = ForumController.getInstance;
        //SuperUserController superUserController = SuperUserController.getInstance;
        public static SubForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SubForumController();
                    Logger.getInstance.logPrint("Sub-forum contoller created");
                }
                return singleton;
            }

        }
        public bool dismissModerator(string dismissedModerator, string dismissByAdmin, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (subForum == null)
            {
                logger.logPrint("Dismiss moderator failed, sub-forum does not exist");
                return false;
            }
            else if (!ForumController.getInstance.isAdmin(dismissByAdmin, forumName))
            {
                logger.logPrint("Dismiss moderator failed, "+ dismissByAdmin+" has no permission");
                return false;
            }
            else if(!isModerator(dismissedModerator, subForumName, forumName))
            {
                logger.logPrint("Dismiss moderator failed, " + dismissedModerator + " is not a moderator");
                return false;
            }
            else
            {
                logger.logPrint("Dismiss moderator "+ dismissedModerator);
                return demoDB.dismissModerator(dismissedModerator, subForumName, forumName);
            }
        }
        public bool isModerator(string name, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (subForum == null)
                return false;
            foreach(string s in subForum.moderators.Keys)
            {
                if (s.Equals(name))
                    return true;
            }
            return false;
            //return subForum.moderators.ContainsKey(name);
        }
        public bool nominateModerator(string newModerator, string nominatorUser, DateTime date, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (ForumController.getInstance.isAdmin(nominatorUser, forumName) && ForumController.getInstance.isMember(newModerator, forumName))
            {
                if (DateTime.Now.CompareTo(date) > 0)
                {
                    logger.logPrint("the date in nominate moderator already past");
                    return false;
                }
                if (demoDB.nominateModerator(newModerator, nominatorUser, date, subForum))
                {
                    logger.logPrint("nominate moderator " + newModerator + "success");
                    return true;
                }
            }
            if(!ForumController.getInstance.isAdmin(nominatorUser, forumName))
                logger.logPrint("To "+nominatorUser+" has no permission to nominate moderator");
            if(!ForumController.getInstance.isMember(newModerator, forumName))
                logger.logPrint("To " + newModerator + " has no permission to be moderator, he is not a member");
            return false;
        }
        public SubForum getSubForum(string subForumName, string forumName)
        {
            return demoDB.getSubForum(subForumName, forumName);
        }




        public bool createThread(String headLine, String content, String writerName,  String forumName, String subForumName)
        {
            DateTime timePublished = DateTime.Now;
            if (headLine.Equals("")&& content.Equals(""))
            {
                logger.logPrint("Create tread failed, there is no head or content in tread");
                return false;
            }
            else if (demoDB.getUser(writerName) == null)
            {
                logger.logPrint("Create tread failed, user does not exist");
                return false;
            }
            else if (demoDB.getSubForum(subForumName,forumName)== null)
            {
                logger.logPrint("Create tread failed, sub-forum does not exist");
                return false;
            }
            else if (!ForumController.getInstance.isMember(writerName, forumName))
            {
                logger.logPrint("Create tread failed, user "+ writerName+" is not memberin forum "+ forumName);
                return false;
            }
            int id = demoDB.getAvilableIntOfPost();
            logger.logPrint("Add thread " + id);
            return demoDB.addPost(writerName, id, headLine, content, -1, timePublished)&&demoDB.addThread(headLine, content, writerName, forumName, subForumName, id,timePublished);
        }

        public bool deleteThread(int firstPostId,string removerName)
        {
            if (demoDB.getThreadByFirstPostId(firstPostId) == null)
            {
                logger.logPrint("Delete thread failed, no thread with that id");
                return false;
            }
            SubForum sf= demoDB.getSubforumByThreadFirstPostId(firstPostId);
            if ((!demoDB.getPost(firstPostId).writerUserName.Equals(removerName))
                &&(!SuperUserController.getInstance.isSuperUser(removerName))
                && (!ForumController.getInstance.isAdmin(removerName, sf.forum)
                && (!isModerator(removerName,sf.name,sf.forum))))
            {
                logger.logPrint("Delete thread failed, there is no permission to that user");
                return false;
            }
            else
            {
                List<Post> donePosts = new List<Post>();
                List<Post> undonePosts = new List<Post>();
                undonePosts.Add(demoDB.getPost(firstPostId));
                while (undonePosts.Count != 0)
                {
                    Post post = undonePosts.ElementAt(0);
                    undonePosts.RemoveAt(0);
                    List<Post> related = demoDB.getRelatedPosts(post.id);
                    while (related != null && related.Count != 0)
                    {
                        undonePosts.Add(related.ElementAt(0));
                        related.RemoveAt(0);
                    }
                    donePosts.Add(post);
                }
                bool hasSucceed= true;
                for(int i =donePosts.Capacity-1; i>=0;i--)
                {
                    hasSucceed = hasSucceed && demoDB.removePost(donePosts.ElementAt(i).id);
                    logger.logPrint("Remove post " + donePosts.ElementAt(i).id);
                }
                logger.logPrint("Remove thread " + firstPostId);
                return hasSucceed && demoDB.removeThread(firstPostId, sf.name, sf.forum);
            } 
        }
    }
}
