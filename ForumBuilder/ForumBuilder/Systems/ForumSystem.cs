﻿using System.Collections.Generic;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;

namespace ForumBuilder.Systems
{
    class ForumSystem : ISystem
    {
        private DemoDB demo_db;

        public ForumSystem()
        {
            demo_db = DemoDB.getInstance;
        }

        private List<IForumController> forums;

        public List<IForumController> _forums
        {
            get{ return forums;}
            set{forums = value;}
        }

        public bool initialize(string userName, string password, string email)
        {
            return demo_db.initialize(userName, password, email);
        }

        public bool createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            return demo_db.createForum(forumName, descrption, forumPolicy, forumRules, administrators);
        }

        public static int Main(string[] args)
        {
            return -1;
        }
    }
}