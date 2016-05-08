using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ClientServiceContracts;

namespace Service
{
    public class ClientProxy /*: ClientBase<IUserNotificationsService>, IUserNotificationsService*/
    {/*//TODO gal: consider removal since the callback mechanism is used
        public void applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {
            Channel.applyPostPublishedInForumNotification(forumName, subForumName, publisherName);
        }

        public void applyPostModificationNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {
            Channel.applyPostModificationNotification(forumName, subForumName, publisherName, title, content);
        }

        public void applyPostDelitionNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {
            Channel.applyPostDelitionNotification(forumName, subForumName, publisherName, title, content);
        }

        public void sendUserMessage(String senderName, String content)
        {
            Channel.sendUserMessage(senderName, content);
        }*/

    }
}
