using ForumBuilder.Controllers;
using System;
using System.Collections.Generic;
using ForumBuilder.Systems;

namespace Service
{
    class ServiceClass
    {
        private static ServiceClass singleton;
        private ForumController forumController;
        private SubForumController subForumController;
        private ThreadController threadController;
        private PostController postController;
        private UserController userController;
        private SuperUserController superUserController;

        private ServiceClass()
        {
            forumController = ForumController.getInstance;
            subForumController = SubForumController.getInstance;
            threadController = ThreadController.getInstance;
            postController = PostController.getInstance;
            userController = UserController.getInstance;
            superUserController = SuperUserController.getInstance;
        }

        public static ServiceClass getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ServiceClass();
                }
                return singleton;
            }

        }

        // IForumController
        public Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName)
        {
            return forumController.dismissAdmin(adminToDismissed, dismissingUserName, forumName);
        }
        public Boolean banMember(String bannedMember, String bannerUserName, String forumName)
        {
            return forumController.banMember(bannedMember, bannerUserName, forumName);
        }
        public Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName)
        {
            return forumController.nominateAdmin(newAdmin, nominatorName, forumName);
        }
        public Boolean registerUser(String newUser, String password, String mail, string forumName)
        {
            return forumController.registerUser(newUser, password, mail, forumName);
        }
        public Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin)
        {
            return forumController.addSubForum(forumName, name, moderators, userNameAdmin);
        }
        public Boolean isAdmin(String userName, String forumName)
        {
            return forumController.isAdmin(userName, forumName);
        }
        public Boolean isMember(String userName, String forumName)
        {
            return forumController.isMember(userName, forumName);
        }
        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, String setterUserName)
        {
            return forumController.setForumPreferences(forumName, newDescription, newForumPolicy, newForumRules, setterUserName);
        }
        public String getForumPolicy(String forumName)
        {
            return forumController.getForumPolicy(forumName);
        }
        public String getForumDescription(String forumName)
        {
            return forumController.getForumDescription(forumName);
        }
        public String getForumRules(String forumName)
        {
            return forumController.getForumRules(forumName);
        }

        // IUserController
        public Boolean addFriend(String userName, String friendToAdd)
        {
            return userController.addFriend(userName,friendToAdd);
        }
        public Boolean deleteFriend(String userName, String deletedFriend)
        {
            return userController.deleteFriend(userName, deletedFriend);
        }
        public Boolean sendPrivateMessage(String fromUserName, String toUserName, String content, Int32 id)
        {
            return userController.sendPrivateMessage(fromUserName, toUserName, content, id);
        }

        // IPostController
        public Boolean deletePost(Int32 postId, String deletingUser)
        {
            return postController.deletePost(postId, deletingUser);
        }
        public Boolean addPost(String headLine, String content, String writerName, DateTime timePublished, Int32 commentedPost, String forum, String subForum)
        {
            return postController.addPost(headLine, content, writerName, timePublished, commentedPost, forum, subForum);
        }

        // ISubForumController
        public Boolean dismissModerator(String dismissedModerator, String dismissByAdmin, String subForumName, String forumName)
        {
            return subForumController.dismissModerator(dismissedModerator, dismissByAdmin, subForumName, forumName);
        }
        public Boolean nominateModerator(String newModerator, String nominatorUser, DateTime date, String subForumName, String forumName)
        {
            return subForumController.nominateModerator(newModerator, nominatorUser, date, subForumName, forumName);
        }

        // ISuperUser
        public Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName)
        {
            return superUserController.createForum(forumName, descrption, forumPolicy, forumRules, administrators, superUserName);
        }

        public static int Main(string[] args)
        {
            ForumSystem.initialize("name", "pass", "email");
            return -1;
        }
    }
}
