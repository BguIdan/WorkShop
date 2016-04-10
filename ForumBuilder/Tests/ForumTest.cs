using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using ForumBuilder.Users;

namespace Tests
{
    [TestClass]
    public class ForumTest
    {
        private IForumController forum;
        private User userNonMember;
        private User userMember;
        private User userAdmin;

        [TestInitialize]
        public void setUp()
        {
            this.forum = ForumController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            Assert.IsTrue(this.forum.registerUser("nonMem", "nonmemPass", "nonmem@gmail.com"));
            Assert.IsTrue(this.forum.registerUser("mem", "mempass", "mem@gmail.com"));
            Assert.IsTrue(this.forum.nominateAdmin("admin", "", ""));
            //TODO add a valid nominator and forum name
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
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isAdmin(userNonMemberName), "userNonMember should not be a member in the forum");
            Assert.IsTrue(this.forum.dismissAdmin(userNonMemberName,""), "userNonMember is not a member in the forum hence his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forum.isAdmin(userNonMemberName), "userNonMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_non_admin()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsFalse(this.forum.isAdmin(userMemberName), "userMember should not be an admin in the forum");
            Assert.IsTrue(this.forum.dismissAdmin(userMemberName, ""), "userMember is not an administrator in the forum hence his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forum.isAdmin(userMemberName), "userMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(this.forum.dismissAdmin(userAdminName, ""), "userAdmin is an administrator in the forum. his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forum.isAdmin(userAdminName), "userAdmin should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_dismissAdmin_on_null_dismissed()
        {
            Assert.IsFalse(this.forum.dismissAdmin(null, ""), "dismiss admin on null should not be successful");
        }

        [TestMethod]
        public void test_dismissAdmin_on_null_dismissor()
        {
            Assert.IsFalse(this.forum.dismissAdmin(this.userAdmin.userName, null), "dismiss admin on null should not be successful");
        }

        /*****************************end of dismiss admin***********************************/

        /******************************ban member***********************************/

        [TestMethod]
        public void test_banMember_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsTrue(this.forum.banMember(userNonMemberName, ""), "ban of userNonMember should be successful");
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsFalse(this.forum.registerUser(this.userNonMember.userName, this.userNonMember.password, this.userNonMember.email), "userNonMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
        }

        [TestMethod]
        public void test_banMember_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsTrue(this.forum.isMember(userMemberName), "userMember should be a member");
            Assert.IsTrue(this.forum.banMember(userMemberName, ""), "ban of userMember should be successful");
            Assert.IsFalse(this.forum.isMember(userMemberName), "userMember should not be a member when banned");
            Assert.IsFalse(this.forum.registerUser(this.userMember.userName, this.userMember.password, this.userMember.email), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forum.isMember(userMemberName), "userMember should not be a member when banned");
        }

        [TestMethod]
        public void test_banMember_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
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
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsTrue(this.forum.nominateAdmin(this.userNonMember), "nomination of non member to be admin should be successful");
            Assert.IsTrue(this.forum.isMember(userNonMemberName), "after becoming an admin the (previously) non member should be a member now");
            Assert.IsTrue(this.forum.isAdmin(userNonMemberName), "the (previously) non member should be an admin now");
        }

        [TestMethod]
        public void test_nominateAdmin_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsTrue(this.forum.isMember(userMemberName), "userMember should be a member in the forum");
            Assert.IsFalse(this.forum.isAdmin(userMemberName), "userMember should not be an admin in the forum");
            Assert.IsTrue(this.forum.nominateAdmin(this.userMember), "the nomination of userMember should be successful");
            Assert.IsTrue(this.forum.isMember(userMemberName), "userMember should be a member in the forum");
            Assert.IsTrue(this.forum.isAdmin(userMemberName), "userMember should be an admin in the forum after the nomination");
        }

        [TestMethod]
        public void test_nominateAdmin_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forum.isMember(userAdminName), "userAdmin should be a member in the forum");
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(this.forum.nominateAdmin(this.userAdmin), "userAdmin is already admin. the nomination should be successful");
            Assert.IsTrue(this.forum.isMember(userAdminName), "userAdmin should still be a member in the forum");
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "userAdmin should still be an admin in the forum");
        }

        [TestMethod]
        public void test_nominateAdmin_on_null()
        {
            Assert.IsFalse(this.forum.nominateAdmin(null), "nomination of null should return false");
        }

        /******************************end of nominate admin***********************************/

        /******************************register user***********************************/
        [TestMethod]
        public void test_registerUser_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "userNonMember should not be a member");
            Assert.IsTrue(this.forum.registerUser(this.userNonMember), "registration of a non member should be successful");
            Assert.IsTrue(this.forum.isMember(userNonMemberName), "after registration the user should become a member");
        }

        [TestMethod]
        public void test_registerUser_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsTrue(this.forum.isMember(userMemberName), "userMember should be a member in the forum");
            Assert.IsFalse(this.forum.registerUser(this.userMember), "the registration of a member should be unsuccessful");
            Assert.IsTrue(this.forum.isMember(userMemberName), "userMember should still be a member in the forum");
        }

        [TestMethod]
        public void test_registerUser_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forum.isMember(userAdminName), "userAdmin should be a member in the forum");
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "userAdmin should be an admin in the forum");
            Assert.IsFalse(this.forum.registerUser(this.userAdmin), "the registration of an admin should be successful");
            Assert.IsTrue(this.forum.isMember(userAdminName), "userAdmin should still be a member in the forum");
        }

        [TestMethod]
        public void test_registerUser_on_null()
        {
            Assert.IsFalse(this.forum.nominateAdmin(null), "registration of null should return false");
        }


        /******************************end of register user***********************************/

        /******************************change policy***********************************/
        public void test_changePolity_valid_policy()
        {
            String oldPolicy = this.forum.getPolicy();
            String newPolicy = "new policy for test";
            Assert.AreNotEqual(oldPolicy, newPolicy, false, "the new policy should be different from the old one");
            Assert.IsTrue(this.forum.changePoliciy(newPolicy), "policy change should be successful");
            Assert.AreEqual(this.forum.getPolicy(), newPolicy, false, "the new policy should be return after the change");
        }

        public void test_changePolicy_with_null()
        {
            String oldPolicy = this.forum.getPolicy();
            Assert.IsFalse(this.forum.changePoliciy(null), "policy change with null should not be successful");
            Assert.AreEqual(this.forum.getPolicy(), oldPolicy, false, "after an unsuccessful change, the old policy should be returned");
        }

        public void test_changePolicy_with_empty_string()
        {
            String oldPolicy = this.forum.getPolicy();
            Assert.IsTrue(this.forum.changePoliciy(""), "policy change with an empty string should be successful");
            Assert.AreEqual(this.forum.getPolicy(), "", false, "the new policy(empty string) should be return after the change");
        }


        /******************************end of change policy***********************************/

        /******************************is admin***********************************/


        [TestMethod]
        public void test_isAdmin_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isAdmin(userNonMemberName), "is admin on non member should return false");
        }

        [TestMethod]
        public void test_isAdmin_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsFalse(this.forum.isAdmin(userMemberName), "is admin on member (not admin) should return false");
        }

        [TestMethod]
        public void test_isAdmin_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "is admin on admin should return true");
        }

        [TestMethod]
        public void test_isAdmin_on_null()
        {
            Assert.IsFalse(this.forum.isAdmin(null), "is admin on null should return false");
        }

        [TestMethod]
        public void test_isAdmin_on_empty_string()
        {
            Assert.IsFalse(this.forum.isAdmin(""), "is admin on an empty string should return false");
        }


        /******************************end of is admin***********************************/

        /******************************is member***********************************/


        [TestMethod]
        public void test_isMember_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "is member on non member should return false");
        }

        [TestMethod]
        public void test_isMember_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsTrue(this.forum.isMember(userMemberName), "is member on member (not admin) should return true");
        }

        [TestMethod]
        public void test_isMember_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forum.isMember(userAdminName), "is member on admin should return true");
        }

        [TestMethod]
        public void test_isMember_on_null()
        {
            Assert.IsFalse(this.forum.isMember(null), "is member on null should return false");
        }

        [TestMethod]
        public void test_isMember_on_empty_string()
        {
            Assert.IsFalse(this.forum.isMember(""), "is member on an empty string should return false");
        }


        /******************************end of is member***********************************/

        /******************************dismiss member***********************************/

        [TestMethod]
        public void test_dismissMember_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "non member user should not be a member");
            Assert.IsTrue(this.forum.dismissMember(this.userNonMember), "dismiss member on a non member should be successful");
            Assert.IsFalse(this.forum.isMember(userNonMemberName), "non member user should remain as non member after his dismissal");
        }

        [TestMethod]
        public void test_dismissMember_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsTrue(this.forum.isMember(userMemberName), "user member should be a member in the forum");
            Assert.IsTrue(this.forum.dismissMember(this.userMember), "dismiss member on member user should be successful");
            Assert.IsFalse(this.forum.isMember(userMemberName), "after dismissal member user should not be a member anymore");
        }

        [TestMethod]
        public void test_dismissMember_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forum.isMember(userAdminName), "user admin should be a member in the forum");
            Assert.IsFalse(this.forum.dismissMember(this.userAdmin), "dismiss member on admin user should be unsuccessful");
            Assert.IsTrue(this.forum.isAdmin(userAdminName), "after unsuccessful dismissal, admin user should still be an admin");
            Assert.IsTrue(this.forum.isMember(userAdminName), "after unsuccessful dismissal, admin user should still not be a member");
        }

        [TestMethod]
        public void test_dismissMember_on_null()
        {
            Assert.IsFalse(this.forum.dismissMember(null), "dismiss member on null should return false");
        }


        /******************************end of dismiss member***********************************/

        /******************************create sub forum***********************************/

        //TODO
        /******************************end of create sub forum***********************************/



    }
}
