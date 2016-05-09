using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PL.proxies;


namespace PL
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private ForumManagerClient _fMC;
        private String _forumToRegister;

        public SignUpWindow(ForumManagerClient forumManager, String forumName)
        {
            if (forumName == null)
                throw new Exception();
            InitializeComponent();
            _fMC = forumManager;
            _forumToRegister = forumName;
        }

        private void UserRegistration(object sender, RoutedEventArgs e)
        {
            string userName = name.Text;
            string pass = Password.Password;
            string userMail = mail.Text;
            /*if (mail==null || pass == null  || name == null)
            {
                MessageBox.Show("Please fill all the required details, make sure password include at least 5 letters");
            }*/
            bool suc = _fMC.registerUser(userName,pass,userMail,_forumToRegister);
            if (suc == false)
            {
                MessageBox.Show("Failed to register. One or more of the details is wrong");
            }
            else
            {
                MessageBox.Show(userName + "  Registration succeeded!");
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        } 
    }
}
