using ForumBuilder.Controllers;
using System;
using System.Net.Mail;
using Database;
using System.ServiceModel;
using Service;
using System.ComponentModel.DataAnnotations;
using ForumBuilder.Common.ServiceContracts;
//TODO gal: remove after use
using System.Threading;
//

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

        //TODO gal: should this really return forumSystem instance?
        public static ForumSystem initialize(String userName, String password, String email)
        {
            if (singleton == null)
            {
                singleton = new ForumSystem();
                //adding the user
                SuperUserController superUserController = SuperUserController.getInstance;
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
                
                    Logger logger = Logger.getInstance;
                try
                {

                    /*//TODO should be removed for the services to be published
                     * //for this to work the exe/vs should be run in administrator mode
                     */
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
            Console.WriteLine(  "welcome to your forum builder!\n" +
                                "please insert your desired user name:");
            String username = getUserName();
            String password = getUserPassword();
            String email = getEmail();

            initialize(username, password, email);
            runServer(username, password, email);
            return 0;
        }

        private static String getUserName()
        {
            String userName = Console.ReadLine();
            String ans;
            while (true)
            {
                Console.WriteLine("please confirm your user name: \"" + userName + "\"  yes/no");
                ans = Console.ReadLine();
                if (ans.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (ans.Equals("no", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("user name: \"" + userName + "\" was rejected, please insert new one");
                    userName = Console.ReadLine();
                }
            }
            return userName;
        }

        private static String getUserPassword()
        {
            Console.WriteLine("please insert your desired password");
            String userPass = Console.ReadLine();
            String ans;
            while (true)
            {
                Console.WriteLine("please confirm your password: \"" + userPass + "\"  yes/no");
                ans = Console.ReadLine();
                if (ans.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (ans.Equals("no", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("password: \"" + userPass + "\" was rejected, please insert new one");
                    userPass = Console.ReadLine();
                }
            }
            return userPass;
        }

        private static String getEmail()
        {
            Console.WriteLine("please insert your email address");
            String email = Console.ReadLine();
            String ans;
            while (true)
            {
                if (!new EmailAddressAttribute().IsValid(email))
                {
                    Console.WriteLine("email address is invalid, please insert a new one");
                    email = Console.ReadLine();
                    continue;
                }
                Console.WriteLine("please confirm your email address: \"" + email + "\"  yes/no");
                ans = Console.ReadLine();
                if (ans.Equals("yes", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (ans.Equals("no", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("email address: \"" + email + "\" was rejected \nplease insert you valid email address");
                    email = Console.ReadLine();
                }
            }
            return email;
        }

        public static void runServer(String userName, String password, String email)
        {
            Console.WriteLine("server is running. \n" +
                                "super user credentials:" +
                                "user name: " + userName + "  password: " + password + 
                                "  email: " + email);
            while (true)
            {
            }
        }
    }
}
