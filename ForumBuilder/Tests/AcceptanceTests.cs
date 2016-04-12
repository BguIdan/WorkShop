using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class AcceptanceTests
    {
        Service.ForumManager forumMan = Service.ForumManager.getInstance;
        Service.SuperUserManager superUserMan = Service.SuperUserManager.getInstance;
        Service.SubForumManager subForumMan = Service.SubForumManager.getInstance;
        Service.UserManager userMan = Service.UserManager.getInstance;
        Service.PostManager postMan = Service.PostManager.getInstance;

        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Test_register_to_forum_withWrongInputs()
        {
            List<string> adminList = new List<string>();
            adminList.Add("admin1");
            adminList.Add("admin2");
            superUserMan.createForum("forumName", "descrption", "forumPolicy", "forumRules", adminList, "superUserName");
            Assert.IsTrue(forumMan.registerUser("admin1", "passWord1", "jksdjk@xc.com", "foumName"));
            Assert.IsTrue(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", "foumName"));
            Assert.IsFalse(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", "foumName"));
            Assert.IsTrue(forumMan.registerUser("mem1", "passWor1", "fff@xc.com", "foumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "fff@xc.com", "foumNames"));
            Assert.IsFalse(forumMan.registerUser("", "passWor1", "fff@xc.com", "foumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "", "fff@xc.com", "foumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "", "foumName"));
            Assert.IsFalse(forumMan.registerUser("mem2", "passWor1", "fff@xc.com", "foumName"));
        }
        [TestMethod]
        public void Test_register_to_forum_Functionality()
        {
            List<string> adminList = new List<string>();
            adminList.Add("admin1");
            adminList.Add("admin2");
            superUserMan.createForum("forumName", "descrption", "forumPolicy", "forumRules", adminList, "superUserName");
            Assert.IsTrue(forumMan.registerUser("admin1", "passWord1", "jksdjk@xc.com", "foumName"));
            Assert.IsFalse(userMan.sendPrivateMessage("admin1", "admin2", "hello"));
            Assert.IsFalse(userMan.addFriend("admin1", "admin2"));
            Assert.IsFalse(userMan.addFriend("admin2", "admin1"));
            Assert.IsTrue(forumMan.registerUser("admin2", "passWord2", "jkkkk@xc.com", "foumName"));
            Assert.IsTrue(userMan.sendPrivateMessage("admin1", "admin2", "its me"));
            Assert.IsTrue(userMan.addFriend("admin1", "admin2"));
            Assert.IsTrue(userMan.addFriend("admin2", "admin1"));
            Assert.IsFalse(userMan.addFriend("admin2", "admin1"));
            Assert.IsFalse(userMan.addFriend("admin1", "mem1"));
            Assert.IsFalse(userMan.addFriend("mem1", "admin1"));
            Assert.IsFalse(userMan.sendPrivateMessage("mem1", "admin1", "i was wonder"));
            Assert.IsTrue(forumMan.registerUser("mem1", "passWor1", "fff@xc.com", "foumName"));
            Assert.IsTrue(userMan.addFriend("admin1", "mem1"));
            Assert.IsTrue(userMan.addFriend("mem1", "admin1"));
            Assert.IsTrue(userMan.sendPrivateMessage("mem1", "admin1", "when the test gona be done"));
        }
    }
}
