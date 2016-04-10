using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;
using System.Collections.Generic;


namespace Tests
{
    [TestClass]
    public class ForumControllerTest
    {
        private IForumController forumController;
        private Forum forum;
        private User userNonMember;
        private User userMember;
        private User userAdmin;
        private User userAdmin2;

        [TestInitialize]
        public void setUp()
        {
            this.forumController = ForumController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            this.userAdmin2 = new User("admin2", "adminpass2", "admin2@gmail.com");
            Assert.IsTrue(this.forumController.registerUser("mem", "mempass", "mem@gmail.com"));
            Assert.IsTrue(this.forumController.registerUser("admin", "adminpass", "admin@gmail.com"));
            Assert.IsTrue(this.forumController.nominateAdmin("admin", "adminpass", "admin@gmail.com"));
            Assert.IsTrue(this.forumController.nominateAdmin("admin2", "adminpass2", "admin2@gmail.com"));
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            adminList.Add("admin2");
            this.forum = new Forum("testForum", "bla", "bla", "the first rule is that you do not talk about fight club", adminList);
            
        }

        [TestCleanup]
        public void cleanUp()
        {
            DemoDB db = DemoDB.getInstance;
            db.clear();
            this.forumController = null;
            this.forum = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userAdmin = null;
            this.userAdmin2 = null;
            //TODO find a way to reset the DB
        }

        /******************************dismiss admin***********************************/

        [TestMethod]
        public void test_DismissAdmin_on_non_member()
        {

            Assert.IsFalse(this.forumController.isAdmin(this.userNonMember.userName, this.forum.forumName), "userNonMember should not be a member in the forum");
            Assert.IsTrue(this.forumController.dismissAdmin(this.userNonMember.userName, this.userAdmin2.userName, this.forum.forumName), "userNonMember is not a member in the forum hence his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forumController.isAdmin(this.userNonMember.userName, this.forum.forumName), "userNonMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_non_admin()
        {
            String memName = this.userMember.userName;
            String forumName = this.forum.forumName;
            String admin2Name = this.userAdmin2.userName;
            Assert.IsFalse(this.forumController.isAdmin(memName, forumName), "userMember should not be an admin in the forum");
            Assert.IsTrue(this.forumController.dismissAdmin(memName, admin2Name, forumName), "userMember is not an administrator in the forum hence his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forumController.isAdmin(memName, forumName), "userMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_admin()
        {
            String userAdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            String adminName = this.userAdmin.userName;
            String admin2Name = this.userAdmin2.userName;
            Assert.IsTrue(this.forumController.isAdmin(adminName, forumName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(this.forumController.dismissAdmin(adminName, admin2Name, forumName), "userAdmin is an administrator in the forum. his dismissal from being administrator should be successful");
            Assert.IsFalse(this.forumController.isAdmin(adminName, forumName), "userAdmin should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_dismissAdmin_on_null_dismissed()
        {
            Assert.IsFalse(this.forumController.dismissAdmin(null, this.userAdmin.userName, this.forum.forumName), "dismiss admin on null should not be successful");
        }

        [TestMethod]
        public void test_dismissAdmin_on_null_dismissor()
        {
            Assert.IsFalse(this.forumController.dismissAdmin(this.userAdmin.userName, null, this.forum.forumName), "dismiss admin on null should not be successful");
        }

        /*****************************end of dismiss admin***********************************/

        /******************************ban member***********************************/

        [TestMethod]
        public void test_banMember_on_non_member()
        {
            String NonMemberName = this.userNonMember.userName;
            String AdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isMember(NonMemberName, forumName), "userNonMember should not be a member");
            Assert.IsTrue(this.forumController.banMember(NonMemberName, AdminName, forumName), "ban of userNonMember should be successful");
            Assert.IsFalse(this.forumController.isMember(NonMemberName, forumName), "userNonMember should not be a member");
            Assert.IsFalse(this.forumController.registerUser(this.userNonMember.userName, this.userNonMember.password, this.userNonMember.email), "userNonMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forumController.isMember(NonMemberName, forumName), "userNonMember should not be a member");
        }

        [TestMethod]
        public void test_banMember_on_member()
        {
            String MemberName = this.userMember.userName;
            String AdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(MemberName, forumName), "userMember should be a member");
            Assert.IsTrue(this.forumController.banMember(MemberName, AdminName, forumName), "ban of userMember should be successful");
            Assert.IsFalse(this.forumController.isMember(MemberName, forumName), "userMember should not be a member when banned");
            Assert.IsFalse(this.forumController.registerUser(this.userMember.userName, this.userMember.password, this.userMember.email), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forumController.isMember(MemberName, forumName), "userMember should not be a member when banned");
        }

        [TestMethod]
        public void test_banMember_on_admin()
        {
            String AdminName = this.userAdmin.userName;
            String AdminName2 = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(AdminName, forumName), "userAdmin should be a member");
            Assert.IsTrue(this.forumController.banMember(AdminName, AdminName2, forumName), "ban of userAdmin should not be successful");
            Assert.IsFalse(this.forumController.isMember(AdminName, forumName), "userMember should not be a member when banned");
            Assert.IsFalse(this.forumController.registerUser(AdminName, this.userAdmin.password, this.userAdmin.email), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forumController.isMember(AdminName, forumName), "userMember should not be a member when banned");
        }

        [TestMethod]
        public void test_banMember_on_null()
        {
            //TODO more null tests
            Assert.IsFalse(this.forumController.banMember(null, this.userAdmin2.userName, this.forum.forumName), "ban of null should not be successful");
        }

        /*****************************end of ban member***********************************/

        /******************************nominate admin***********************************/
        [TestMethod]
        public void test_nominateAdmin_on_non_member()
        {
            String NonMemberName = this.userNonMember.userName;
            String AdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isMember(NonMemberName, forumName), "userNonMember should not be a member");
            Assert.IsTrue(this.forumController.nominateAdmin(NonMemberName, AdminName, forumName), "nomination of non member to be admin should be successful");
            Assert.IsTrue(this.forumController.isMember(NonMemberName, forumName), "after becoming an admin the (previously) non member should be a member now");
            Assert.IsTrue(this.forumController.isAdmin(NonMemberName, forumName), "the (previously) non member should be an admin now");
        }

        [TestMethod]
        public void test_nominateAdmin_on_member()
        {
            String userMemberName = this.userMember.userName;
            String AdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should be a member in the forum");
            Assert.IsFalse(this.forumController.isAdmin(userMemberName, forumName), "userMember should not be an admin in the forum");
            Assert.IsTrue(this.forumController.nominateAdmin(userMemberName, AdminName, forumName), "the nomination of userMember should be successful");
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should be a member in the forum");
            Assert.IsTrue(this.forumController.isAdmin(userMemberName, forumName), "userMember should be an admin in the forum after the nomination");
        }

        [TestMethod]
        public void test_nominateAdmin_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            String AdminName2 = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "userAdmin should be a member in the forum");
            Assert.IsTrue(this.forumController.isAdmin(userAdminName, forumName), "userAdmin should be an admin in the forum");
            Assert.IsTrue(this.forumController.nominateAdmin(userAdminName, AdminName2, forumName), "userAdmin is already admin. the nomination should be successful");
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "userAdmin should still be a member in the forum");
            Assert.IsTrue(this.forumController.isAdmin(userAdminName, forumName), "userAdmin should still be an admin in the forum");
        }

        [TestMethod]
        public void test_nominateAdmin_on_null()
        {
            Assert.IsFalse(this.forumController.nominateAdmin(null, this.userAdmin.userName, this.forum.forumName), "nomination of null should return false");
        }

        /******************************end of nominate admin***********************************/

        /******************************register user***********************************/
        [TestMethod]
        public void test_registerUser_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isMember(userNonMemberName, forumName), "userNonMember should not be a member");
            Assert.IsTrue(this.forumController.registerUser(this.userNonMember.userName, this.userNonMember.password, this.userNonMember.email), "registration of a non member should be successful");
            Assert.IsTrue(this.forumController.isMember(userNonMemberName, forumName), "after registration the user should become a member");
        }

        [TestMethod]
        public void test_registerUser_on_member()
        {
            String userMemberName = this.userMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should be a member in the forum");
            Assert.IsFalse(this.forumController.registerUser(this.userMember.userName, this.userMember.password, this.userMember.email), "the registration of a member should be unsuccessful");
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should still be a member in the forum");
        }

        [TestMethod]
        public void test_registerUser_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "userAdmin should be a member in the forum");
            Assert.IsTrue(this.forumController.isAdmin(userAdminName, forumName), "userAdmin should be an admin in the forum");
            Assert.IsFalse(this.forumController.registerUser(this.userAdmin.userName, this.userAdmin.password, this.userAdmin.email), "the registration of an admin should be successful");
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "userAdmin should still be a member in the forum");
        }

        [TestMethod]
        public void test_registerUser_on_null()
        {
            Assert.IsFalse(this.forumController.registerUser(null, null, null), "registration of null should return false");
            //TODO more null tests
        }


        /******************************end of register user***********************************/

        /******************************change policy***********************************/
        //TODO add tests for changes by other users
        public void test_changePolity_valid_policy()
        {
            String forumName = this.forum.forumName;
            String oldPolicy = this.forumController.getForumPolicy(forumName);
            String newPolicy = "new policy for test";
            String adminName = this.userAdmin.userName;
            Assert.AreNotEqual(oldPolicy, newPolicy, false, "the new policy should be different from the old one");
            Assert.IsTrue(this.forumController.changePoliciy(newPolicy, adminName, this.forumController.getForumPolicy(forumName)), "policy change should be successful");
            Assert.AreEqual(this.forumController.getForumPolicy(forumName), newPolicy, false, "the new policy should be return after the change");
        }

        public void test_changePolicy_with_null()
        {
            String forumName = this.forum.forumName;
            String oldPolicy = this.forum.forumPolicy;
            String adminName = this.userAdmin.userName;
            Assert.IsFalse(this.forumController.changePoliciy(null, adminName, forumName), "policy change with null should not be successful");
            Assert.AreEqual(this.forumController.getForumPolicy(forumName), oldPolicy, false, "after an unsuccessful change, the old policy should be returned");
        }

        public void test_changePolicy_with_empty_string()
        {
            String forumName = this.forum.forumName;
            String adminName = this.userAdmin.userName;
            String oldPolicy = this.forumController.getForumPolicy(forumName);
            Assert.IsTrue(this.forumController.changePoliciy("", adminName, forumName), "policy change with an empty string should be successful");
            Assert.AreEqual(this.forumController.getForumPolicy(forumName), "", false, "the new policy(empty string) should be return after the change");
        }


        /******************************end of change policy***********************************/

        /******************************is admin***********************************/


        [TestMethod]
        public void test_isAdmin_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isAdmin(userNonMemberName, forumName), "is admin on non member should return false");
        }

        [TestMethod]
        public void test_isAdmin_on_member()
        {
            String userMemberName = this.userMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isAdmin(userMemberName, forumName), "is admin on member (not admin) should return false");
        }

        [TestMethod]
        public void test_isAdmin_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isAdmin(userAdminName, forumName), "is admin on admin should return true");
        }

        [TestMethod]
        public void test_isAdmin_on_null()
        {
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isAdmin(null, forumName), "is admin on null should return false");
        }

        [TestMethod]
        public void test_isAdmin_on_empty_string()
        {
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isAdmin("", forumName), "is admin on an empty string should return false");
        }


        /******************************end of is admin***********************************/

        /******************************is member***********************************/


        [TestMethod]
        public void test_isMember_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isMember(userNonMemberName, forumName), "is member on non member should return false");
        }

        [TestMethod]
        public void test_isMember_on_member()
        {
            String userMemberName = this.userMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "is member on member (not admin) should return true");
        }

        [TestMethod]
        public void test_isMember_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "is member on admin should return true");
        }

        [TestMethod]
        public void test_isMember_on_null()
        {
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isMember(null, forumName), "is member on null should return false");
        }

        [TestMethod]
        public void test_isMember_on_empty_string()
        {
            String forumName = this.forum.forumName;
            Assert.IsFalse(this.forumController.isMember("", forumName), "is member on an empty string should return false");
        }


        /******************************end of is member***********************************/

        /******************************dismiss member***********************************/
/*
        [TestMethod]
        public void test_dismissMember_on_non_member()
        {
            String userNonMemberName = this.userNonMember.userName;
            String forumName = this.forum.forumName;
            String AdminName = this.userAdmin.userName;
            Assert.IsFalse(this.forumController.isMember(userNonMemberName, forumName), "non member user should not be a member");
            Assert.IsTrue(this.forumController.dismissMember(this.userNonMember, AdminName, forumName), "dismiss member on a non member should be successful");
            Assert.IsFalse(this.forumController.isMember(userNonMemberName), "non member user should remain as non member after his dismissal");
        }

        [TestMethod]
        public void test_dismissMember_on_member()
        {
            String userMemberName = this.userMember.userName;
            Assert.IsTrue(this.forumController.isMember(userMemberName), "user member should be a member in the forum");
            Assert.IsTrue(this.forumController.dismissMember(this.userMember), "dismiss member on member user should be successful");
            Assert.IsFalse(this.forumController.isMember(userMemberName), "after dismissal member user should not be a member anymore");
        }

        [TestMethod]
        public void test_dismissMember_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            Assert.IsTrue(this.forumController.isMember(userAdminName), "user admin should be a member in the forum");
            Assert.IsFalse(this.forumController.dismissMember(this.userAdmin), "dismiss member on admin user should be unsuccessful");
            Assert.IsTrue(this.forumController.isAdmin(userAdminName), "after unsuccessful dismissal, admin user should still be an admin");
            Assert.IsTrue(this.forumController.isMember(userAdminName), "after unsuccessful dismissal, admin user should still not be a member");
        }

        [TestMethod]
        public void test_dismissMember_on_null()
        {
            Assert.IsFalse(this.forumController.dismissMember(null), "dismiss member on null should return false");
        }

*/
        /******************************end of dismiss member***********************************/

        /******************************create sub forum***********************************/

        //TODO
        /******************************end of create sub forum***********************************/



    }
}
