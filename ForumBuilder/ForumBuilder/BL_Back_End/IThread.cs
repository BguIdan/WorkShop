﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ForumBuilder.BL_Back_End
{
    public interface IThread
    {
        Boolean addPost(IPost newPost);
        Boolean deleteThread(IThread toDelete);
    }
}
