using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Common.ServiceContracts;
using Database;
using System.ServiceModel;
using PL.notificationHost;
using PL.proxies;
using ForumBuilder.Common.DataContracts;
using ForumBuilder.Controllers;
using Service;

namespace Tests
{
    [TestClass]
    public class AcceptanceTests
    {
        

        /*************************use case 2******************************/

        [TestMethod]
        public void AT_test_create_and_manipulate_forum()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName); 
            String forumName = forumData.forumName;
            String adminName = userAdmin.userName;

            Assert.IsTrue(forum.registerUser("mem", "mempass", "mem@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser("mem2", "mempass", "mem@gmail.com", forumName));
            Assert.IsTrue(forum.isMember("mem2", forumName), "userMember should be a member");
            Assert.IsTrue(forum.banMember("mem2", adminName, forumName), "ban of userMember should be successful");
            Assert.IsFalse(forum.isMember("mem2", forumName), "userMember should not be a member when banned");
            Assert.IsFalse(forum.isMember("mem2", forumName), "userMember should not be a member when banned");
            Assert.IsFalse(forum.isMember("nonMem", forumName), "userNonMember should not be a member");
            Assert.IsTrue(forum.registerUser("nonMem", "pass", "email", forumName), "registration of a non member should be successful");
            Assert.IsTrue(forum.isMember("nonMem", forumName), "after registration the user should become a member");
            Assert.IsTrue(forum.addSubForum(forumName, "sub", new Dictionary<String, DateTime>(), adminName));
            Assert.IsTrue(forum.isAdmin(adminName, forumName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(forum.dismissAdmin(adminName, "tomer", forumName), "userAdmin is an administrator in the forum. his dismissal from being administrator should be successful");
            Assert.IsFalse(forum.isAdmin(adminName, forumName), "userAdmin should not be a administrator in the forum");
        }

        /*************************end use case 2******************************/


        /*************************use case 3******************************/

        [TestMethod]
        public void AT_test_changeForumPreferences_valid_policy()
        {
            
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            String oldPolicy = forum.getForumPolicy(forumData.forumName);
            String newPolicy = "new policy for test";
            String oldDescription = forum.getForumDescription(forumData.forumName);
            String newDescr = "new description";
            String newRules = "there are no rules";
            Assert.AreNotEqual(oldPolicy, newPolicy, false, "the new policy should be different from the old one");
            Assert.AreNotEqual(oldDescription, newDescr, false, "the new description should be different from the old one");
            Assert.IsTrue(forum.setForumPreferences(forumData.forumName, newDescr, newPolicy, newRules, userAdmin.userName), "policy change should be successful");
            Assert.AreEqual(forum.getForumPolicy(forumData.forumName), newPolicy, false, "the new policy should be return after the change");
            Assert.AreEqual(forum.getForumDescription(forumData.forumName), newDescr, false, "the new description should be return after the change");
            db.clear();
        }

        [TestMethod]
        public void AT_test_changeForumPreferences_with_null_inputs()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            String oldPolicy = forum.getForumPolicy(forumData.forumName);
            String oldDescr = forum.getForumDescription(forumData.forumName);
            Assert.IsFalse(forum.setForumPreferences(forumData.forumName, null, null, null, forumData.forumName), "policy change with null should not be successful");
            Assert.AreEqual(forum.getForumPolicy(forumData.forumName), oldPolicy, false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(forum.getForumDescription(forumData.forumName), oldDescr, false, "after an unsuccessful change, the old description should be returned");
            db.clear();

        }


        [TestMethod]
        public void AT_test_changeForumPreferences_with_empty_string()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            String oldPolicy = forum.getForumPolicy(forumData.forumName);
            String oldDescr = forum.getForumDescription(forumData.forumName);
            Assert.IsTrue(forum.setForumPreferences(forumData.forumName, "", "", "", userAdmin.userName), "policy change with null should not be successful");
            Assert.AreEqual(forum.getForumPolicy(forumData.forumName), "", false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(forum.getForumDescription(forumData.forumName), "", false, "after an unsuccessful change, the old description should be returned");
            db.clear();
        }

        /*************************end use case 3******************************/

        /*************************use case 4******************************/

        [TestMethod]
        public void AT_Test_register_to_forum_withWrongInputs()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forumMan = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            Assert.IsFalse(forumMan.registerUser("admin", "passWord2", "jkkkk@xc.com", forumData.forumName));
            Assert.IsTrue(forumMan.registerUser("mem1", "passWor1", "fff@xc.com", forumData.forumName));
            Assert.IsTrue(forumMan.registerUser("mem2", "passWor1", "fff@xc.com", forumData.forumName));
            Assert.IsFalse(forumMan.registerUser("", "passWor1", "fff@xc.com", forumData.forumName));
            Assert.IsFalse(forumMan.registerUser("mem2", "", "fff@xc.com", forumData.forumName));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "", forumData.forumName));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "fff@xc.com", forumData.forumName));
        }

        [TestMethod]
        public void AT_Test_register_to_forum_Functionality()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forumMan = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin1", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            IUserManager userMan = new UserManagerClient();
            Assert.IsFalse(userMan.sendPrivateMessage("admin1", "admin2", "hello"));
            Assert.IsFalse(userMan.addFriend("admin1", "admin2"));
            Assert.IsFalse(userMan.addFriend("admin2", "admin1"));
            Assert.IsTrue(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", forumData.forumName));
            Assert.IsTrue(userMan.sendPrivateMessage("admin1", "admin2", "its me"));
            Assert.IsTrue(userMan.addFriend("admin1", "admin2"));
            Assert.IsTrue(userMan.addFriend("admin2", "admin1"));
            Assert.IsFalse(userMan.addFriend("admin2", "admin1"));
            Assert.IsFalse(userMan.addFriend("admin1", "mem1"));
            Assert.IsFalse(userMan.addFriend("mem1", "admin1"));
            Assert.IsFalse(userMan.sendPrivateMessage("mem1", "admin1", "i was wonder"));
            Assert.IsTrue(forumMan.registerUser("mem1", "passWor1", "fff@xc.com", forumData.forumName));
            Assert.IsTrue(userMan.addFriend("admin1", "mem1"));
            Assert.IsTrue(userMan.addFriend("mem1", "admin1"));
            Assert.IsTrue(userMan.sendPrivateMessage("mem1", "admin1", "when the test gona be done"));
        }

        /*************************end use case 4******************************/

        /*************************use case 5+9******************************/
        
        [TestMethod]
        public void AT_test_create_subForum_and_nominate_moderator()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin1", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            String subForumName = "sub";
            Assert.IsTrue(forum.addSubForum(forumData.forumName, subForumName, new Dictionary<String, DateTime>(), userAdmin.userName));

            Assert.IsTrue(forum.registerUser("mem", "mempasS1", "mem@gmail.com", forumData.forumName));

            ISubForumManager subForum = new SubForumManagerClient();
            Assert.IsTrue(subForum.nominateModerator("mem", userAdmin.userName, new DateTime(2030, 1, 1), subForumName, forumData.forumName), "nomination of member user should be successful");
            Assert.IsTrue(SubForumController.getInstance.isModerator("mem", subForumName, forumData.forumName), "member should be moderator after his successful numonation");
        }

        /*************************end use case 5+9******************************/

        /*************************use case 6******************************/

/*        [TestMethod]
        public void AT_test_add_thread_and_post()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            SuperUserController superUserController = SuperUserController.getInstance;
            UserData superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserController.addSuperUser(superUser.email, superUser.password, superUser.userName);
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ForumData forumData = new ForumData("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", new List<String>(), new List<String>());
            UserData userAdmin = new UserData("admin1", "adminpass", "admin@gmail.com");
            superUserController.addUser(userAdmin.userName, userAdmin.password, userAdmin.email, superUser.userName);
            List<string> adminList = new List<string>();
            adminList.Add(userAdmin.userName);
            superUserController.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, superUser.userName);
            String subForumName = "sub";

            String forumName = forumData.forumName;
            String adminName = userAdmin.userName;
            String userMemberName = "mem";
            String moderatorName = "mod";
            ISubForumManager subForum = new SubForumManagerClient();
            IPostManager post = new PostManagerClient();

            Assert.IsTrue(forum.addSubForum(forumName, subForumName, new Dictionary<String, DateTime>(), adminName));

            Assert.IsTrue(forum.registerUser(userMemberName, "mempass", "mem@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser(moderatorName, "modpass", "mod@gmail.com", forumName));

            Assert.IsTrue(SubForumManager.getInstance.nominateModerator(moderatorName, adminName, new DateTime(2030, 1, 1), subForumName, forumName), "nomination of member user should be successful");
//            Assert.IsTrue(SubForumManager.getInstance.isModerator(moderatorName, subForumName, forumName), "member should be moderator after his successful numonation");

            Assert.IsTrue(forum.addSubForum(forumName, subForumName, new Dictionary<String, DateTime>(), adminName));
            Assert.IsTrue(subForum.createThread("headLine", "content", userMemberName, forumName, subForumName));
            List<PostData> posts = post.getAllPosts(forumName, subForumName);
            int postId = posts[0].id;
            Assert.IsTrue(subForum.createThread("headLine", "content", userMemberName, forumName, subForumName));
            Assert.AreEqual(posts.Count, 1);

            Assert.IsTrue(post.addPost("headLine2", "content2", adminName, postId));
            Assert.AreEqual(posts.Count, 2);

            Assert.IsTrue(post.addPost("headLine3", "content3", userMemberName, postId));
            Assert.AreEqual(posts.Count, 3);
        }
        */
        /*************************end use case 6******************************/


    }
}
