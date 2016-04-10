using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Users;

namespace Tests
{
    [TestClass]
    public class UserTest
    {
        private User user;

        [TestInitialize]
        public void setUp()
        {
            this.user = new User("testUser", "testpass", "test@user.com");
        }

        [TestCleanup]
        public void cleanUp()
        {
            this.user = null;
        }

        /*******************************get user name*********************************/
        [TestMethod]
        public void test_getUserName_valid_return()
        {
            String userName = this.user.userName;
            Assert.IsNotNull(userName, "returned user name should not be null");
            Assert.IsTrue(userName.Length > 0, "user name should be greater than 0");
        }

        /*******************************end of get user name*********************************/

        /*******************************get add friend*********************************/

        /*******************************end of add friend*********************************/


        /*******************************get delete friend*********************************/

        /*******************************end of delete friend*********************************/


        /*******************************notify user via mail*********************************/

        /*******************************end of notify user via mail*********************************/


        /*******************************get user name*********************************/

        /*******************************end of get user name*********************************/




        /*******************************get user name*********************************/

        /*******************************end of get user name*********************************/




        /*******************************get user name*********************************/

        /*******************************end of get user name*********************************/




        /*******************************get user name*********************************/

        /*******************************end of get user name*********************************/




        /*******************************get user name*********************************/

        /*******************************end of get user name*********************************/


    }
}
