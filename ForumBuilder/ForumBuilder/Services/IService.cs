using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.Forums;
using ForumBuilder.Users;

namespace ForumBuilder.Services
{
    interface IService
    {
        Boolean isForumExists(string name);
        Boolean isSubForumExists(string forumName, string subForumName);
        IUser getUser(string userName);
        List<IPost> getRelatedPosts(int postId);
        Boolean createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators);
        Boolean initialize(string userName, string password, string email);
    }
}
