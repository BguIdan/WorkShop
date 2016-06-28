using BL_Back_End;
using Database;
using ForumBuilder.Controllers;
using ForumBuilder.Systems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class loadTest
    {
        private IForumController forumController;
        private Forum forum;
        private User superUser;
        private String forumName = "forum";

        const int INITIAL_COMMENT_COUNT = 1;

        [TestInitialize]
        public void setUp()
        {
            DBClass db = DBClass.getInstance;
            db.clear();
            ForumSystem.initialize("guy", "AG36djs", "hello@dskkl.com");
            ISuperUserController superUserController = SuperUserController.getInstance;
            this.superUser = new User("guy", "AG36djs", "hello@dskkl.com", new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day));
            SuperUserController.getInstance.addSuperUser(this.superUser.email, this.superUser.password, this.superUser.userName);
            ForumPolicy fp = new ForumPolicy("p", true, 0, true, 180, 1, true, true, 5, 0, new List<string>());
            this.forum = new Forum(this.forumName, "descr", fp, new List<string>());
            this.superUser = new User("tomer", "1qW", "fkfkf@wkk.com", new DateTime(DateTime.Today.Year - 1, DateTime.Today.Month, DateTime.Today.Day));
            SuperUserController.getInstance.addSuperUser(this.superUser.email, superUser.password, superUser.userName);
            superUserController.createForum(this.forumName, "descr", fp, new List<string>(), "tomer");

        }

        [TestCleanup]
        public void cleanUp()
        {
            this.forumController = null;
            this.forum = null;
            DBClass db = DBClass.getInstance;
            db.clear();
        }


        [TestMethod]
        public void test_1()
        {
            var threads = new List<System.Threading.Thread>();
            for (int p = 0; p < 10; p++)
            {
                threads.Add(new System.Threading.Thread(myTask));
            }
            //start all threads
            foreach (var thread in threads)
            {
                thread.Start();
            }

            //waiting for all threads to complete
            for (int i = 0; i < threads.Count; i++)
            {
                threads[i].Join();
            }
            //they removed the forum here

        }

        private void myTask()
        {
            int p = System.Threading.Thread.CurrentThread.ManagedThreadId;
            this.forumController.registerUser("userName" + p, "idanA1", "d@d.d", "tomer", "tomer", forum.forumName);
        }
    }
}
