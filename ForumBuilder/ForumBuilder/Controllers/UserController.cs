using System;
using ForumBuilder.Users;
using ForumBuilder.BL_DB;
using ForumBuilder.BL_Back_End;

namespace ForumBuilder.Controllers
{
    class UserController : IUserController
    {
        private static UserController singleton;
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
        public bool addFriend(string userName, string friendToAdd)
        {
            throw new NotImplementedException();
        }

        public bool deleteFriend(string userName, string deletedFriend)
        {
            throw new NotImplementedException();
        }

        public bool sendPrivateMessage(string fromUserName, string toUserName, string content, Int32 id)
        {
            User sender = DemoDB.getInstance.getUser(fromUserName);
            User reciver = DemoDB.getInstance.getUser(toUserName);
            if (sender != null && reciver != null && !content.Equals(""))
            {
                Message mess = new Message(id, fromUserName, toUserName, content);
                DemoDB.getInstance.Messages.Add(mess);
                return true;
            }

            return false;
        }

    }
}
