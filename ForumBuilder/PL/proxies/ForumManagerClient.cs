using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ForumBuilder.Common.ServiceContracts;
using ForumBuilder.Common.DataContracts;

namespace PL.proxies
{
    public class ForumManagerClient : DuplexClientBase<IForumManager>, IForumManager
    {

        public ForumManagerClient(InstanceContext instanceContext) :
            base(instanceContext)
        {
        }
    
        public ForumManagerClient(InstanceContext instanceContext, string endpointConfigurationName) :
            base(instanceContext, endpointConfigurationName)
        {
        }

        public ForumManagerClient(InstanceContext instanceContext, string endpointConfigurationName, string remoteAddress) :
            base(instanceContext, endpointConfigurationName, remoteAddress)
        {
        }

        public ForumManagerClient(InstanceContext instanceContext, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(instanceContext, endpointConfigurationName, remoteAddress)
        {
        }

        public ForumManagerClient(InstanceContext instanceContext, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(instanceContext, binding, remoteAddress)
        {
        }

        public Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName)
        {
            return Channel.dismissAdmin(adminToDismissed, dismissingUserName, forumName);
        }

        public Boolean banMember(String bannedMember, String bannerUserName, String forumName)
        {
            return Channel.banMember(bannedMember, bannerUserName, forumName);
        }

        public Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName)
        {
            return Channel.nominateAdmin(newAdmin, nominatorName, forumName);
        }

        public Boolean registerUser(String newUser, String password, String mail, String forumName)
        {
            return Channel.registerUser(newUser, password, mail, forumName);
        }

        public Boolean login(String user, String forumName, string password)
        {
            return Channel.login(user, forumName, password);
        }
        
        public Boolean logout(String user, String forumName)
        {
            return Channel.logout(user, forumName);
        }

        public Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin)
        {
            return Channel.addSubForum(forumName, name, moderators, userNameAdmin);
        }

        public Boolean isAdmin(String userName, String forumName)
        {
            return Channel.isAdmin(userName, forumName);
        }

        public Boolean isMember(String userName, String forumName)
        {
            return Channel.isMember(userName, forumName);
        }

        public Boolean setForumPreferences(String forumName, String newDescription, String newForumPolicy, String newForumRules, String setterUserName)
        {
            return Channel.setForumPreferences(forumName, newDescription, newForumPolicy, newForumRules, setterUserName);
        }

        public String getForumPolicy(String forumName)
        {
            return Channel.getForumPolicy(forumName);
        }

        public String getForumDescription(String forumName)
        {
            return Channel.getForumDescription(forumName);
        }

        public String getForumRules(String forumName)
        {
            return Channel.getForumRules(forumName);
        }

        public ForumData getForum(String forumName)
        {
            return Channel.getForum(forumName);
        }
        public List<String> getForums()
        {
            return Channel.getForums();
        }
    }
}
