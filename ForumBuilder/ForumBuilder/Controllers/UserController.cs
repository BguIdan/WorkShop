using System;
using ForumBuilder.BL_DB;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Controllers
{
    public class UserController : IUserController
    {
        private static UserController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        public static UserController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new UserController();
                }
                return singleton;
            }

        }
        public bool addFriend(string userName, string friendToAddName)
        {
            User user = demoDB.getUser(userName);
            User friendToAdd = demoDB.getUser(friendToAddName);
            if (user == null)
                return false;
            if (friendToAdd == null)
                return false;
            if (demoDB.addFriendToUser(userName, friendToAddName) == false)
                return false;
            return true;
        }

        public bool deleteFriend(string userName, string deletedFriendName)
        {
            User user = demoDB.getUser(userName);
            User friendToAdd = demoDB.getUser(deletedFriendName);
            if (user == null)
                return false;
            if (friendToAdd == null)
                return false;
            if (demoDB.removeFriendOfUser(userName, deletedFriendName) == false)
                return false;
            return true;
        }

        public bool sendPrivateMessage(string fromUserName, string toUserName, string content, Int32 id)
        {
            User sender = demoDB.getUser(fromUserName);
            User reciver = demoDB.getUser(toUserName);
            if (sender != null && reciver != null && !content.Equals(""))
            {
                Message mess = new Message(id, fromUserName, toUserName, content);
                demoDB.Messages.Add(mess);
                return true;
            }

            return false;
        }

    }
}
