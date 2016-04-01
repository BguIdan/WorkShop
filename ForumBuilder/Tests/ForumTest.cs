using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Forum;
using ForumBuilder.User;

namespace Tests
{
    [TestClass]
    public class ForumTest
    {
        private IForum forum;
        private IUser userNonMember;
        private IUser userMember;
        private IUser userAdmin;

        [TestInitialize]
        public void setUp()
        {
            this.forum = new Forum();
            this.userNonMember = new User();
            this.userMember = new User();
            this.userAdmin = new User();
            Assert.IsTrue(this.forum.registerUser(userMember));
            Assert.IsTrue(this.forum.registerUser(userAdmin));
            Assert.IsTrue(this.forum.nominateAdmin(userAdmin));
        }

        [TestCleanup]
        public void cleanUp()
        {
            this.forum = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userAdmin = null;
        }

        /******************************dismiss admin***********************************/

        [TestMethod]
        public void test_DismissAdmin_on_non_member()
        {
            String userNonMemberName = this.userNonMember.getUserName();
            Assert.IsFalse(this.forum.isAdmin(userNonMemberName), "userNonMember should not be a member in the forum");
            Assert.IsTrue(this.forum.dismissAdmin(userNonMember), "userNonMember is not a member in the forum hence his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forum.isAdmin(userNonMemberName), "userNonMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_non_admin()
        {
            String userMemberName = this.userMember.getUserName();
            Assert.IsFalse(this.forum.isAdmin(userMemberName), "userMember should not be an admin in the forum");
            Assert.IsTrue(this.forum.dismissAdmin(userMember), "userMember is not an administrator in the forum hence his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forum.isAdmin(userMemberName), "userMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_admin()
        {
            String userAdminName = this.userAdmin.getUserName();
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(this.forum.dismissAdmin(userMember), "userAdmin is an administrator in the forum. his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forum.isAdmin(userAdminName), "userAdmin should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_dismissAdmin_on_null()
        {
            Assert.IsFalse(this.forum.dismissAdmin(null), "dismiss admin on null should not be successful");
        }

        /*****************************end of dismiss admin***********************************/

        /******************************ban member***********************************/

        [TestMethod]
        public void test_banMember_on_non_member()
        {
            String userNonMemberName = this.userNonMember.getUserName();
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsTrue(this.forum.banMember(this.userNonMember), "ban of userNonMember should be successful");
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsFalse(this.forum.registerUser(this.userNonMember), "userNonMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
        }

        [TestMethod]
        public void test_banMember_on_member()
        {
            String userMemberName = this.userMember.getUserName();
            Assert.IsTrue(this.forum.isMember(userMemberName), "userMember should be a member");
            Assert.IsTrue(this.forum.banMember(this.userMember), "ban of userMember should be successful");
            Assert.IsFalse(this.forum.isMember(userMemberName), "userMember should not be a member when banned");
            Assert.IsFalse(this.forum.registerUser(this.userMember), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forum.isMember(userMemberName), "userMember should not be a member when banned");
        }

        [TestMethod]
        public void test_banMember_on_admin()
        {
            String userAdminName = this.userAdmin.getUserName();
            Assert.IsTrue(this.forum.isMember(userAdminName), "userAdmin should be a member");
            Assert.IsTrue(this.forum.banMember(this.userAdmin), "ban of userAdmin should not be successful");
            Assert.IsFalse(this.forum.isMember(userAdminName), "userMember should not be a member when banned");
            Assert.IsFalse(this.forum.registerUser(this.userAdmin), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forum.isMember(userAdminName), "userMember should not be a member when banned");
        }

        [TestMethod]
        public void test_banMember_on_null()
        {
            Assert.IsFalse(this.forum.banMember(null), "ban of null should not be successful");
        }

        /*****************************end of ban member***********************************/

        /******************************nominate admin***********************************/
        [TestMethod]
        public void test_nominateAdmin_on_non_member()
        {
            String userNonMemberName = this.userNonMember.getUserName();
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsTrue(this.forum.nominateAdmin(this.userNonMember), "nomination of non member to be admin should be successful");
            Assert.IsTrue(this.forum.isMember(userNonMemberName), "after becoming an admin the (previously) non member should be a member now")
            Assert.IsTrue(this.forum.isAdmin(userNonMemberName), "the (previously) non member should be an admin now");
        }
    }
}
