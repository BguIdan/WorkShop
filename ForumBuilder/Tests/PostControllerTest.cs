using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumBuilder.Controllers;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;
using ForumBuilder.Systems;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class PostControllerTest
    {
        private IForumController forumController;
        private IPostController postController;
        private ISubForumController subForumController;
        private Forum forum;
        private User userNonMember;
        private User userMember;
        private User userMod;
        private User userAdmin;
        private SuperUser superUser;
        private String forumName = "forum";
        private String subForumName = "subForum";
        private int postId;

        [TestInitialize]
        public void setUp()
        {
            ForumSystem.initialize("guy", "AG36djs", "hello@dskkl.com");
            this.forumController = ForumController.getInstance;
            this.postController = PostController.getInstance;
            this.subForumController = SubForumController.getInstance;
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userMod = new User("mod", "modpass", "mod@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            List<string> adminList = new List<string>();
            adminList.Add(this.userAdmin.userName);
            Dictionary<String, DateTime> modList = new Dictionary<String, DateTime>();
            modList.Add(this.userMod.userName, new DateTime(2030, 1, 1));
            this.forum = new Forum(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList);
            ISuperUserController superUserController = SuperUserController.getInstance;
            superUser = new SuperUser("fkfkf@wkk.com", "1qW", "tomer");
            SuperUserController.getInstance.addSuperUser(superUser.email, superUser.password, superUser.userName);
            superUserController.createForum(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer");
            Assert.IsTrue(this.forumController.registerUser("mem", "mempass", "mem@gmail.com", this.forumName));
            Assert.IsTrue(this.forumController.registerUser("mod", "modpass", "mod@gmail.com", this.forumName));
            Assert.IsTrue(this.forumController.registerUser("admin", "adminpass", "admin@gmail.com", this.forumName));
            Assert.IsTrue(this.forumController.addSubForum(this.forumName, this.subForumName, modList, this.userAdmin.userName));
            Assert.IsTrue(this.subForumController.createThread("headLine", "content", this.userMember.userName, this.forumName, this.subForumName));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
            this.postId = posts[0].id;
        }

        [TestCleanup]
        public void cleanUp()
        {
            this.forumController = null;
            this.subForumController = null;
            this.postController = null;
            this.forum = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userMod = null;
            this.userAdmin = null;
            DemoDB db = DemoDB.getInstance;
            db.clear();
        }

        /**************************add comment*********************************/

        [TestMethod]
        public void test_addComment_mem()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userMember.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_admin()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userAdmin.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_mod()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userMod.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_nonMember()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }


        [TestMethod]
        public void test_addComment_mem_empty_headLine()
        {
            String headLine = "";
            String content = "content";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userMember.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_admin_empty_headLine()
        {
            String headLine = "";
            String content = "content";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userAdmin.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_mod_empty_headLine()
        {
            String headLine = "";
            String content = "content";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userMod.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_nonMember_empty_headLine()
        {
            String headLine = "";
            String content = "content";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_mem_empty_content()
        {
            String headLine = "head";
            String content = "";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userMember.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_admin_empty_content()
        {
            String headLine = "head";
            String content = "";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userAdmin.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_mod_empty_content()
        {
            String headLine = "head";
            String content = "";
            Assert.IsTrue(this.postController.addComment(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 2);
            Post comment = posts[1];
            Assert.AreEqual(comment.title, headLine);
            Assert.AreEqual(comment.content, content);
            Assert.AreEqual(comment.writerUserName, this.userMod.userName);
            Assert.AreEqual(comment.parentId, this.postId);
            Assert.AreNotEqual(comment.id, this.postId);
        }

        [TestMethod]
        public void test_addComment_nonMember_empty_content()
        {
            String headLine = "head";
            String content = "";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_mem_empty_inputs()
        {
            String headLine = "";
            String content = "";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_admin_empty_inputs()
        {
            String headLine = "";
            String content = "";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_mod_empty_inputs()
        {
            String headLine = "";
            String content = "";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_nonMember_empty_inputs()
        {
            String headLine = "head";
            String content = "";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_invalid_postId()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userMember.userName, this.postId + 1));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_null_headLine()
        {
            String headLine = null;
            String content = "content";
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userMember.userName, this.postId + 1));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_null_content()
        {
            String headLine = "head";
            String content = null;
            Assert.IsFalse(this.postController.addComment(headLine, content, this.userMember.userName, this.postId + 1));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_null_user()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsFalse(this.postController.addComment(headLine, content, null, this.postId + 1));
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        /**************************add comment*********************************/

    }
}
