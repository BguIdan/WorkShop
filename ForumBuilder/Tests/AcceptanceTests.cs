﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Common.ServiceContracts;
using Database;
using System.ServiceModel;
using PL.notificationHost;
using PL.proxies;
using ForumBuilder.Common.DataContracts;

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
            ISuperUserManager superUser = new SuperUserManagerClient();
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost())); 
            String forumName = "forum";
            String adminName = "admin";
            List<string> adminList = new List<string>();
            adminList.Add(adminName);
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");
            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser("mem", "mempass", "mem@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser("mem2", "mempass", "mem@gmail.com", forumName));
            Assert.IsTrue(forum.isMember("mem2", forumName), "userMember should be a member");
            Assert.IsTrue(forum.banMember("mem2", adminName, forumName), "ban of userMember should be successful");
            Assert.IsFalse(forum.isMember("mem2", forumName), "userMember should not be a member when banned");
            Assert.IsFalse(forum.registerUser("mem2", "mempass", "mem@gmail.com", forumName), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(forum.isMember("mem2", forumName), "userMember should not be a member when banned");
            Assert.IsFalse(forum.isMember("nonMem", forumName), "userNonMember should not be a member");
            Assert.IsTrue(forum.registerUser("nonMem", "pass", "email", forumName), "registration of a non member should be successful");
            Assert.IsTrue(forum.isMember("nonMem", forumName), "after registration the user should become a member");
            Assert.IsTrue(forum.addSubForum(forumName, "sub", new Dictionary<String, DateTime>(), adminName));
            Assert.IsTrue(forum.isAdmin(adminName, forumName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(forum.dismissAdmin(adminName, "guy", forumName), "userAdmin is an administrator in the forum. his dismissal from being administrator should be successful");
            Assert.IsFalse(forum.isAdmin(adminName, forumName), "userAdmin should not be a administrator in the forum");
            db.clear();
        }

        /*************************end use case 2******************************/


        /*************************use case 3******************************/

        [TestMethod]
        public void AT_test_changeForumPreferences_valid_policy()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            ISuperUserManager superUser = new SuperUserManagerClient();
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost())); 
            String forumName = "forum";
            String adminName = "admin";
            List<string> adminList = new List<string>();
            adminList.Add(adminName);
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");
            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            String oldPolicy = forum.getForumPolicy(forumName);
            String newPolicy = "new policy for test";
            String oldDescription = forum.getForumDescription(forumName);
            String newDescr = "new description";
            String oldRules = forum.getForumRules(forumName);
            String newRules = "there are no rules";
            Assert.AreNotEqual(oldPolicy, newPolicy, false, "the new policy should be different from the old one");
            Assert.AreNotEqual(oldDescription, newDescr, false, "the new description should be different from the old one");
            Assert.AreNotEqual(oldRules, newRules, false, "the new rules should be different from the old one");
            Assert.IsTrue(forum.setForumPreferences(forumName, newDescr, newPolicy, newRules, adminName), "policy change should be successful");
            Assert.AreEqual(forum.getForumPolicy(forumName), newPolicy, false, "the new policy should be return after the change");
            Assert.AreEqual(forum.getForumDescription(forumName), newDescr, false, "the new description should be return after the change");
            Assert.AreEqual(forum.getForumRules(forumName), newRules, false, "the new rules should be return after the change");
            db.clear();
        }

        [TestMethod]
        public void AT_test_changeForumPreferences_with_null_inputs()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            ISuperUserManager superUser = new SuperUserManagerClient();
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost())); 
            String forumName = "forum";
            String adminName = "admin";
            List<String> adminList = new List<String>();
            adminList.Add(adminName);
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");
            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            String oldPolicy = forum.getForumPolicy(forumName);
            String oldDescr = forum.getForumDescription(forumName);
            String oldRules = forum.getForumRules(forumName);
            Assert.IsFalse(forum.setForumPreferences(forumName, null, null, null, adminName), "policy change with null should not be successful");
            Assert.AreEqual(forum.getForumPolicy(forumName), oldPolicy, false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(forum.getForumDescription(forumName), oldDescr, false, "after an unsuccessful change, the old description should be returned");
            Assert.AreEqual(forum.getForumRules(forumName), oldRules, false, "after an unsuccessful change, the old rules should be returned");
            db.clear();

        }


        [TestMethod]
        public void AT_test_changeForumPreferences_with_empty_string()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            ISuperUserManager superUser = new SuperUserManagerClient();
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            String forumName = "forum";
            String adminName = "admin";
            List<String> adminList = new List<String>();
            adminList.Add(adminName);
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");
            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            String oldPolicy = forum.getForumPolicy(forumName);
            String oldDescr = forum.getForumDescription(forumName);
            String oldRules = forum.getForumRules(forumName);
            Assert.IsTrue(forum.setForumPreferences(forumName, "", "", "", adminName), "policy change with null should not be successful");
            Assert.AreEqual(forum.getForumPolicy(forumName), "", false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(forum.getForumDescription(forumName), "", false, "after an unsuccessful change, the old description should be returned");
            Assert.AreEqual(forum.getForumRules(forumName), "", false, "after an unsuccessful change, the old rules should be returned");
            db.clear();
        }

        /*************************end use case 3******************************/

        /*************************use case 4******************************/

        [TestMethod]
        public void AT_Test_register_to_forum_withWrongInputs()
        {
            IForumManager forumMan = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ISuperUserManager superUserMan = new SuperUserManagerClient();
            superUserMan.initialize("guy", "AG36djs", "hello@dskkl.com");
            List<String> adminList = new List<String>();
            adminList.Add("admin1");
            adminList.Add("admin2");

            superUserMan.createForum("forumName", "descrption", "forumPolicy", "forumRules", adminList, "guy");
            Assert.IsTrue(forumMan.registerUser("admin1", "passWord1", "jksdjk@xc.com", "forumName"));
            Assert.IsTrue(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", "forumName"));
            Assert.IsFalse(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", "forumName"));
            Assert.IsTrue(forumMan.registerUser("mem1", "passWor1", "fff@xc.com", "forumName"));
            Assert.IsTrue(forumMan.registerUser("mem2", "passWor1", "fff@xc.com", "forumName"));
            Assert.IsFalse(forumMan.registerUser("", "passWor1", "fff@xc.com", "forumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "", "fff@xc.com", "forumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "", "forumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "fff@xc.com", "forumName"));
            DBClass db = DBClass.getInstance;
            db.clear();
        }

        [TestMethod]
        public void AT_Test_register_to_forum_Functionality()
        {
            IForumManager forumMan = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ISuperUserManager superUserMan = new SuperUserManagerClient();
            IUserManager userMan = new UserManagerClient();

            superUserMan.initialize("guy", "AG36djs", "hello@dskkl.com");
            List<String> adminList = new List<String>();
            adminList.Add("admin1");
            adminList.Add("admin2");

            superUserMan.createForum("forumName", "descrption", "forumPolicy", "forumRules", adminList, "guy");

            Assert.IsTrue(forumMan.registerUser("admin1", "passWord1", "jksdjk@xc.com", "forumName"));
            Assert.IsFalse(userMan.sendPrivateMessage("admin1", "admin2", "hello"));
            Assert.IsFalse(userMan.addFriend("admin1", "admin2"));
            Assert.IsFalse(userMan.addFriend("admin2", "admin1"));
            Assert.IsTrue(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", "forumName"));
            Assert.IsTrue(userMan.sendPrivateMessage("admin1", "admin2", "its me"));
            Assert.IsTrue(userMan.addFriend("admin1", "admin2"));
            Assert.IsTrue(userMan.addFriend("admin2", "admin1"));
            Assert.IsFalse(userMan.addFriend("admin2", "admin1"));
            Assert.IsFalse(userMan.addFriend("admin1", "mem1"));
            Assert.IsFalse(userMan.addFriend("mem1", "admin1"));
            Assert.IsFalse(userMan.sendPrivateMessage("mem1", "admin1", "i was wonder"));
            Assert.IsTrue(forumMan.registerUser("mem1", "passWor1", "fff@xc.com", "forumName"));
            Assert.IsTrue(userMan.addFriend("admin1", "mem1"));
            Assert.IsTrue(userMan.addFriend("mem1", "admin1"));
            Assert.IsTrue(userMan.sendPrivateMessage("mem1", "admin1", "when the test gona be done"));
            DBClass db = DBClass.getInstance;
            db.clear();
        }

        /*************************end use case 4******************************/

        /*************************use case 5+9******************************/
        
        [TestMethod]
        public void AT_test_create_subForum_and_nominate_moderator()
        {
            ISuperUserManager superUser = new SuperUserManagerClient();
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ISubForumManager subForum = new SubForumManagerClient();
            String forumName = "forum";
            String adminName = "admin";
            String subForumName = "subforum";
            String ModeratorName = "mod";
            List<string> adminList = new List<string>();
            adminList.Add(adminName);
            Dictionary<String, DateTime> modList = new Dictionary<String, DateTime>();
            modList.Add(ModeratorName, new DateTime(2030, 1, 1));
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");
            
            Assert.IsTrue(forum.addSubForum(forumName, subForumName, modList, adminName));

            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser("mem", "mempass", "mem@gmail.com", forumName));

            Assert.IsTrue(subForum.nominateModerator("mem", adminName, new DateTime(2030, 1, 1), subForumName, forumName), "nomination of member user should be successful");
            Assert.IsTrue(subForum.isModerator("mem", subForumName, forumName), "member should be moderator after his successful numonation");

            DBClass db = DBClass.getInstance;
            db.clear();
        }

        /*************************end use case 5+9******************************/

        /*************************use case 6******************************/

        [TestMethod]
        public void AT_test_add_thread_and_post()
        {
            ISuperUserManager superUser = new SuperUserManagerClient();
            IForumManager forum = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            ISubForumManager subForum = new SubForumManagerClient();
            IPostManager post = new PostManagerClient();
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            String forumName = "forum";
            String adminName = "admin";
            String subForumName = "subforum";
            String moderatorName = "mod";
            String userMemberName = "mem";
            List<string> adminList = new List<string>();
            adminList.Add(adminName);
            Dictionary<String, DateTime> modList = new Dictionary<String, DateTime>();
            modList.Add(moderatorName, new DateTime(2030, 1, 1));
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");


            Assert.IsTrue(forum.addSubForum(forumName, subForumName, modList, adminName));

            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser(userMemberName, "mempass", "mem@gmail.com", forumName));
            Assert.IsTrue(forum.registerUser(moderatorName, "modpass", "mod@gmail.com", forumName));

            Assert.IsTrue(subForum.nominateModerator(moderatorName, adminName, new DateTime(2030, 1, 1), subForumName, forumName), "nomination of member user should be successful");
            Assert.IsTrue(subForum.isModerator(moderatorName, subForumName, forumName), "member should be moderator after his successful numonation");

            Assert.IsTrue(forum.addSubForum(forumName, subForumName, modList, adminName));
            Assert.IsTrue(subForum.createThread("headLine", "content", userMemberName, forumName, subForumName));
            List<PostData> posts = post.getAllPosts(forumName, subForumName);
            int postId = posts[0].id;
            Assert.IsTrue(subForum.createThread("headLine", "content", userMemberName, forumName, subForumName));
            Assert.AreEqual(posts.Count, 1);

            Assert.IsTrue(post.addPost("headLine2", "content2", adminName, postId));
            Assert.AreEqual(posts.Count, 2);

            Assert.IsTrue(post.addPost("headLine3", "content3", userMemberName, postId));
            Assert.AreEqual(posts.Count, 3);

            DBClass db = DBClass.getInstance;
            db.clear();


        
        }

        /*************************end use case 6******************************/


    }
}
