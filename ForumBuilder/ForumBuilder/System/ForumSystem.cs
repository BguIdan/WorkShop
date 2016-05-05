using ForumBuilder.Controllers;
using System;
using System.Net.Mail;
using Database;
using System.ServiceModel;
using Service;

namespace ForumBuilder.Systems
{
    public class ForumSystem
    {
        private static ForumSystem singleton;
        Logger logger = Logger.getInstance;
        private static ForumSystem getInstance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ForumSystem();
                }
                return singleton;
            }
        }

        public static ForumSystem initialize(String userName, String password, String email)
        {
            if (singleton == null)
            {
                singleton = new ForumSystem();
                //adding the user
                /*SuperUserController superUserController = SuperUserController.getInstance;
                if (superUserController.addSuperUser(email, password, userName))
                {
                    //  send configuration email to the super user's 
                    //sendmail(email);
                }
                else
                {
                    singleton = null;
                    return null;
                }
                */
                    Logger logger = Logger.getInstance;
                try
                {
                    /*
                    ServiceHost forumService = new ServiceHost(typeof(ForumManager));
                    forumService.Open();
                    logger.logPrint("forum service was initialized under localhost:8081");

                    ServiceHost postService = new ServiceHost(typeof(PostManager));
                    postService.Open();
                    logger.logPrint("post service was initialized under localhost:8082");

                    ServiceHost subForumService = new ServiceHost(typeof(SubForumManager));
                    subForumService.Open();
                    logger.logPrint("sub forum service was initialized under localhost:8083");

                    ServiceHost superUserService = new ServiceHost(typeof(SuperUserManager));
                    superUserService.Open();
                    logger.logPrint("super user service was initialized under localhost:8084");

                    ServiceHost userService = new ServiceHost(typeof(UserManager));
                    userService.Open();
                    logger.logPrint("user service was initialized under localhost:8085");
                     */
                }
                catch (CommunicationException ce)
                {
                    logger.logPrint("failed to initialize services");
                    return null;
                }

                logger.logPrint("The System was initialized successully");
            }
            return getInstance;
        }
        private static void sendmail(string email)
        {
            String ourEmail = "ourEmail@gmail.com";
            MailMessage mail = new MailMessage(ourEmail, email);
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.google.com";
            mail.Subject = "please configure your account";
            mail.Body = "please configure your account";
            client.Send(mail);
        }

        public static int Main(string[] args)
        {
            /*//TODO client server communication POC delete later
             * initialize("gal","pass","gal@gmail.com");
            Console.WriteLine("The service is ready.");
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine();
            Console.ReadLine();*/
            return -1;
        }
    }
}
