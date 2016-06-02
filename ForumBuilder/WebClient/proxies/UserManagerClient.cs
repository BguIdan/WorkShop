using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;

namespace WebClient.proxies
{
    public class UserManagerClient : ClientBase<IUserManager>, IUserManager
    {
         public UserManagerClient()
        {
        }
    
        public UserManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName)
        {
        }
    
        public UserManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }
    
        public UserManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public UserManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }

        public Boolean addFriend(String userName, String friendToAdd)
        {
            return Channel.addFriend(userName, friendToAdd);
        }

        public Boolean deleteFriend(String userName, String deletedFriend)
        {
            return Channel.deleteFriend(userName, deletedFriend);
        }

        public Boolean sendPrivateMessage(String fromUserName, String toUserName, String content)
        {
            return Channel.sendPrivateMessage(fromUserName, toUserName, content);
        }
        public List<string[]> getAllPrivateMessages(String userName)
        {
            return Channel.getAllPrivateMessages(userName);
        }

        public List<String> getFriendList(String userName)
        {
            return Channel.getFriendList(userName);
        }

        public string restorePassword(string userName, string ans1, string ans2)
        {
            return Channel.restorePassword(userName, ans1, ans2);
        }
    }
}
