using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Controllers;
namespace Service
{
    class UserManager:IUserManager
    {
        private static UserManager singleton;
        private IUserController userController;

        private UserManager()
        {
            userController = UserController.getInstance;
        }

        public static UserManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new UserManager();
                }
                return singleton;
            }
        }

        public Boolean addFriend(String userName, String friendToAdd)
        {
            return userController.addFriend(userName, friendToAdd);
        }
        public Boolean deleteFriend(String userName, String deletedFriend)
        {
            return userController.deleteFriend(userName, deletedFriend);
        }
        public Boolean sendPrivateMessage(String fromUserName, String toUserName, String content, Int32 id)
        {
            return userController.sendPrivateMessage(fromUserName, toUserName, content, id);
        }
    }
}
