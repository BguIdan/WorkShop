using System;
using ForumBuilder.BL_DB;


namespace ForumBuilder.Controllers
{
    public class ThreadController
    {
        private static ThreadController singleton;
        DemoDB demoDB = DemoDB.getInstance;
        Systems.Logger logger = Systems.Logger.getInstance;
        
        public static ThreadController getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ThreadController();
                    Systems.Logger.getInstance.logPrint("Thread contoller created");
                }
                return singleton;
            }

        }


    }
}
