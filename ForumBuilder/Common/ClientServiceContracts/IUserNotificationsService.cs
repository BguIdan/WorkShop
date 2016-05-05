using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ForumBuilder.Common.ClientServiceContracts
{
    [ServiceContract]
    public interface IUserNotificationsService
    {

        [OperationContract]
        Boolean applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName);

        [OperationContract]
        Boolean applyPostModificationNotification(String forumName, String subForumName, String publisherName, String title, String content);

        [OperationContract]
        Boolean applyPostDelitionNotification(String forumName, String subForumName, String publisherName, String title, String content);

        [OperationContract]
        Boolean sendUserMessage(String senderName, String content);
    }
}
