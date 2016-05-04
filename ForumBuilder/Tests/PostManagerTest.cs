
using BL_Back_End;
using Database;
using ForumBuilder.Common.DataContracts;
using ForumBuilder.Common.ServiceContracts;
using ForumBuilder.Systems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PL.proxies;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class PostManagerTest
    {

        private IForumManager forumManager;
        private IPostManager postManager;
        private ISubForumManager subForumManager;
        private ForumData forum;
        private UserData userNonMember;
        private UserData userMember;
        private UserData userMod;
        private UserData userAdmin;
        private UserData superUser;
        private String forumName = "forum";
        private String subForumName = "subForum";
        private int postId;

        const int INITIAL_COMMENT_COUNT = 1;

        [TestInitialize]
        public void setUp()
        {
            ForumSystem.initialize("guy", "AG36djs", "hello@dskkl.com");
            this.forumManager = new ForumManagerClient();
            this.postManager = new PostManagerClient();
            this.subForumManager = new SubForumManagerClient();
            this.userNonMember = new UserData("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new UserData("mem", "mempass", "mem@gmail.com");
            this.userMod = new UserData("mod", "modpass", "mod@gmail.com");
            this.userAdmin = new UserData("admin", "adminpass", "admin@gmail.com");
            List<string> adminList = new List<string>();
            adminList.Add(this.userAdmin.userName);
            Dictionary<String, DateTime> modList = new Dictionary<String, DateTime>();
            modList.Add(this.userMod.userName, new DateTime(2030, 1, 1));
            this.forum = new ForumData(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club");
            ISuperUserManager superUserManager = new SuperUserManagerClient();
            this.superUser = new UserData("tomer", "1qW", "fkfkf@wkk.com");
            superUserManager.createForum(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer");
            Assert.IsTrue(this.forumManager.registerUser("mem", "mempass", "mem@gmail.com", this.forumName));
            Assert.IsTrue(this.forumManager.registerUser("mod", "modpass", "mod@gmail.com", this.forumName));
            Assert.IsTrue(this.forumManager.registerUser("admin", "adminpass", "admin@gmail.com", this.forumName));
            Assert.IsTrue(this.forumManager.addSubForum(this.forumName, this.subForumName, modList, this.userAdmin.userName));
            Assert.IsTrue(this.subForumManager.createThread("headLine", "content", this.userMember.userName, this.forumName, this.subForumName));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
            this.postId = posts[0].id;
        }

        [TestCleanup]
        public void cleanUp()
        {
            this.forumManager = null;
            this.subForumManager = null;
            this.postManager = null;
            this.forum = null;
            this.userNonMember = null;
            this.userMember = null;
            this.userMod = null;
            this.userAdmin = null;
            DBClass db = DBClass.getInstance;
            db.clear();
        }

        /**************************add comment*********************************/

        [TestMethod]
        public void test_addComment_mem()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }


        [TestMethod]
        public void test_addComment_mem_empty_headLine()
        {
            String headLine = "";
            String content = "content";
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_mem_empty_content()
        {
            String headLine = "head";
            String content = "";
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsTrue(this.postManager.addPost(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
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
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_mem_empty_inputs()
        {
            String headLine = "";
            String content = "";
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_admin_empty_inputs()
        {
            String headLine = "";
            String content = "";
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userAdmin.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_mod_empty_inputs()
        {
            String headLine = "";
            String content = "";
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userMod.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_nonMember_empty_inputs()
        {
            String headLine = "head";
            String content = "";
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userNonMember.userName, this.postId));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_invalid_postId()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId + 1));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_null_headLine()
        {
            String headLine = null;
            String content = "content";
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId + 1));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_null_content()
        {
            String headLine = "head";
            String content = null;
            Assert.IsFalse(this.postManager.addPost(headLine, content, this.userMember.userName, this.postId + 1));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_addComment_null_user()
        {
            String headLine = "head";
            String content = "content";
            Assert.IsFalse(this.postManager.addPost(headLine, content, null, this.postId + 1));
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, 1);
        }

        /**************************end of add comment*********************************/

        /**************************end of remove comment*********************************/

        [TestMethod]
        public void test_removeComment_mem_noSubComments()
        {
            test_addComment_mem();
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.writerUserName == this.userMember.userName)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.deletePost(id, this.userMember.userName));
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
            }
        }

        [TestMethod]
        public void test_removeComment_admin_noSubComments()
        {
            test_addComment_admin();
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.writerUserName == this.userAdmin.userName)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.deletePost(id, this.userAdmin.userName));
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
            }
        }

        [TestMethod]
        public void test_removeComment_not_owner_by_mem_noSubComments()
        {
            test_addComment_admin();
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == this.postId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsFalse(this.postManager.deletePost(id, this.userMember.userName));
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.id == id)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should not be deleted by other user");
        }

        [TestMethod]
        public void test_removeComment_not_owner_admin_noSubComments()
        {
            test_addComment_mem();
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == this.postId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.deletePost(id, this.userAdmin.userName));
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.id == id)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNull(newPost, "the added post should not be deleted by other user");
        }

        [TestMethod]
        public void test_removeComment_mem_withSubComments()
        {
            test_addComment_mem();
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == this.postId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userMember.userName, id));
            Assert.IsTrue(this.postManager.addPost("head2", "subcomment2", this.userAdmin.userName, id));
            Assert.IsTrue(this.postManager.deletePost(id, this.userMember.userName));
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
                Assert.AreNotEqual(p.parentId, id);
            }
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_removeComment_admin_withSubComments()
        {
            test_addComment_admin();
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.writerUserName == this.userAdmin.userName)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userMember.userName, id));
            Assert.IsTrue(this.postManager.addPost("head2", "subcomment2", this.userAdmin.userName, id));
            Assert.IsTrue(this.postManager.deletePost(id, this.userAdmin.userName));
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
                Assert.AreNotEqual(p.parentId, id);
            }
            Assert.AreEqual(posts.Count, 1);
        }

        [TestMethod]
        public void test_removeComment_subComment_without_subcomments_by_mem()
        {
            int commentCounter = INITIAL_COMMENT_COUNT;
            test_addComment_mem();
            commentCounter++;
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.writerUserName == this.userMember.userName)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int parentId = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userMember.userName, parentId));
            commentCounter++;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == parentId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.deletePost(id, this.userMember.userName));
            commentCounter--;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, commentCounter);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
            }
        }

        [TestMethod]
        public void test_removeComment_subComment_without_subcomments_by_admin()
        {
            int commentCounter = INITIAL_COMMENT_COUNT;
            test_addComment_admin();
            commentCounter++;
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.writerUserName == this.userAdmin.userName)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int parentId = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userAdmin.userName, parentId));
            commentCounter++;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == parentId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.deletePost(id, this.userAdmin.userName));
            commentCounter--;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, commentCounter);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
            }
        }

        [TestMethod]
        public void test_removeComment_subComment_with_subcomments_by_mem()
        {
            int commentCounter = INITIAL_COMMENT_COUNT;
            test_addComment_mem();
            commentCounter++;
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == this.postId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int parentId = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userMember.userName, parentId));
            commentCounter++;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == parentId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "content", this.userMember.userName, id));
            Assert.IsTrue(this.postManager.addPost("head", "content", this.userAdmin.userName, id));
            commentCounter += 2;
            Assert.IsTrue(this.postManager.deletePost(id, this.userMember.userName));
            commentCounter -= 3;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, commentCounter);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
                Assert.AreNotEqual(p.parentId, id);
            }
        }

        [TestMethod]
        public void test_removeComment_subComment_with_subcomments_by_admin()
        {
            int commentCounter = INITIAL_COMMENT_COUNT;
            test_addComment_admin();
            commentCounter++;
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == this.postId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int parentId = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userAdmin.userName, parentId));
            commentCounter++;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == parentId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int id = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "content", this.userMember.userName, id));
            Assert.IsTrue(this.postManager.addPost("head", "content", this.userAdmin.userName, id));
            commentCounter += 2;
            Assert.IsTrue(this.postManager.deletePost(id, this.userAdmin.userName));
            commentCounter -= 3;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(posts.Count, commentCounter);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
                Assert.AreNotEqual(p.parentId, id);
            }
        }

        [TestMethod]
        public void test_removeComment_subcomment_with_nested_subComments()
        {
            int commentCounter = INITIAL_COMMENT_COUNT;
            test_addComment_admin();
            commentCounter++;
            List<Post> posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Post newPost = null;
            foreach (Post p in posts)
            {
                if (p.writerUserName == this.userAdmin.userName)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int parentId = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "subcomment", this.userAdmin.userName, parentId));
            commentCounter++;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            newPost = null;
            foreach (Post p in posts)
            {
                if (p.parentId == parentId)
                {
                    newPost = p;
                    break;
                }
            }
            Assert.IsNotNull(newPost, "the added post should exist");
            int originalId = newPost.id;
            int id = newPost.id;
            Assert.IsTrue(this.postManager.addPost("head", "content", this.userMember.userName, originalId));
            Assert.IsTrue(this.postManager.addPost("head", "content", this.userAdmin.userName, originalId));
            commentCounter += 2;
            for (int i = 0; i < 5; i++)
            {
                posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
                newPost = null;
                foreach (Post p in posts)
                {
                    if (p.parentId == id)
                    {
                        newPost = p;
                        break;
                    }
                }
                Assert.IsNotNull(newPost, "the added post should exist");
                id = newPost.id;
                Assert.IsTrue(this.postManager.addPost("head", "content", this.userMember.userName, id));
                Assert.IsTrue(this.postManager.addPost("head", "content", this.userAdmin.userName, id));
                commentCounter += 2;
            }
            Assert.IsTrue(this.postManager.deletePost(originalId, this.userAdmin.userName));
            commentCounter -= 13;
            posts = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(commentCounter, posts.Count);
            foreach (Post p in posts)
            {
                Assert.AreNotEqual(p.id, id);
                Assert.AreNotEqual(p.parentId, id);
            }
            Assert.AreEqual(commentCounter, 2);
        }


        [TestMethod]
        public void test_removeComment_invalidId_notExists()
        {
            int invalidId = INITIAL_COMMENT_COUNT + 1;
            Boolean foundInvalid;
            List<Post> postsPre = this.postManager.getAllPosts(this.forumName, this.subForumName);
            while (true)
            {
                foundInvalid = true;
                foreach (Post p in postsPre)
                {
                    if (p.id == invalidId)
                    {
                        foundInvalid = false;
                        break;
                    }
                }
                if (foundInvalid)
                    break;
                else
                    invalidId++;
            }
            Assert.IsFalse(this.postManager.deletePost(invalidId, this.userMember.userName));
            List<Post> postsAfter = this.postManager.getAllPosts(this.forumName, this.subForumName);
            Assert.AreEqual(postsPre.Count, postsAfter.Count);
        }

    }
}
