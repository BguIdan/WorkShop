using System;
using ForumBuilder.Users;

namespace ForumBuilder.Controllers
{
    class UserController : IUserController
    {
        public bool addFriend(string userName, string friendToAdd)
        {
            throw new NotImplementedException();
        }

        public bool deleteFriend(string userName, string deletedFriend)
        {
            throw new NotImplementedException();
        }

        public bool sendPrivateMessage(string fromUserName, string toUserName, string content)
        {
            throw new NotImplementedException();
        }
    }
}
