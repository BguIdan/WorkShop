using System.Collections.Generic;
using ForumBuilder.Controllers;
using ForumBuilder.BL_Back_End;
using ForumBuilder.BL_DB;
using System;
using System.Net.Mail;
using System.Linq;

namespace ForumBuilder.Systems
{
    class ForumSystem : ISystem
    {
        private static ForumSystem singleton;
        private DemoDB demo_db = DemoDB.getInstance;
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
                //adding the user
                if (SuperUserController.getInstance.addSuperUser(email, password, userName))
                {
                    //  send configuration email to the super user's 
                    sendmail(email);
                }
                else
                    return null;

                Logger.getInstance.logPrint("the system was initialized successully");
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
        /*
        public bool createForum(string forumName, string descrption, string forumPolicy, string forumRules, List<string> administrators)
        {
            return demo_db.createForum(forumName, descrption, forumPolicy, forumRules, administrators);
        }
        */
        public static int Main(string[] args)
        {
            return -1;
        }
    }
}
