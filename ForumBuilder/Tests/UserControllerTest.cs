﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using ForumBuilder.BL_DB;
using ForumBuilder.BL_Back_End;

namespace Tests
{
    [TestClass]
    public class UserControllerTest
    {

        private IUserController userController = UserController.getInstance;
        private IForumController forumController;
        private User userNonMember;
        private User userMember;
        private User userAdmin;
        private String ForumName = "testForum";

        [TestInitialize]
        public void setUp()
        {
            this.forumController = ForumController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            Assert.IsTrue(this.forumController.registerUser("mem", "mempass", "mem@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumController.registerUser("admin", "adminpass", "admin@gmail.com", this.ForumName));
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            ISuperUserController superUser = SuperUserController.getInstance;
            superUser.createForum(ForumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "");
        }

        [TestCleanup]
        public void cleanUp()
        {
            DemoDB db = DemoDB.getInstance;
            db.clear();
            this.forumController = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userAdmin = null;
            //TODO find a way to reset the DB
        }


        /**********************add friend*********************/

        public void test_addFriend_nonMember_with_nonMember()
        {
            List<String> friends = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userNonMember.userName, "heIsNotAMember"), "non member cannot add a friend");
            List<String> newFriendList = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }


        public void test_addFriend_nonMember_with_member()
        {
            List<String> friends = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userNonMember.userName, this.userMember.userName), "non member cannot add a friend");
            List<String> newFriendList = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        public void test_addFriend_nonMember_with_admin()
        {
            List<String> friends = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userNonMember.userName, this.userAdmin.userName), "non member cannot add a friend");
            List<String> newFriendList = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        public void test_addFriend_member_with_nonMember()
        {
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userMember.userName, "heIsNotAMember"), "member cannot add a non member");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }


        public void test_addFriend_member_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userController.addFriend(this.userMember.userName, newMemberName), "member should be able to add new friend(member)");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
        }

        public void test_addFriend_member_with_admin()
        {
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userMember.userName, this.userAdmin.userName), "member should be able to add new friend(admin)");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(this.userAdmin.userName));
        }

        public void test_addFriend_admin_with_nonMember()
        {
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userAdmin.userName, "heIsNotAMember"), "admin cannot add a non member");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }


        public void test_addFriend_admin_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userController.addFriend(this.userAdmin.userName, newMemberName), "admin should be able to add new friend(member)");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
        }

        public void test_addFriend_admin_with_admin()
        {
            String adminName = "admin2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(adminName, "adminpass", "admin@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumController.nominateAdmin(adminName, this.userAdmin.userName, this.ForumName));
            Assert.IsFalse(this.userController.addFriend(this.userAdmin.userName, adminName), "admin should be able to add new friend(admin)");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(adminName));
        }

        public void test_addFriend_admin_with_null()
        {
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userAdmin.userName, null), "adding friend with null should fail");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        /**********************end of add friend*********************/

        /**********************delete friend*********************/

        public void test_deleteFriend_member_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userController.deleteFriend(this.userMember.userName, newMemberName), "member should be able to add new friend(member)");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
            Assert.IsTrue(this.userController.deleteFriend(this.userMember.userName, newMemberName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(newMemberName));
        }

        public void test_deleteFriend_member_with_admin()
        {
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.deleteFriend(this.userMember.userName, this.userAdmin.userName), "member should be able to add new friend(admin)");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(this.userAdmin.userName));
            Assert.IsTrue(this.userController.deleteFriend(this.userMember.userName, this.userAdmin.userName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(this.userAdmin.userName));
        }


        public void test_deleteFriend_admin_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userController.deleteFriend(this.userAdmin.userName, newMemberName), "admin should be able to add new friend(member)");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
            Assert.IsTrue(this.userController.deleteFriend(this.userAdmin.userName, newMemberName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(newMemberName));
        }

        public void test_deleteFriend_admin_with_admin()
        {
            String adminName = "admin2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(adminName, "adminpass", "admin@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumController.nominateAdmin(adminName, this.userAdmin.userName, this.ForumName));
            Assert.IsFalse(this.userController.deleteFriend(this.userAdmin.userName, adminName), "admin should be able to add new friend(admin)");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(adminName));
            Assert.IsTrue(this.userController.deleteFriend(this.userAdmin.userName, adminName), "deleting friend should be successful");
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(adminName));
        }

        public void test_deleteFriend_admin_with_null()
        {
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.deleteFriend(this.userAdmin.userName, null), "deleting friend with null should fail");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }


    }
}