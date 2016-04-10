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

                if (userName.Equals("") || password.Equals("") || email.Equals(""))
                {
                    Console.WriteLine("one or more of the fields is missing");
                    //return false;
                }
                // should check if the password is strong enough
                bool isNumExist = false;
                bool isSmallKeyExist = false;
                bool isBigKeyExist = false;
                bool isKeyRepeting3Times = false;
                for (int i = 0; i < password.Length; i++)
                {
                    if (password.ElementAt(i) <= '9' && password.ElementAt(i) >= '0')
                    {
                        isNumExist = true;
                    }
                    if (password.ElementAt(i) <= 'Z' && password.ElementAt(i) >= 'A')
                    {
                        isBigKeyExist = true;
                    }
                    if (password.ElementAt(i) <= 'z' && password.ElementAt(i) >= 'a')
                    {
                        isSmallKeyExist = true;
                    }
                    if (i < password.Length - 2 && (password.ElementAt(i).Equals(password.ElementAt(i + 1)) && password.ElementAt(i).Equals(password.ElementAt(i + 2))))
                    {
                        isKeyRepeting3Times = true;
                    }
                    if (!(isNumExist && isSmallKeyExist && isBigKeyExist && !isKeyRepeting3Times))
                    {
                        Console.WriteLine("password isnt strong enough");
                        //return false;
                    }
                }
                // check if the the email is in a correct format
                int index = email.IndexOf("@");
                if (index < 0 || index == email.Length - 1)
                {
                    Console.WriteLine("error in email format");
                    //return false;
                }
                //  send configuration email to the super user's 
                //sendmail(email);

                //adding the user
                SuperUserController.getInstance.addSuperUser(email, password, userName);

                Console.WriteLine("the system was initialized successully");
                //return true;
            }
            else
                return getInstance;
            return null;
        }
        private void sendmail(string email)
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
