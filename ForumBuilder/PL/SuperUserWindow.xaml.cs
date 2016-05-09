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
using System.ServiceModel;
using PL.notificationHost;
using PL.proxies;
using ForumBuilder.Common.DataContracts;

namespace PL
{
    /// <summary>
    /// Interaction logic for SuperUserWindow.xaml
    /// </summary>
    public partial class SuperUserWindow : Window
    {
        private SuperUserManagerClient _sUMC;
        private ForumManagerClient _fMC;
        private UserData _myUser;

        public SuperUserWindow(String userName, String password, String email)
        {
            InitializeComponent();
            _sUMC = new SuperUserManagerClient();
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _myUser = new UserData(userName, password, email);
        }

        private void MenuItem_Actions(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "CreateForum": { createNewForum(); } break;
                case "Set": { setPreferences(); } break;
                case "CreateSub": { createSub(); } break;
                case "Del": { Delete(); } break;
                case "Createuser": { createUser(); } break;
                case "logoutMenu": { logout(); } break;
            }
        }

        private void logout()
        {
            MainWindow _mw = new MainWindow();
            _mw.Show();
            this.Close();
        }

        private void MenuItem_View(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "ViewForums": { showForumList(); } break;
            }

        }

        private void showForumList()
        {

        }

        private void createNewForum()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Collapsed;
            createForum.Visibility = System.Windows.Visibility.Visible;
        }

        private void setPreferences()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            createForum.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Visible;
        }

        private void createSub()
        {
            //TODO: complete the mehtod
        }

        private void Delete()
        {
            //TODO: complete the mehtod
        }

        private void createUser()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            createForum.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn_CreateNewForum(object sender, RoutedEventArgs e)
        {
            string forumName = newForumName.Text;
            string desc = newForumDescription.Text;
            string rules = newForumRules.Text;
            string policy = newForumPolicy.Text;
            string administrators = newAdminUserName.Text;
            List<string> admins = administrators.Split(',').ToList();
            Boolean isCreated = _sUMC.createForum(forumName, desc, policy, rules, admins, _myUser.userName);
            if (isCreated)
            {
                MessageBox.Show("Forum: " + forumName + " was successfully created!");
                ForumData newForum = _fMC.getForum(forumName);
                MainMenu.Visibility = System.Windows.Visibility.Visible;
                createForum.Visibility = System.Windows.Visibility.Collapsed;

                //TODO: direct to the new forum window
                //TODO: send mail to every forum manager?
            }
            else { MessageBox.Show("Something went wrong the forum wasn't created, try again."); }
        }

        private void btn_SetForumPref(object sender, RoutedEventArgs e)
        {
            string forumToSet = ForumNameToSet.Text;
            //TODO: direct to the forum window
        }

        private void btn_CreateNewUser(object sender, RoutedEventArgs e)
        {
            string name = userName.Text;
            string pass = Password.Password;
            string userMail = email.Text;

            bool succ = _sUMC.addUser(name, pass, userMail, _myUser.userName);
            if (!succ)
            {
                MessageBox.Show("Failed to create user!");
            }
            else
            {
                MessageBox.Show(name + "  creation succeeded!");
                MainMenu.Visibility = System.Windows.Visibility.Visible;
                createUserWin.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

    }
}
        