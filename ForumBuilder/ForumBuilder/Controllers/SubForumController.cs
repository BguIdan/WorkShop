using System;
using ForumBuilder.Controllers;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.Systems;
using BL_Back_End;
using Database;

namespace ForumBuilder.Controllers
{
    public class SubForumController : ISubForumController
    {
        private static SubForumController singleton;
        ForumController forumController = ForumController.getInstance;
        DBClass DB = DBClass.getInstance;
        Logger logger = Logger.getInstance;

        public static SubForumController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new SubForumController();
                    Logger.getInstance.logPrint("Sub-forum contoller created",0);
                    Logger.getInstance.logPrint("Sub-forum contoller created",1);
                }
                return singleton;
            }
        }
        public bool dismissModerator(string dismissedModerator, string dismissByAdmin, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (subForum == null)
            {
                logger.logPrint("Dismiss moderator failed, sub-forum does not exist",0);
                logger.logPrint("Dismiss moderator failed, sub-forum does not exist",2);
                return false;
            }
            if (DB.getforumByName(subForum.forum).forumPolicy.minNumOfModerators <= subForum.moderators.Count)
            {
                logger.logPrint("Dismiss moderator failed, sub-forum has not enough moderators",0);
                logger.logPrint("Dismiss moderator failed, sub-forum has not enough moderators",2);
                return false;
            }
            else if (!ForumController.getInstance.isAdmin(dismissByAdmin, forumName) && !SuperUserController.getInstance.isSuperUser(dismissByAdmin))
            {
                logger.logPrint("Dismiss moderator failed, "+ dismissByAdmin+" has no permission",0);
                logger.logPrint("Dismiss moderator failed, " + dismissByAdmin + " has no permission",2);
                return false;
            }
            else if(!isModerator(dismissedModerator, subForumName, forumName))
            {
                logger.logPrint("Dismiss moderator failed, " + dismissedModerator + " is not a moderator",0);
                logger.logPrint("Dismiss moderator failed, " + dismissedModerator + " is not a moderator",2);
                return false;
            }
            else
            {
                logger.logPrint("Dismiss moderator "+ dismissedModerator,0);
                logger.logPrint("Dismiss moderator " + dismissedModerator,2);
                return DB.dismissModerator(dismissedModerator, subForumName, forumName);
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
        }
        public bool nominateModerator(string newModerator, string nominatorUser, DateTime date, string subForumName, string forumName)
        {
            SubForum subForum = getSubForum(subForumName, forumName);
            if (subForum == null)
            {
                logger.logPrint("sub forum does not exist",0);
                logger.logPrint("sub forum does not exist",2);
                return false;
            }
            if (DB.getUser(newModerator) == null)
            {
                logger.logPrint("user does not exist", 0);
                logger.logPrint("user does not exist", 2);
                return false;
            }
            if ((ForumController.getInstance.isAdmin(nominatorUser, forumName)|| SuperUserController.getInstance.isSuperUser(nominatorUser)) && 
                ForumController.getInstance.isMember(newModerator, forumName)&&
                DB.getforumByName(forumName).forumPolicy.seniorityInForum<=(DB.getUser(newModerator).date-DateTime.Today).Days)
            {
                if (DateTime.Now.CompareTo(date) > 0)
                {
                    logger.logPrint("the date in nominate moderator already past",0);
                    logger.logPrint("the date in nominate moderator already past",2);
                    return false;
                }
                if (DB.nominateModerator(newModerator, date, subForumName,forumName,nominatorUser))
                {
                    logger.logPrint("nominate moderator " + newModerator + "success",0);
                    logger.logPrint("nominate moderator " + newModerator + "success",1);
                    return true;
                }
            }
            if(!ForumController.getInstance.isAdmin(nominatorUser, forumName)&&!SuperUserController.getInstance.isSuperUser(nominatorUser)){
                logger.logPrint("nominateModerator fail, To " + nominatorUser+" has no permission to nominate moderator",0);
                logger.logPrint("nominateModerator fail, To " + nominatorUser+" has no permission to nominate moderator",2);
                }
            if(!ForumController.getInstance.isMember(newModerator, forumName)){
                logger.logPrint("nominateModerator fail, To " + newModerator + " has no permission to be moderator, he is not a member",0);
                logger.logPrint("nominateModerator fail, To " + newModerator + " has no permission to be moderator, he is not a member",2);
                }
            if(DB.getforumByName(forumName).forumPolicy.seniorityInForum > (DB.getUser(newModerator).date - DateTime.Today).Days){
                logger.logPrint("nominateModerator fail, To " + newModerator + " has not enough seniority",0);
                logger.logPrint("nominateModerator fail, To " + newModerator + " has not enough seniority",2);
                }
            return false;
        }
        public SubForum getSubForum(string subForumName, string forumName)
        {
            return DB.getSubForum(subForumName, forumName);
        }

        public String createThread(String headLine, String content, String writerName,  String forumName, String subForumName)
        {
            DateTime timePublished = DateTime.Now;
            if (headLine==null || content==null||(headLine.Equals("")&& content.Equals("")))
            {
                logger.logPrint("Create tread failed, there is no head or content in tread",0);
                logger.logPrint("Create tread failed, there is no head or content in tread",2);
                return "Create tread failed, there is no head or content in tread";
            }
            else if (DB.getUser(writerName) == null)
            {
                logger.logPrint("Create tread failed, user does not exist",0);
                logger.logPrint("Create tread failed, user does not exist",2);
                return "Create tread failed, user does not exist";
            }
            else if (DB.getSubForum(subForumName,forumName)== null)
            {
                logger.logPrint("Create tread failed, sub-forum does not exist",0);
                logger.logPrint("Create tread failed, sub-forum does not exist",2);
                return "Create tread failed, sub-forum does not exist";
            }
            else if (!ForumController.getInstance.isMember(writerName, forumName))
            {
                logger.logPrint("Create tread failed, user "+ writerName+" is not memberin forum "+ forumName,0);
                logger.logPrint("Create tread failed, user " + writerName + " is not memberin forum " + forumName,2);
                return "Create tread failed, user " + writerName + " is not memberin forum " + forumName;
            }
            int id = DB.getAvilableIntOfPost();
            logger.logPrint("Add thread " + id,0);
            logger.logPrint("Add thread " + id,1);
            this.forumController.sendThreadCreationNotification(headLine, content, writerName, forumName, subForumName);
            if (DB.addPost(writerName, id, headLine, content, -1, timePublished, forumName) && DB.addThread(forumName, subForumName, id))
                return "Create tread succeed";
            return "Create tread failed";

        }

        public String deleteThread(int firstPostId,string removerName)
        {
            if (DB.getThreadByFirstPostId(firstPostId) == null)
            {
                logger.logPrint("Delete thread failed, no thread with that id",0);
                logger.logPrint("Delete thread failed, no thread with that id",2);
                return "Delete thread failed, no thread with that id";
            }
            SubForum sf= DB.getSubforumByThreadFirstPostId(firstPostId);
            if ((!DB.getPost(firstPostId).writerUserName.Equals(removerName))
                &&(!SuperUserController.getInstance.isSuperUser(removerName))
                && (!ForumController.getInstance.isAdmin(removerName, sf.forum)
                && (!isModerator(removerName,sf.name,sf.forum))))
            {
                logger.logPrint("Delete thread failed, there is no permission to that user",0);
                logger.logPrint("Delete thread failed, there is no permission to that user",2);
                return "Delete thread failed, there is no permission to that user";
            }
            else
            {
                List<Post> donePosts = new List<Post>();
                List<Post> undonePosts = new List<Post>();
                undonePosts.Add(DB.getPost(firstPostId));
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
                DB.removeThread(firstPostId);
                for (int i =donePosts.Count-1; i>=0;i--)
                {
                    DB.removePost(donePosts.ElementAt(i).id);
                    logger.logPrint("Remove post " + donePosts.ElementAt(i).id,0);
                    logger.logPrint("Remove post " + donePosts.ElementAt(i).id,1);
                }
                logger.logPrint("Remove thread " + firstPostId,0);
                logger.logPrint("Remove thread " + firstPostId,1);
                return "Thread removed";
            } 
        }
    }
}
