﻿using System;
using System.Collections.Generic;

namespace ForumBuilder.Controllers
{
    interface ISuperUserController : IUserController
    {
        Boolean createForum(String forumName, String descrption, String forumPolicy, String forumRules, List<String> administrators, String superUserName);
    }
}
