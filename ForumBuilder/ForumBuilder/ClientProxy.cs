using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ClientServiceContracts;

namespace Service
{
    public class ClientProxy : ClientBase<IUserNotificationsService>, IUserNotificationsService
    {
        public Boolean applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {
            return Channel.applyPostPublishedInForumNotification(forumName, subForumName, publisherName);
        }

        public Boolean applyPostModificationNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {
            return Channel.applyPostModificationNotification(forumName, subForumName, publisherName, title, content);
        }

        public Boolean applyPostDelitionNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {
            return Channel.applyPostDelitionNotification(forumName, subForumName, publisherName, title, content);
        }

        public Boolean sendUserMessage(String senderName, String content)
        {
            return Channel.sendUserMessage(senderName, content);
        }

    }
}
