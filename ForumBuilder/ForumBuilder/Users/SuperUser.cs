﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumBuilder.BL_Back_End;
using ForumBuilder.Systems;

namespace ForumBuilder.Users
{
    public class SuperUser : ISuperUser
    {
        private ForumSystem _system;

        public SuperUser()
        {
            _system = new ForumSystem();
        }

        public ForumSystem _forumSystem
        {
            get { return _system; }
            set { _system = value; }
        }
        
    }
}
