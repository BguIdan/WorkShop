using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Common.ClientServiceContracts;
using System.ServiceModel;

namespace PL.notificationHost
{
    public class ClientNotificationHost : IUserNotificationsService
    {
        public Boolean applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {
            //TODO
            return false;
        }

        public Boolean applyPostModificationNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {
            //TODO
            return false;
        }

        public Boolean applyPostDelitionNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {
            //TODO
            return false;
        }

        public Boolean sendUserMessage(String senderName, String content)
        {
            //TODO
            return false;
        }

    }
}
