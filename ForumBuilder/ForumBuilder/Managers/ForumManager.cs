using ForumBuilder.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Common.ServiceContracts;
using BL_Back_End;
using ForumBuilder.Common.DataContracts;

namespace Service
{
    public class ForumManager : IForumManager
    {
        private static ForumManager singleton;
        private IForumController forumController;

        private ForumManager()
        {
            forumController=ForumController.getInstance;
        }

        public static ForumManager getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ForumManager();
                }
                return singleton;
            }
        }

        public Boolean dismissAdmin(String adminToDismissed, String dismissingUserName, String forumName)
        {
            return forumController.dismissAdmin(adminToDismissed, dismissingUserName, forumName);
        }
        public Boolean banMember(String bannedMember, String bannerUserName, String forumName)
        {
            return forumController.banMember(bannedMember, bannerUserName, forumName);
        }
        public Boolean nominateAdmin(String newAdmin, String nominatorName, String forumName)
        {
            return forumController.nominateAdmin(newAdmin, nominatorName, forumName);
        }
        public Boolean registerUser(String newUser, String password, String mail, string forumName)
        {
            return forumController.registerUser(newUser, password, mail, forumName);
        }

        public Boolean login(String user, String forumName,string password)
        {
            return forumController.login(user, forumName, password);
        }

        public Boolean logout(String user, String forumName)
        {
            return forumController.logout(user, forumName);
        }

        public Boolean addSubForum(String forumName, String name, Dictionary<String, DateTime> moderators, String userNameAdmin)
        {
            return forumController.addSubForum(forumName, name, moderators, userNameAdmin);
        }
        public Boolean isAdmin(String userName, String forumName)
        {
            return forumController.isAdmin(userName, forumName);
        }
        public Boolean isMember(String userName, String forumName)
        {
            return forumController.isMember(userName, forumName);
        }
        public Boolean setForumPreferences(String forumName, String newDescription, string policy, bool isQuestionIdentifying,
            int seniorityInForum, bool deletePostByModerator, int timeToPassExpiration, int minNumOfModerators,
            bool hasCapitalInPassword, bool hasNumberInPassword, int minLengthOfPassword, String setterUserName)
        {
            return forumController.setForumPreferences(forumName, newDescription, policy, isQuestionIdentifying, seniorityInForum, deletePostByModerator,
                    timeToPassExpiration, minNumOfModerators, hasCapitalInPassword, hasNumberInPassword, minLengthOfPassword, setterUserName);
        }
        public String getForumPolicy(String forumName)
        {
            return forumController.getForumPolicy(forumName);
        }
        public String getForumDescription(String forumName)
        {
            return forumController.getForumDescription(forumName);
        }
       
        public ForumData getForum(String forumName)
        {
            Forum temp = forumController.getForum(forumName);
            ForumData toReturn = new ForumData(temp.forumName , temp.description , temp.forumPolicy.policy , temp.subForums,temp.members);
            return toReturn;
            
        }
        public List<String> getForums()
        {
            return forumController.getForums();
        }
        public int getAdminReportNumOfPOst(String AdminName, String forumName)
        {
            return forumController.getAdminReportNumOfPOst( AdminName, forumName);
        }

        public List<PostData> getAdminReportPostOfmember(String AdminName, String forumName, String memberName)
        {
            List<Post> posts = forumController.getAdminReportPostOfmember(AdminName, forumName, memberName);
            List<PostData> postsData = new List<PostData>();
            foreach (Post p in posts)
            {
                postsData.Add(new PostData(p.writerUserName, p.id, p.title, p.content, p.parentId, p.timePublished));
            }
            return postsData;
        }

        public List<String> getAdminReport(String AdminName, String forumName)
        {
            return forumController.getAdminReport(AdminName, forumName);
        }
    }
}
