using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using BL_Back_End;
using System.Collections.Generic;
using ForumBuilder.Systems;
using Database;

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
        private User superUser1;
        private User superUser;

        [TestInitialize]
        public void setUp()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            ForumSystem.initialize("guy", "AG36djs", "hello@dskkl.com");
            this.superUser = new User("guy", "AG36djs", "hello@dskkl.com");
            SuperUserController.getInstance.addSuperUser(this.superUser.email, this.superUser.password, this.superUser.userName);
            this.forumController = ForumController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            this.userAdmin2 = new User("admin2", "adminpass2", "admin2@gmail.com");
            ISuperUserController superUser = SuperUserController.getInstance;
            superUser.addUser("admin", "adminpass", "admin@gmail.com", "guy");
            superUser.addUser("admin2", "adminpass2", "admin2@gmail.com", "guy");
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            adminList.Add("admin2");
            ForumPolicy fp = new ForumPolicy("p", true, 0, true, 180, 1, true, true, 5);
            this.forum = new Forum("testForum", "descr",fp,adminList);            
            superUser1 = new User("tomer", "1qW", "fkfkf@wkk.com");
            SuperUserController.getInstance.addSuperUser(superUser1.email, superUser1.password, superUser1.userName);
            superUser.createForum("testForum", "descr",fp, adminList, "tomer");            
            Assert.IsTrue(this.forumController.registerUser("mem", "mempass", "mem@gmail.com", this.forum.forumName));

        }

        [TestCleanup]
        public void cleanUp()
        {
            this.forumController = null;
            this.forum = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userAdmin = null;
            this.userAdmin2 = null;
            DBClass db = DBClass.getInstance;
            db.clear();
        }

        /******************************dismiss admin***********************************/

        [TestMethod]
        public void test_DismissAdmin_on_non_member()
        {

            Assert.IsFalse(this.forumController.isAdmin(this.userNonMember.userName, this.forum.forumName), "userNonMember should not be a member in the forum");
            Assert.IsFalse(this.forumController.dismissAdmin(this.userNonMember.userName, this.userAdmin2.userName, this.forum.forumName), "userNonMember is not a member in the forum hence his dismissal from being administrator should be failure");
            Assert.IsFalse(this.forumController.isAdmin(this.userNonMember.userName, this.forum.forumName), "userNonMember should not be a administrator in the forum");
        }

        [TestMethod]
        public void test_DismissAdmin_on_member_non_admin()
        {
            String memName = this.userMember.userName;
            String forumName = this.forum.forumName;
            String admin2Name = this.userAdmin2.userName;
            Assert.IsFalse(this.forumController.isAdmin(memName, forumName), "userMember should not be an admin in the forum");
            Assert.IsFalse(this.forumController.dismissAdmin(memName, admin2Name, forumName), "userMember is not an administrator in the forum hence his dismissal from being administrator should Not be successful");
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
            Assert.IsTrue(this.forumController.dismissAdmin(adminName, superUser1.userName, forumName), "userAdmin is an administrator in the forum. his dismissal from being administrator should be successful");
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
            Assert.IsFalse(this.forumController.banMember(NonMemberName, AdminName, forumName), "ban of userNonMember should Not be successful");
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
            //Assert.IsFalse(this.forumController.registerUser(this.userMember.userName, this.userMember.password, this.userMember.email, forumName), "userMember should not be able to become a member since he is banned");
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
            //Assert.IsFalse(this.forumController.registerUser(AdminName, this.userAdmin.password, this.userAdmin.email, forumName), "userMember should not be able to become a member since he is banned");
            Assert.IsFalse(this.forumController.isMember(AdminName, forumName), "userMember should not be a member when banned");
        }

        [TestMethod]
        public void test_banMember_on_null()
        {
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
            Assert.IsFalse(this.forumController.nominateAdmin(NonMemberName, AdminName, forumName), "nomination of non member to be admin should NOT be successful");
        }

        [TestMethod]
        public void test_nominateAdmin_on_member()
        {
            String userMemberName = this.userMember.userName;
            String AdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should be a member in the forum");
            Assert.IsFalse(this.forumController.isAdmin(userMemberName, forumName), "userMember should not be an admin in the forum");
            Assert.IsTrue(this.forumController.nominateAdmin(userMemberName, this.superUser1.userName, forumName), "the nomination of userMember should be successful");
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
            Assert.IsFalse(this.forumController.nominateAdmin(userAdminName, AdminName2, forumName), "userAdmin is already admin. the nomination should NOT be successful");
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
            Assert.IsTrue(this.forumController.registerUser(this.userNonMember.userName, this.userNonMember.password, this.userNonMember.email, forumName), "registration of a non member should be successful");
            Assert.IsTrue(this.forumController.isMember(userNonMemberName, forumName), "after registration the user should become a member");
        }

        [TestMethod]
        public void test_registerUser_on_member()
        {
            String userMemberName = this.userMember.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should be a member in the forum");
            Assert.IsFalse(this.forumController.registerUser(this.userMember.userName, this.userMember.password, this.userMember.email, forumName), "the registration of a member should be unsuccessful");
            Assert.IsTrue(this.forumController.isMember(userMemberName, forumName), "userMember should still be a member in the forum");
        }

        [TestMethod]
        public void test_registerUser_on_admin()
        {
            String userAdminName = this.userAdmin.userName;
            String forumName = this.forum.forumName;
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "userAdmin should be a member in the forum");
            Assert.IsTrue(this.forumController.isAdmin(userAdminName, forumName), "userAdmin should be an admin in the forum");
            Assert.IsFalse(this.forumController.registerUser(this.userAdmin.userName, this.userAdmin.password, this.userAdmin.email, forumName), "the registration of an admin should be successful");
            Assert.IsTrue(this.forumController.isMember(userAdminName, forumName), "userAdmin should still be a member in the forum");
        }

        [TestMethod]
        public void test_registerUser_on_null()
        {
            Assert.IsFalse(this.forumController.registerUser(null, null, null,null), "registration of null should return false");
        }


        /******************************end of register user***********************************/

        /******************************set Forum Preferences***********************************/
        [TestMethod]
        public void test_setForumPreferences_valid_policy()
        {
            String forumName = this.forum.forumName;
            String oldPolicy = this.forumController.getForumPolicy(forumName);
            String newPolicy = "new policy for test";
            String oldDescription = this.forumController.getForumDescription(forumName);
            String newDescr = "new description";
            String newRules = "there are no rules";
            String adminName = this.userAdmin.userName;
            Assert.AreNotEqual(oldPolicy, newPolicy, false, "the new policy should be different from the old one");
            Assert.AreNotEqual(oldDescription, newDescr, false, "the new description should be different from the old one");
            ForumPolicy fp = new ForumPolicy("p", true, 0, true, 180, 1, true, true, 5);
            Assert.IsTrue(this.forumController.setForumPreferences(forumName, newDescr, fp, adminName), "policy change should be successful");
            Assert.AreEqual(this.forumController.getForumPolicy(forumName), newPolicy, false, "the new policy should be return after the change");
            Assert.AreEqual(this.forumController.getForumDescription(forumName), newDescr, false, "the new description should be return after the change");;
        
        }

        
        public void test_setForumPreferences_with_null()
        {
            String forumName = this.forum.forumName;
            String oldPolicy = this.forumController.getForumPolicy(forumName);
            String oldDescr = this.forumController.getForumDescription(forumName);
            String adminName = this.userAdmin.userName;
            ForumPolicy fp = new ForumPolicy("p", true, 0, true, 180, 1, true, true, 5);
            Assert.IsFalse(this.forumController.setForumPreferences(forumName, null, fp, adminName), "policy change with null should not be successful");
            Assert.AreEqual(this.forumController.getForumPolicy(forumName), oldPolicy, false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(this.forumController.getForumDescription(forumName), oldDescr, false, "after an unsuccessful change, the old description should be returned");

        }
        
        [TestMethod]
        public void test_setForumPreferences_with_empty_string()
        {
            String forumName = this.forum.forumName;
            String oldPolicy = this.forumController.getForumPolicy(forumName);
            String oldDescr = this.forumController.getForumDescription(forumName);
            String adminName = this.userAdmin.userName;
            ForumPolicy fp = new ForumPolicy("", true, 0, true, 180, 1, true, true, 5);
            Assert.IsTrue(this.forumController.setForumPreferences(forumName, "", fp, adminName), "policy change with null should not be successful");
            Assert.AreEqual(this.forumController.getForumPolicy(forumName), "", false, "after an unsuccessful change, the old policy should be returned");
            Assert.AreEqual(this.forumController.getForumDescription(forumName), "", false, "after an unsuccessful change, the old description should be returned");
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


    }
}
