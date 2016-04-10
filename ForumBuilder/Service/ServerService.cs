using ForumBuilder.Controllers;

namespace Service
{
    class ServerService
    {
        private static ServerService singleton;
        

        private ServerService()
        {

        }

        public static ServerService getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ServerService();
                }
                return singleton;
            }

        }

        public static int Main(string[] args)
        {
            return -1;
        }

    }
}
