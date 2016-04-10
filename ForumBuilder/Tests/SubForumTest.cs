using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using System.Collections.Generic;
using System.Linq;
using ForumBuilder.BL_Back_End;

namespace Tests
{
    [TestClass]
    public class SubForumTest
    {/*
        private ISubForumController subForum;
        private User memberUser;
        private User moderatorUser;

        [TestInitialize]
        public void setUp()
        {
            this.subForum = new SubForumController();
            this.memberUser = new User("memberUser", "mempass", "mem@gmail.com");
            this.moderatorUser = new User("moderMem", "moderpass", "moder@gmail.com");
            Assert.IsTrue(this.subForum.nominateModerator(moderatorUser.userName, ""));
            //TODO update the nominator to be valid user
        }

        [TestCleanup]
        public void cleanUp()
        {
            this.subForum = null;
        }

        /******************************dismiss moderator***************************************

        [TestMethod]
        public void test_dismissModerator_on_valid_moderator()
        {
            String userModeratorName = this.moderatorUser.userName;
            List<String> moderatorList = this.subForum.getModerators();
            Assert.IsTrue(moderatorList.Contains(userModeratorName), "the moderatorList list should contain the moderator");
            Assert.IsTrue(this.subForum.dismissModerator(userModeratorName), "the dismissal of user moderator should be successful");
            Assert.IsFalse(moderatorList.Contains(userModeratorName), "after dismissal user moderator user should not be moderator anymore");
        }

        [TestMethod]
        public void test_dismissModerator_on_non_moderator()
        {
            String memberUserName = this.memberUser.userName;
            List<String> moderatorList = this.subForum.getModerators();
            Assert.IsFalse(moderatorList.Contains(memberUserName), "the moderatorList list should not contain the non moderator member");
            Assert.IsFalse(subForum.dismissModerator(memberUserName), "dismiss moderator on non moderator should return false");
            Assert.IsFalse(moderatorList.Contains(memberUserName), "the moderatorList list should not contain the non moderator member");
        }

        [TestMethod]
        public void test_dismissModerator_on_null()
        {
            List<String> moderatorList = this.subForum.getModerators();
            Assert.IsFalse(subForum.dismissModerator(null), "dismiss moderator on null should return false");
            List<String> moderatorListAfterDismissal = this.subForum.getModerators();
            Assert.IsTrue(((moderatorList.Count == moderatorListAfterDismissal.Count) &&
                            moderatorList.Except(moderatorListAfterDismissal).Any()), "after unsuccessful dismissal the lists should be equal");
        }

        [TestMethod]
        public void test_dismissModerator_on_empty_string()
        {

            List<String> moderatorList = this.subForum.getModerators();
            Assert.IsFalse(subForum.dismissModerator(""), "dismiss moderator on an empty string should return false");
            List<String> moderatorListAfterDismissal = this.subForum.getModerators();
            Assert.IsTrue(((moderatorList.Count == moderatorListAfterDismissal.Count) &&
                            moderatorList.Except(moderatorListAfterDismissal).Any()), "after unsuccessful dismissal the lists should be equal");
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


        /******************************nominate moderator***************************************

        [TestMethod]
        public void test_nominateModerator_on_member()
        {
            String memberName = this.memberUser.userName;
            List<String> moderatorListPriorNomination = this.subForum.getModerators();
            Assert.IsTrue(this.subForum.nominateModerator(memberName), "nomination of member user should be successful");
            List<String> moderatorListAfterNomination = this.subForum.getModerators();
            Assert.IsTrue(moderatorListAfterNomination.Exists(s => s.Equals(memberName)), "member user should be added to the moderator list");
            Assert.IsTrue((moderatorListAfterNomination.Except(moderatorListPriorNomination).Count() == 1), "moderator list should be changed by one additional string");
        }

        [TestMethod]
        public void test_nominateModerator_on_moderator()
        {
            String moderatorName = this.moderatorUser.userName;
            List<String> moderatorListPriorNomination = this.subForum.getModerators();
            Assert.IsFalse(this.subForum.nominateModerator(moderatorName), "nomination of moderator user should not be successful");
            List<String> moderatorListAfterNomination = this.subForum.getModerators();
            Assert.IsTrue((moderatorListAfterNomination.Except(moderatorListPriorNomination).Count() == 0), "moderator list should not change");
        }

        [TestMethod]
        public void test_nominateModerator_on_null()
        {
            List<String> moderatorListPriorNomination = this.subForum.getModerators();
            Assert.IsFalse(this.subForum.nominateModerator(null), "nomination of null should not be successful");
            List<String> moderatorListAfterNomination = this.subForum.getModerators();
            Assert.IsTrue((moderatorListAfterNomination.Except(moderatorListPriorNomination).Count() == 0), "moderator list should not change");
        }

        [TestMethod]
        public void test_nominateModerator_on_empty_string_name()
        {
            List<String> moderatorListPriorNomination = this.subForum.getModerators();
            Assert.IsFalse(this.subForum.nominateModerator(""), "nomination with empty string as name should not be successful");
            List<String> moderatorListAfterNomination = this.subForum.getModerators();
            Assert.IsTrue((moderatorListAfterNomination.Except(moderatorListPriorNomination).Count() == 0), "moderator list should not change");
        }


        /******************************end of nominate moderator***************************************/

        /******************************delete thread***************************************/

        //TODO
        /******************************end of delete thread***************************************/

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
