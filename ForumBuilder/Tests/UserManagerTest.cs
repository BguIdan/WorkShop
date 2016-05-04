﻿using Database;
using ForumBuilder.Common.DataContracts;
using ForumBuilder.Common.ServiceContracts;
using ForumBuilder.Systems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PL.proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    class UserManagerTest
    {
        private IUserController userController = UserController.getInstance;
        private IForumController forumController;
        private User userNonMember;
        private User userMember;
        private User userAdmin;

        private IUserManager userManager = new UserManagerClient();
        private IForumManager forumManager;
        private UserData userNonMember;
        private UserData userMember;
        private UserData userAdmin;
        private String ForumName = "testForum";

        [TestInitialize]
        public void setUp()
        {
            ForumSystem.initialize("tomer", "1qW", "fkfkf@wkk.com");
            this.forumManager = new ForumManagerClient();
            ISuperUserManager superUser = new SuperUserManagerClient();
            this.userNonMember = new UserData("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new UserData("mem", "mempass", "mem@gmail.com");
            this.userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            superUser.createForum(ForumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer");
            Assert.IsTrue(this.forumManager.registerUser("mem", "mempass", "mem@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumManager.registerUser("admin", "adminpass", "admin@gmail.com", this.ForumName));


        }

        [TestCleanup]
        public void cleanUp()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            this.forumManager = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userAdmin = null;
        }


        [TestMethod]
        public void test_addFriend_nonMember_with_nonMember()
        {
            List<String> friends = this.userManager.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends, null, "");
            Assert.IsFalse(this.userManager.addFriend(this.userNonMember.userName, "heIsNotAMember"), "non member cannot add a friend");
            List<String> newFriendList = this.userManager.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList, null, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_nonMember_with_member()
        {
            List<String> friends = this.userManager.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends, null, "");
            Assert.IsFalse(this.userManager.addFriend(this.userNonMember.userName, this.userMember.userName), "non member cannot add a friend");
            List<String> newFriendList = this.userManager.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList, null, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_nonMember_with_admin()
        {
            List<String> friends = this.userManager.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends, null, "");
            Assert.IsFalse(this.userManager.addFriend(this.userNonMember.userName, this.userAdmin.userName), "non member cannot add a friend");
            List<String> newFriendList = this.userManager.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList, null, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_member_with_nonMember()
        {
            List<String> friends = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userManager.addFriend(this.userMember.userName, "heIsNotAMember"), "member cannot add a non member");
            List<String> newFriendList = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_member_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumManager.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userManager.addFriend(this.userMember.userName, newMemberName), "member should be able to add new friend(member)");
            List<String> newFriendList = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
        }

        [TestMethod]
        public void test_addFriend_member_with_admin()
        {
            List<String> friends = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.userManager.addFriend(this.userMember.userName, this.userAdmin.userName), "member should be able to add new friend(admin)");
            List<String> newFriendList = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(this.userAdmin.userName));
        }

        [TestMethod]
        public void test_addFriend_admin_with_nonMember()
        {
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userManager.addFriend(this.userAdmin.userName, "heIsNotAMember"), "admin cannot add a non member");
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_admin_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumManager.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userManager.addFriend(this.userAdmin.userName, newMemberName), "admin should be able to add new friend(member)");
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
        }

        [TestMethod]
        public void test_addFriend_admin_with_admin()
        {
            String adminName = "admin2";
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumManager.registerUser(adminName, "adminpass", "admin@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumManager.nominateAdmin(adminName, this.userAdmin.userName, this.ForumName));
            Assert.IsTrue(this.userManager.addFriend(this.userAdmin.userName, adminName), "admin should be able to add new friend(admin)");
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(adminName));
        }

        [TestMethod]
        public void test_addFriend_admin_with_null()
        {
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userManager.addFriend(this.userAdmin.userName, null), "adding friend with null should fail");
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }



        [TestMethod]
        public void test_deleteFriend_member_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumManager.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userManager.addFriend(userMember.userName, newMemberName));
            Assert.IsTrue(this.userManager.deleteFriend(this.userMember.userName, newMemberName), "member should be able to add new friend(member)");
            Assert.IsTrue(this.userManager.addFriend(userMember.userName, newMemberName));
            List<String> newFriendList = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
            Assert.IsTrue(this.userManager.deleteFriend(this.userMember.userName, newMemberName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(newMemberName));
        }

        [TestMethod]
        public void test_deleteFriend_member_with_admin()
        {
            List<String> friends = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userManager.deleteFriend(this.userMember.userName, this.userAdmin.userName), "member should be able to add new friend(admin)");
            Assert.IsTrue(this.userManager.addFriend(userMember.userName, userAdmin.userName));
            List<String> newFriendList = this.userManager.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(this.userAdmin.userName));
            Assert.IsTrue(this.userManager.deleteFriend(this.userMember.userName, this.userAdmin.userName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(this.userAdmin.userName));
        }

        [TestMethod]
        public void test_deleteFriend_admin_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumManager.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userManager.addFriend(userAdmin.userName, newMemberName));
            Assert.IsTrue(this.userManager.deleteFriend(this.userAdmin.userName, newMemberName), "admin should be able to add new friend(member)");
            Assert.IsTrue(this.userManager.addFriend(userAdmin.userName, newMemberName));
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
            Assert.IsTrue(this.userManager.deleteFriend(this.userAdmin.userName, newMemberName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(newMemberName));
        }

        [TestMethod]
        public void test_deleteFriend_admin_with_admin()
        {
            String adminName = "admin2";
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumManager.registerUser(adminName, "adminpass", "admin@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumManager.nominateAdmin(adminName, this.userAdmin.userName, this.ForumName));
            Assert.IsFalse(this.userManager.deleteFriend(this.userAdmin.userName, adminName), "admin should be able to add new friend(admin)");
            Assert.IsTrue(this.userManager.addFriend(userAdmin.userName, adminName));
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(adminName));
            Assert.IsTrue(this.userManager.deleteFriend(this.userAdmin.userName, adminName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(adminName));
        }

        [TestMethod]
        public void test_deleteFriend_admin_with_null()
        {
            List<String> friends = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userManager.deleteFriend(this.userAdmin.userName, null), "deleting friend with null should fail");
            List<String> newFriendList = this.userManager.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }
    }
    
}
