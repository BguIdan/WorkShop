using ForumBuilder.Controllers;
using System;
using System.Net.Mail;
using Database;
using System.ServiceModel;
using Service;
using System.ComponentModel.DataAnnotations;
using ForumBuilder.Common.ServiceContracts;
using System.Collections.Generic;
using BL_Back_End;

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
                SuperUserController superUserController = SuperUserController.getInstance;
                if (superUserController.addSuperUser(email, password, userName))
                {
                    //  send configuration email to the super user's 
                    //sendmail(email);
                }
                /*else
                {
                    singleton = null;
                    return null;
                }*/
                
                    Logger logger = Logger.getInstance;
                try
                {

                    /*
                     * //for this to work the exe/vs should be run in administrator mode
                     */
                    ServiceHost forumService = new ServiceHost(typeof(ForumManager));
                    forumService.Open(); 
                    logger.logPrint("forum service was initialized under localhost:8081",0);
                    logger.logPrint("forum service was initialized under localhost:8081",1);

                    ServiceHost postService = new ServiceHost(typeof(PostManager));
                    postService.Open();
                    logger.logPrint("post service was initialized under localhost:8082",0);
                    logger.logPrint("post service was initialized under localhost:8082",1);

                    ServiceHost subForumService = new ServiceHost(typeof(SubForumManager));
                    subForumService.Open();
                    logger.logPrint("sub forum service was initialized under localhost:8083",0);
                    logger.logPrint("sub forum service was initialized under localhost:8083",1);

                    ServiceHost superUserService = new ServiceHost(typeof(SuperUserManager));
                    superUserService.Open();
                    logger.logPrint("super user service was initialized under localhost:8084",0);
                    logger.logPrint("super user service was initialized under localhost:8084",1);

                    ServiceHost userService = new ServiceHost(typeof(UserManager));
                    userService.Open();
                    logger.logPrint("user service was initialized under localhost:8085",0);
                    logger.logPrint("user service was initialized under localhost:8085",1);
                }
                catch (CommunicationException)
                {
                    logger.logPrint("failed to initialize services",0);
                    logger.logPrint("failed to initialize services",2);
                    return null;
                }

                logger.logPrint("The System was initialized successully",0);
                logger.logPrint("The System was initialized successully",1);
            }
            else
            {
                SuperUserController superUserController = SuperUserController.getInstance;
                superUserController.addSuperUser(email, password, userName);
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
            //TODO gal: remove later
            //DBClass.getInstance.clear();
            var item = DBClass.getInstance;
            Console.WriteLine(  "welcome to your forum builder!\n" +
                                "please insert your desired user name:");
            String username = "idan";//getUserName();
            String password = "idanA1";//getUserPassword();
            String email = "d@d.d";//getEmail();

            initialize(username, password, email);
            ForumPolicy fp = new ForumPolicy("p", true, 0, true, 180,1, true, true, 2);
            List<String> list = new List<String>();
            list.Add("idan");
            if (!SuperUserController.getInstance.createForum("f", "f",fp, list, "idan").Equals("Forum " + "f" + " creation success"))
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!1");
            if (!SuperUserController.getInstance.createForum("f2", "f", fp, list, "idan").Equals("Forum " + "f2" + " creation success"))
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!2");
            if (!ForumController.getInstance.registerUser("g1", "gG1", "g@g.g","sad","bad" ,"f").Equals("Register user succeed"))
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!3");
            if (!ForumController.getInstance.registerUser("g2", "gG1", "a@a.a","good", "awesome", "f").Equals("Register user succeed"))
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!4");
            if (!ForumController.getInstance.registerUser("g3", "gG1", "b@b.b", "good", "awesome", "f2").Equals("Register user succeed"))
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!5");
            if (!ForumController.getInstance.nominateAdmin("g1", "idan", "f").Equals("admin nominated successfully"))
                Console.WriteLine("!!!!!!!!!!!!!!!!!!!6");
            Dictionary<String, DateTime> d = new Dictionary<String, DateTime>();
            d.Add("g1", new DateTime(2017, 1, 1));
            if (!ForumController.getInstance.addSubForum("f", "f1",d , "g1").Equals("sub-forum added"))
                //Console.WriteLine("!!!!!!!!!!!!!!!!!!!");

            runServer(username, password, email);
            //DBClass.getInstance.clear();
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
            /*while (true)
            {
            }*/
            Console.ReadLine();
        }
    }
}
