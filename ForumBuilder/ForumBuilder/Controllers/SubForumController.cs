using System;
using ForumBuilder.Controllers;
using ForumBuilder.BL_DB;
using System.Collections.Generic;
using System.Linq;

namespace ForumBuilder.Controllers
{
    public class SubForumController : ISubForumController
    {
        private static SubForumController singleton;

        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        ForumController forumController = ForumController.getInstance;
        SuperUserController superUserController = SuperUserController.getInstance;
        public static SubForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SubForumController();
                    Systems.Logger.getInstance.logPrint("Sub-forum contoller created");
                }
                return singleton;
            }

        }
        public bool dismissModerator(string dismissedModerator, string dismissByAdmin, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (forumController.isAdmin(dismissByAdmin, forumName) && forumController.isMember(dismissedModerator, forumName))
            {
                return demoDB.dismissModerator(dismissedModerator, dismissByAdmin, subForum);
            }
            return false;
        }
        public bool isModerator(string name, string subForumName, string forumName)
        {
            if (getSubForum(subForumName, forumName) == null)
                return false;
            return getSubForum(subForumName, forumName).moderators.ContainsKey(name);
        }
        public bool nominateModerator(string newModerator, string nominatorUser, DateTime date, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (forumController.isAdmin(nominatorUser, forumName) && forumController.isMember(newModerator, forumName))
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
            if(!forumController.isAdmin(nominatorUser, forumName))
                logger.logPrint("To "+nominatorUser+" has no permission to nominate moderator");
            if(!forumController.isMember(newModerator, forumName))
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
            else if (!forumController.isMember(writerName, forumName))
            {
                logger.logPrint("Create tread failed, user "+ writerName+" is not memberin forum "+ forumName);
                return false;
            }
            int id = demoDB.getAvilableIntOfPost();
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
                &&(!superUserController.isSuperUser(removerName))
                && (!forumController.isAdmin(removerName, sf.forum)
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
                }
                return hasSucceed && demoDB.removeThread(firstPostId, sf.name, sf.forum);
            } 
        }
    }
}
