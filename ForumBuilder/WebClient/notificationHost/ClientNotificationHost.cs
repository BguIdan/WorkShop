using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Common.ClientServiceContracts;
using System.ServiceModel;
using System.Windows;
using System.Web.UI;
using System.Web;

namespace WebClient.notificationHost
{
   /* public class ClientNotificationHost : IUserNotificationsService
    {
        public void applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            showAlert("new post<br>" + publisherName + " published a post in " + forumName + 
                    "'s sub-forum " + subForumName, page);
         }

        public void applyPostModificationNotification(String forumName, String publisherName, String title, String content)
        {
                //MessageBox.Show(publisherName + "'s post you were following in " + forumName + "was modified (" + title + ")", "post modified");
        }

        public void applyPostDelitionNotification(String forumName, String publisherName)
        {
                //MessageBox.Show(publisherName + "'s post you were following in " + forumName + "was deleted", "post deleted");
        }

        public void sendUserMessage(String senderName, String content)
        {
            //MessageBox.Show(content, senderName + " set you a message");
        }

        private void showAlert(String content, Page p)
        {
            p.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }

    }*/
}
