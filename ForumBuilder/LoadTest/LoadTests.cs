using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database;
using ForumBuilder.Systems;
using ForumBuilder.Controllers;
using BL_Back_End;
using System.Collections.Generic;

namespace LoadTest
{
    [TestClass]
    public class LoadTests
    {
        private IForumController forumController;
        private IPostController postController;
        private ISubForumController subForumController;
        private Forum forum;
        private User userNonMember;
        private User userMember;
        private User userMod;
        private User userAdmin;
        private User superUser;
        private String forumName = "forum";
        private String subForumName = "subForum";
        private int postId;
       
        [TestMethod]
        public void add_user_and_comment()
        {
          //  DBClass db = DBClass.getInstance;
          //  db.clear();
            //ForumSystem.initialize("guy", "AG36djs", "hello@dskkl.com");
            this.forumController = ForumController.getInstance;
            this.postController = PostController.getInstance;
            this.subForumController = SubForumController.getInstance;
            ISuperUserController superUserController = SuperUserController.getInstance;
            SuperUserController.getInstance.addSuperUser("asjdin@gmail.com", "admiAD1ass", "guy");
            this.userNonMember = new User("nonMem", "nonmemPass", "nonmem@gmail.com");
            this.userMember = new User("mem", "mempass", "mem@gmail.com");
            this.userMod = new User("mod", "modpass", "mod@gmail.com");
            this.userAdmin = new User("admin", "adminpass", "admin@gmail.com");
            superUserController.addUser("admin", "admCD3inpass", "admin@gmail.com", "guy");
            List<string> adminList = new List<string>();
            adminList.Add(this.userAdmin.userName);
            Dictionary<String, DateTime> modList = new Dictionary<String, DateTime>();
            modList.Add(this.userMod.userName, new DateTime(2030, 1, 1));
            this.forum = new Forum(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList);
            this.superUser = new User("tomer", "1qW", "fkfkf@wkk.com");
            SuperUserController.getInstance.addSuperUser(this.superUser.email, superUser.password, superUser.userName);
            superUserController.createForum(this.forumName, "descr", "policy", "the first rule is that you do not talk about fight club", adminList, "tomer");
            this.forumController.registerUser("mem", "mempass", "mem@gmail.com", this.forumName);
            this.forumController.registerUser("mod", "modpass", "mod@gmail.com", this.forumName);
            this.forumController.addSubForum(this.forumName, this.subForumName, modList, this.userAdmin.userName);
            this.subForumController.createThread("headLine", "content", this.userMember.userName, this.forumName, this.subForumName);
            List<Post> posts = this.postController.getAllPosts(this.forumName, this.subForumName);
            this.postId = posts[0].id;

            String headLine = "head";
            String content = "content";
            this.postController.addComment(headLine, content, this.userMember.userName, this.postId);
           // db.clear();


        }
    }
}
