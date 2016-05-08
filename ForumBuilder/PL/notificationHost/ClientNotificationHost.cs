using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Common.ClientServiceContracts;
using System.ServiceModel;
using System.Windows;

namespace PL.notificationHost
{
    public class ClientNotificationHost : IUserNotificationsService
    {
        public void applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {//TODO make the sub forum update?
                MessageBox.Show(publisherName + " published a post in " + forumName + 
                    "'s sub-forum " + subForumName, "new post");
         }

        public void applyPostModificationNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {//TODO make the post update?
                MessageBox.Show(publisherName + "'s post you were following in " + forumName + 
                    "'s sub-forum " + subForumName + "was modified", "post modified");
        }

        public void applyPostDelitionNotification(String forumName, String subForumName, String publisherName, String title, String content)
        {//TODO make the post update?
                MessageBox.Show(publisherName + "'s post you were following in " + forumName +
                    "'s sub-forum " + subForumName + "was modified", "post modified");
        }

        public void sendUserMessage(String senderName, String content)
        {
            //TODO
        }

    }
}
