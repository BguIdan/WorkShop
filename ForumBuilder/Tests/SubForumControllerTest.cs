using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;


namespace Tests
{
    [TestClass]
    public class SubForumControllerTest
    {
        private IForumController forumController;
        private Forum forum;
        private User userNonMember;
        private User userMember;
        private User userModerator;
        private User userAdmin;
        private ISubForumController subForum;
        private String subForumName = "subforum";
        private String forumName = "testForum";
        private SuperUser superUser1;


        [TestInitialize]
        public void setUp()
        {
            this.forumController = ForumController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userModerator = new User("mod", "modpass", "mod@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            Dictionary<String, DateTime> modList = new Dictionary<String, DateTime>();
            modList.Add(this.userModerator.userName, new DateTime(2030, 1, 1));
            List<string> adminList = new List<string>();
            adminList.Add("admin");
            this.forum = new Forum(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList);
            ISuperUserController superUser = SuperUserController.getInstance;
            superUser1 = new SuperUser("fkfkf@wkk.com", "1qW", "tomer");
            SuperUserController.getInstance.addSuperUser(superUser1._email, superUser1._password, superUser1._userName);
            superUser.createForum(forum.forumName, forum.description, forum.forumPolicy, forum.forumRules, forum.administrators, superUser1._userName);
            Assert.IsTrue(this.forumController.registerUser("admin", "adminpass", "admin@gmail.com", this.forumName));
            Assert.IsTrue(this.forumController.registerUser("mem", "mempass", "mem@gmail.com", this.forumName));
            Assert.IsTrue(this.forumController.registerUser("mod", "modpass", "mod@gmail.com", this.forumName));
            //Assert.IsTrue(this.forumController.nominateAdmin("admin", "adminpass", "admin@gmail.com"));
            Assert.IsTrue(superUser.createForum("testForum", "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer"));
            Assert.IsTrue(this.forumController.addSubForum(this.forum.forumName, this.subForumName, modList, this.userAdmin.userName));
            this.subForum = SubForumController.getInstance;

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
            this.userModerator = null;
            this.userAdmin = null;
            this.subForum = null;
        }

        /******************************dismiss moderator***************************************/

        [TestMethod]
        public void test_dismissModerator_on_valid_moderator()
        {
            String userModeratorName = this.userModerator.userName;
            Assert.IsTrue(this.subForum.isModerator(userModeratorName, this.subForumName, this.forumName), "user moderator should be a moderator");
            Assert.IsTrue(this.subForum.dismissModerator(userModeratorName, this.userAdmin.userName, this.subForumName, this.forumName), "the dismissal of user moderator should be successful");
            Assert.IsFalse(this.subForum.isModerator(userModeratorName, this.subForumName, this.forumName), "user moderator should not be a moderator after his dismissal");
        }

        [TestMethod]
        public void test_dismissModerator_on_non_moderator()
        {
            String memberUserName = this.userMember.userName;
            Assert.IsFalse(this.subForum.isModerator(memberUserName, this.subForumName, this.forumName), "the moderatorList list should not contain the non moderator member");
            Assert.IsFalse(subForum.dismissModerator(memberUserName, this.userAdmin.userName, this.subForumName, this.forumName), "dismiss moderator on non moderator should return false");
            Assert.IsFalse(this.subForum.isModerator(memberUserName, this.subForumName, this.forumName), "the moderatorList list should not contain the non moderator member");
        }

        [TestMethod]
        public void test_dismissModerator_on_null()
        {
            Assert.IsFalse(subForum.dismissModerator(null, this.userAdmin.userName, this.subForumName, this.forumName), "dismiss moderator on null should return false");
        }

        [TestMethod]
        public void test_dismissModerator_on_empty_string()
        {

            Assert.IsFalse(subForum.dismissModerator("", this.userAdmin.userName, this.subForumName, this.forumName), "dismiss moderator on an empty string should return false");
        }


        /******************************end of dismiss moderator***************************************



        /******************************create thread***************************************

        [TestMethod]
        public void test_createThread_on_valid_input()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsTrue(this.subForum.createThread("good headline", "great content"), "the addition of thread with valid input should be successful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count + 1), "thread list size should increase by 1");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 1));
        }

        [TestMethod]
        public void test_createThread_on_empty_headline()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsTrue(this.subForum.createThread("", "great content"), "the addition of thread with an empty headline should be successful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count + 1), "thread list size should increase by 1");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 1));
        }

        [TestMethod]
        public void test_createThread_on_empty_content()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsTrue(this.subForum.createThread("good headline", ""), "the addition of thread with an empty content string should be successful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count + 1), "thread list size should increase by 1");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 1));
        }

        [TestMethod]
        public void test_createThread_on_empty_string_inputs()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsFalse(this.subForum.createThread("", ""), "the addition of thread with an empty string inputs should be unsuccessful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count), "thread list size should not increase");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 0), "thread list should stay the same");
        }

        [TestMethod]
        public void test_createThread_on_null_headline()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsFalse(this.subForum.createThread(null, "great content"), "the addition of thread with a null headline should be unsuccessful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count), "thread list size should stay the same");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 0), "thread list should stay the same");
        }

        [TestMethod]
        public void test_createThread_on_null_content()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsTrue(this.subForum.createThread("good headline", null), "the addition of thread with a null content should be unsuccessful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count), "thread list size should stay the same");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 0), "thread list should stay the same");
        }

        [TestMethod]
        public void test_createThread_on_null_inputs()
        {
            List<IThread> threadsPriorAddition = this.subForum.getThreads();
            Assert.IsFalse(this.subForum.createThread(null, null), "the addition of thread with null as inputs be unsuccessful");
            List<IThread> threadsAfterAddition = this.subForum.getThreads();
            Assert.IsTrue((threadsPriorAddition.Count == threadsAfterAddition.Count), "thread list size should not increase");
            Assert.IsTrue((threadsAfterAddition.Except(threadsPriorAddition).Count() == 0), "thread list should stay the same");
        }


        /******************************end of create thread***************************************/


        /******************************nominate moderator***************************************/

        [TestMethod]
        public void test_nominateModerator_on_member()
        {
            String memberName = this.userMember.userName;
            Assert.IsTrue(this.subForum.nominateModerator(memberName, this.userAdmin.userName, new DateTime(2030, 1, 1), this.subForumName, this.forumName), "nomination of member user should be successful");
            Assert.IsTrue(this.subForum.isModerator(memberName, this.subForumName, this.forumName), "member should be moderator after his successful numonation");
        }

        [TestMethod]
        public void test_nominateModerator_on_moderator()
        {
            String moderatorName = this.userModerator.userName;
            Assert.IsFalse(this.subForum.nominateModerator(moderatorName, this.userAdmin.userName, new DateTime(2030, 1, 1), this.subForumName, this.forumName), "nomination of moderator user should not be successful");
            Assert.IsTrue(this.subForum.isModerator(moderatorName, this.subForumName, this.forumName), "moderator user should still br a moderator");
        }

        [TestMethod]
        public void test_nominateModerator_on_null()
        {
            Assert.IsFalse(this.subForum.nominateModerator(null, this.userAdmin.userName, new DateTime(2030, 1, 1), this.subForumName, this.forumName), "nomination of null should not be successful");
        }

        [TestMethod]
        public void test_nominateModerator_on_empty_string_name()
        {
            Assert.IsFalse(this.subForum.nominateModerator("", this.userAdmin.userName, new DateTime(2030, 1, 1), this.subForumName, this.forumName), "nomination with empty string as name should not be successful");
        }


        /******************************end of nominate moderator***************************************/

        /******************************get moderators***************************************

        [TestMethod]
        public void test_getModerators_check_different_list_instances()
        {
            List<String> moderatorList1 = this.subForum.getModerators();
            List<String> moderatorList2 = this.subForum.getModerators();
            Assert.IsFalse(Object.Equals(moderatorList1, moderatorList2), "get moderators should not return a reference to it's inner moderator list");
        }

        [TestMethod]
        public void test_getModerators_check_different_list_elements_instances()
        {
            List<String> moderatorList1 = this.subForum.getModerators();
            List<String> moderatorList2 = this.subForum.getModerators();
            foreach (String s1 in moderatorList1)
            {
                foreach (String s2 in moderatorList2)
                {
                    Assert.IsFalse(Object.Equals(s1, s2), "get moderators should not return a reference to it's inner moderator list");
                }
            }
        }

        [TestMethod]
        public void test_getModerators_check_result_consistent()
        {
            List<String> moderatorList1 = this.subForum.getModerators();
            List<String> moderatorList2 = this.subForum.getModerators();
            Assert.IsTrue(areListsEqual<String>(moderatorList1, moderatorList2), "get moderators returned different list in two calls");
        }


        /******************************end of get moderators***************************************/

        /******************************get threads***************************************

        [TestMethod]
        public void test_getThreads_check_different_list_instances()
        {
            List<IThread> threadList1 = this.subForum.getThreads();
            List<IThread> threadList2 = this.subForum.getThreads();
            Assert.IsFalse(Object.Equals(threadList1, threadList2), "get moderators should not return a reference to it's inner moderator list");
        }

        [TestMethod]
        public void test_getThreads_check_different_list_elements_instances()
        {
            List<IThread> threadList1 = this.subForum.getThreads();
            List<IThread> threadList2 = this.subForum.getThreads();
            foreach (IThread t1 in threadList1)
            {
                foreach (IThread t2 in threadList2)
                {
                    Assert.IsFalse(Object.Equals(t1, t2), "get moderators should not return a reference to it's inner moderator list");
                }
            }
        }

        /******************************end of get threads***************************************/


        private bool areListsEqual<T>(List<T> list1, List<T> list2)
        {
            return ((list1.Count == list2.Count) &&
                            list1.Except(list2).Any());
        }
    }
}
