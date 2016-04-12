﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;
using ForumBuilder.BL_DB;

namespace Tests
{
    [TestClass]
    public class AcceptanceTests
    {

        /*************************use case 2******************************

        [TestMethod]
        public void AT_test_create_and_manipulate_forum()
        {
            SuperUserManager superUser = SuperUserManager.getInstance;
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            ForumManager forum = ForumManager.getInstance;
            userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            userMember = new User("mem", "mempass", "mem@gmail.com");
            userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            userAdmin2 = new User("admin2", "adminpass2", "admin2@gmail.com");
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            adminList.Add("admin2");
            superUser.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer");
            Assert.IsTrue(forumController.registerUser("mem", "mempass", "mem@gmail.com", forum.forumName));
            Assert.IsTrue(forumController.registerUser("admin", "adminpass", "admin@gmail.com", forum.forumName));
            Assert.IsTrue(forumController.registerUser("admin2", "adminpass2", "admin2@gmail.com", forum.forumName));

        }

        /*************************use case 2******************************/


        /*************************use case 3******************************/

        [TestMethod]
        public void AT_test_changeForumPreferences_valid_policy()
        {
            SuperUserManager superUser = SuperUserManager.getInstance;
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            ForumManager forum = ForumManager.getInstance;
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
            Assert.IsTrue(forum.setForumPreferences(forumName, newPolicy, newDescr, newRules, adminName), "policy change should be successful");
            Assert.AreEqual(forum.getForumPolicy(forumName), newPolicy, false, "the new policy should be return after the change");
            Assert.AreEqual(forum.getForumDescription(forumName), newDescr, false, "the new description should be return after the change");
            Assert.AreEqual(forum.getForumRules(forumName), newRules, false, "the new rules should be return after the change");
            DemoDB db = DemoDB.getInstance;
            db.clear();
        }

        [TestMethod]
        public void AT_test_changeForumPreferences_with_null_inputs()
        {
            SuperUserManager superUser = SuperUserManager.getInstance;
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            ForumManager forum = ForumManager.getInstance;
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
            DemoDB db = DemoDB.getInstance;
            db.clear();

        }

        [TestMethod]
        public void AT_test_changeForumPreferences_with_empty_string()
        {
            SuperUserManager superUser = SuperUserManager.getInstance;
            superUser.initialize("guy", "AG36djs", "hello@dskkl.com");
            ForumManager forum = ForumManager.getInstance;
            String forumName = "forum";
            String adminName = "admin";
            List<String> adminList = new List<String>();
            adminList.Add(adminName);
            superUser.createForum(forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "guy");
            Assert.IsTrue(forum.registerUser(adminName, "adminpass", "admin@gmail.com", forumName));
            String oldPolicy = forum.getForumPolicy(forumName);
            String oldDescr = forum.getForumDescription(forumName);
            String oldRules = forum.getForumRules(forumName);
            Assert.IsFalse(forum.setForumPreferences(forumName, "", "", "", adminName), "policy change with null should not be successful");
            Assert.AreEqual(forum.getForumPolicy(forumName), oldPolicy, false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(forum.getForumDescription(forumName), oldDescr, false, "after an unsuccessful change, the old description should be returned");
            Assert.AreEqual(forum.getForumRules(forumName), oldRules, false, "after an unsuccessful change, the old rules should be returned");
            DemoDB db = DemoDB.getInstance;
            db.clear();
        }

        /*************************use case 3******************************/

    }
}
