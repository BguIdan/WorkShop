using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using BL_Back_End;
using ForumBuilder.Systems;
using Database;

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
            DBClass db = DBClass.getInstance;
            db.clear();
            ForumSystem.initialize("tomer", "1qW", "fkfkf@wkk.com");
            this.forumController = ForumController.getInstance;
            ISuperUserController superUser = SuperUserController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            superUser.addUser("admin", "adminpass", "admin@gmail.com");
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            SuperUserController.getInstance.addSuperUser("fkfkf@wkk.com", "1qW", "tomer");
            superUser.createForum(ForumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer");
            Assert.IsTrue(this.forumController.registerUser("mem", "mempass", "mem@gmail.com", this.ForumName));
 //           Assert.IsTrue(this.forumController.registerUser("admin", "adminpass", "admin@gmail.com", this.ForumName));
            
            
        }

        [TestCleanup]
        public void cleanUp()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            this.forumController = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userAdmin = null;
        }


        /**********************add friend*********************/
        [TestMethod]
        public void test_addFriend_nonMember_with_nonMember()
        {
            List<String> friends = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends, null, "");
            Assert.IsFalse(this.userController.addFriend(this.userNonMember.userName, "heIsNotAMember"), "non member cannot add a friend");
            List<String> newFriendList = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList, null, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_nonMember_with_member()
        {
            List<String> friends = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends, null, "");
            Assert.IsFalse(this.userController.addFriend(this.userNonMember.userName, this.userMember.userName), "non member cannot add a friend");
            List<String> newFriendList = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList, null, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_nonMember_with_admin()
        {
            List<String> friends = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(friends, null, "");
            Assert.IsFalse(this.userController.addFriend(this.userNonMember.userName, this.userAdmin.userName), "non member cannot add a friend");
            List<String> newFriendList = this.userController.getFriendList(this.userNonMember.userName);
            Assert.AreEqual(newFriendList,null , "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
        public void test_addFriend_member_with_nonMember()
        {
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userMember.userName, "heIsNotAMember"), "member cannot add a non member");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
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

        [TestMethod]
        public void test_addFriend_member_with_admin()
        {
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.userController.addFriend(this.userMember.userName, this.userAdmin.userName), "member should be able to add new friend(admin)");
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(this.userAdmin.userName));
        }

        [TestMethod]
        public void test_addFriend_admin_with_nonMember()
        {
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.addFriend(this.userAdmin.userName, "heIsNotAMember"), "admin cannot add a non member");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "unsuccessful friend addition should not change the friend list");
        }

        [TestMethod]
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

        [TestMethod]
        public void test_addFriend_admin_with_admin()
        {
            String adminName = "admin2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(adminName, "adminpass", "admin@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumController.nominateAdmin(adminName, this.userAdmin.userName, this.ForumName));
            Assert.IsTrue(this.userController.addFriend(this.userAdmin.userName, adminName), "admin should be able to add new friend(admin)");
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(adminName));
        }

        [TestMethod]
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

        [TestMethod]
        public void test_deleteFriend_member_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userController.addFriend(userMember.userName, newMemberName));
            Assert.IsTrue(this.userController.deleteFriend(this.userMember.userName, newMemberName), "member should be able to add new friend(member)");
            Assert.IsTrue(this.userController.addFriend(userMember.userName, newMemberName));
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
            Assert.IsTrue(this.userController.deleteFriend(this.userMember.userName, newMemberName), "deleting friend should be successful");
            newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(newMemberName));
        }

        [TestMethod]
        public void test_deleteFriend_member_with_admin()
        {
            List<String> friends = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsFalse(this.userController.deleteFriend(this.userMember.userName, this.userAdmin.userName), "member should be able to add new friend(admin)");
            Assert.IsTrue(this.userController.addFriend(userMember.userName, userAdmin.userName));
            List<String> newFriendList = this.userController.getFriendList(this.userMember.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(this.userAdmin.userName));
            Assert.IsTrue(this.userController.deleteFriend(this.userMember.userName, this.userAdmin.userName), "deleting friend should be successful");
            newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(this.userAdmin.userName));
        }

        [TestMethod]
        public void test_deleteFriend_admin_with_member()
        {
            String newMemberName = "mem2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(newMemberName, "mempass2", "mem2@gmail.com", this.ForumName));
            Assert.IsTrue(this.userController.addFriend(userAdmin.userName, newMemberName));
            Assert.IsTrue(this.userController.deleteFriend(this.userAdmin.userName, newMemberName), "admin should be able to add new friend(member)");
            Assert.IsTrue(this.userController.addFriend(userAdmin.userName, newMemberName));
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(newMemberName));
            Assert.IsTrue(this.userController.deleteFriend(this.userAdmin.userName, newMemberName), "deleting friend should be successful");
            newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(newMemberName));
        }

        [TestMethod]
        public void test_deleteFriend_admin_with_admin()
        {
            String adminName = "admin2";
            List<String> friends = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(friends.Count, 0, "initial friend list should be empty");
            Assert.IsTrue(this.forumController.registerUser(adminName, "adminpass", "admin@gmail.com", this.ForumName));
            Assert.IsTrue(this.forumController.nominateAdmin(adminName, this.userAdmin.userName, this.ForumName));
            Assert.IsFalse(this.userController.deleteFriend(this.userAdmin.userName, adminName), "admin should be able to add new friend(admin)");
            Assert.IsTrue(this.userController.addFriend(userAdmin.userName, adminName));
            List<String> newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 1, "friend list size should increase from 0 to 1");
            Assert.IsTrue(newFriendList.Contains(adminName));
            Assert.IsTrue(this.userController.deleteFriend(this.userAdmin.userName, adminName), "deleting friend should be successful");
            newFriendList = this.userController.getFriendList(this.userAdmin.userName);
            Assert.AreEqual(newFriendList.Count, 0, "friend list size should increase from 1 to 0");
            Assert.IsFalse(newFriendList.Contains(adminName));
        }

        [TestMethod]
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
