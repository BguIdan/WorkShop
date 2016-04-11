using System;
using System.Collections.Generic;
using ForumBuilder.BL_DB;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Controllers
{
    public class UserController : IUserController
    {
        private static UserController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        //ForumController forumController = ForumController.getInstance;

        public static UserController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new UserController();
                    Systems.Logger.getInstance.logPrint("User contoller created");
                }
                return singleton;
            }

        }
        public bool addFriend(string userName, string friendToAddName)
        {
            User user = demoDB.getUser(userName);
            User friendToAdd = demoDB.getUser(friendToAddName);
            if (user == null)
            {
                logger.logPrint("Add friend faild, " + userName + "is not a user");
                return false;
            }
            if (friendToAdd == null)
            {
                logger.logPrint("Add friend faild, " + friendToAddName + "is not a user");
                return false;
            }
            if(!ForumController.getInstance.isMembersOfSameForum(friendToAddName, userName))
            {
                logger.logPrint("Add friend faild, " + friendToAddName + " and "+userName + " are not in the same forum");
                return false;
            }
            return demoDB.addFriendToUser(userName, friendToAddName);            
        }

        public bool deleteFriend(string userName, string deletedFriendName)
        {
            User user = demoDB.getUser(userName);
            User friendTodelete = demoDB.getUser(deletedFriendName);
            if (user == null)
            {
                logger.logPrint("Remove friend faild, " + userName + "is not a user");
                return false;
            }
            if (friendTodelete == null)
            {
                logger.logPrint("Remove friend faild, " + deletedFriendName + "is not a user");
                return false;
            }
            if (!getFriendList(userName).Contains(deletedFriendName))
            {
                logger.logPrint("Remove friend faild, " + userName + " and " + deletedFriendName + " are not friends");
                return false;
            }
            return demoDB.removeFriendOfUser(userName, deletedFriendName);
        }

        public bool sendPrivateMessage(string fromUserName, string toUserName, string content, int id)
        {
            User sender = demoDB.getUser(fromUserName);
            User reciver = demoDB.getUser(toUserName);
            if (sender == null)
            {
                logger.logPrint("Send message faild, " + fromUserName + "is not a user");
                return false;
            }
            else if (reciver == null)
            {
                logger.logPrint("Send message faild, " + toUserName + "is not a user");
                return false;
            }
            else if (!ForumController.getInstance.isMembersOfSameForum(fromUserName, toUserName))
            {
                logger.logPrint("Send message faild, " + fromUserName + " and " + toUserName + " are not in the same forum");
                return false;
            }
            else if (content.Equals(""))
            {
                logger.logPrint("Send message faild, no content in message");
                return false;
            }
            else
                return demoDB.addMessage(id, fromUserName, toUserName, content);
        }

        public List<String> getFriendList(String userName)
        {
            if (demoDB.getUser(userName) == null)
                return null;
            return demoDB.getUserFriends(userName);
        }

    }
}
