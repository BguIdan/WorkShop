using System;
using System.Collections.Generic;
using BL_Back_End;
using Database;

namespace ForumBuilder.Controllers
{
    public class UserController : IUserController
    {
        private static UserController singleton;
        DBClass DB = DBClass.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;

        public static UserController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new UserController();
                    Systems.Logger.getInstance.logPrint("User contoller created",0);
                    Systems.Logger.getInstance.logPrint("User contoller created",1);
                }
                return singleton;
            }

        }
        
        public bool addFriend(string userName, string friendToAddName)
        {
            User user = DB.getUser(userName);
            User friendToAdd = DB.getUser(friendToAddName);
            if (user == null)
            {
                logger.logPrint("Add friend faild, " + userName + "is not a user",0);
                logger.logPrint("Add friend faild, " + userName + "is not a user",2);
                return false;
            }
            if (friendToAdd == null)
            {
                logger.logPrint("Add friend faild, " + friendToAddName + "is not a user",0);
                logger.logPrint("Add friend faild, " + friendToAddName + "is not a user",2);
                return false;
            }
            if(!ForumController.getInstance.isMembersOfSameForum(friendToAddName, userName))
            {
                logger.logPrint("Add friend faild, " + friendToAddName + " and "+userName + " are not in the same forum",0);
                logger.logPrint("Add friend faild, " + friendToAddName + " and " + userName + " are not in the same forum",2);
                return false;
            }
            return DB.addFriendToUser(userName, friendToAddName);            
        }

        public bool deleteFriend(string userName, string deletedFriendName)
        {
            User user = DB.getUser(userName);
            User friendTodelete = DB.getUser(deletedFriendName);
            if (user == null)
            {
                logger.logPrint("Remove friend faild, " + userName + "is not a user",0);
                logger.logPrint("Remove friend faild, " + userName + "is not a user",2);
                return false;
            }
            if (friendTodelete == null)
            {
                logger.logPrint("Remove friend faild, " + deletedFriendName + "is not a user",0);
                logger.logPrint("Remove friend faild, " + deletedFriendName + "is not a user",2);
                return false;
            }
            if (!getFriendList(userName).Contains(deletedFriendName))
            {
                logger.logPrint("Remove friend faild, " + userName + " and " + deletedFriendName + " are not friends",0);
                logger.logPrint("Remove friend faild, " + userName + " and " + deletedFriendName + " are not friends",2);
                return false;
            }
            return DB.removeFriendOfUser(userName, deletedFriendName);
        }

        public bool sendPrivateMessage(string fromUserName, string toUserName, string content)
        {
            User sender = DB.getUser(fromUserName);
            User reciver = DB.getUser(toUserName);
            if (sender == null)
            {
                logger.logPrint("Send message faild, " + fromUserName + "is not a user",0);
                logger.logPrint("Send message faild, " + fromUserName + "is not a user",2);
                return false;
            }
            else if (reciver == null)
            {
                logger.logPrint("Send message faild, " + fromUserName + "is not a user",0);
                logger.logPrint("Send message faild, " + fromUserName + "is not a user",2);
                return false;
            }
            else if (!ForumController.getInstance.isMembersOfSameForum(fromUserName, toUserName))
            {
                logger.logPrint("Send message faild, " + fromUserName + " and " + toUserName + " are not in the same forum",0);
                logger.logPrint("Send message faild, " + fromUserName + " and " + toUserName + " are not in the same forum",2);
                return false;
            }
            else if (content.Equals(""))
            {
                logger.logPrint("Send message faild, no content in message",0);
                logger.logPrint("Send message faild, no content in message",2);
                return false;
            }
            else
                return DB.addMessage(fromUserName, toUserName, content);
        }

        public List<String> getFriendList(String userName)
        {
            if (DB.getUser(userName) == null)
                return null;
            return DB.getUserFriends(userName);
        }

        public List<string[]> getAllPrivateMessages(string userName)
        {
            List<Message> messageList = DB.getMessagesOfUserAsReciver(userName);
            List<string[]> messagesOfWantedUser = new List<string[]>();
            foreach(Message msg in messageList)
            {
                string[] messageAsStringArray = { msg.sender, msg.Content };
                messagesOfWantedUser.Add(messageAsStringArray);
            }
            return messagesOfWantedUser;
        }
        
        public string restorePassword(string userName, string ans1, string ans2)
        {
            string password = null;
            List<String> answers = DB.getAnswers(userName);
            if (ans1.Equals(answers[0]) && ans2.Equals(answers[1]))
            {
                password = DB.getPassword(userName);
            }
            return password;
        }
    }
}
