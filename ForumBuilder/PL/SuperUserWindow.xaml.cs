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
        private ForumData _fData;

        public SuperUserWindow(String userName, String password, String email)
        {
            InitializeComponent();
            _sUMC = new SuperUserManagerClient();
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _myUser = new UserData(userName, password, email);
        }

        private void MenuItem_View(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "viewReports": { showList(); } break;
            }

        }

        private void showList()
        {
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Collapsed;
            createForum.Visibility = System.Windows.Visibility.Collapsed;
            viewGrid.Visibility = System.Windows.Visibility.Visible;
            List<String> members = _sUMC.getSuperUserReportOfMembers(_myUser.userName);
            numOfFOrums.Text = "Number of forums :  " + (_sUMC.SuperUserReportNumOfForums(_myUser.userName)).ToString();
            foreach (string member in members)
            {
                memberListBox.Items.Add(member);
            }
        }

        private void MenuItem_Actions(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "CreateForum": { createNewForum(); } break;
                case "Set": { setPreferences(); } break;
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


        private void createNewForum()
        {
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Collapsed;
            viewGrid.Visibility = System.Windows.Visibility.Collapsed;
            createForum.Visibility = System.Windows.Visibility.Visible;
        }

        private void setPreferences()
        {
            createForum.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Collapsed;
            viewGrid.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Visible;
        }

        private void createUser()
        {
            createForum.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            viewGrid.Visibility = System.Windows.Visibility.Collapsed;
            createUserWin.Visibility = System.Windows.Visibility.Visible;
        }

        private void btn_CreateNewForum(object sender, RoutedEventArgs e)
        {
            string forumName = newForumName.Text;
            string desc = newForumDescription.Text;
            string administrators = newAdminUserName.Text;
            List<string> admins = administrators.Split(',').ToList();
            Boolean isCreated = _sUMC.createForum(forumName, desc, new ForumPolicyData() , admins, _myUser.userName);
            if (isCreated)
            {
                MessageBox.Show("Forum: " + forumName + " was successfully created!");
                createForumDialog.Visibility = System.Windows.Visibility.Visible;
                createForumDialog.Focusable = true;
            }
            else { MessageBox.Show("Something went wrong the forum wasn't created, try again."); }
        }

        private void btn_toSetPref(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.Name.Equals("yesBtn"))
            {
                createForum.Visibility = System.Windows.Visibility.Collapsed;
                createForumDialog.Visibility = System.Windows.Visibility.Collapsed;
                createForumDialog.Focusable = false;
                setPreferencesWin.Visibility = System.Windows.Visibility.Visible;
            }
            else 
            { 
                createForum.Visibility = System.Windows.Visibility.Collapsed;
                createForumDialog.Visibility = System.Windows.Visibility.Collapsed;
                createForumDialog.Focusable = false;
                newForumName.Clear();
                newForumDescription.Clear();
                newAdminUserName.Clear();
            }
        }

        private void btn_SetForumPref(object sender, RoutedEventArgs e)
        {
            setPreferencesWin.Focusable = false;
            MyDialog.Visibility = System.Windows.Visibility.Visible;
            MyDialog.Focusable = true;
        }

        private void btn_ToSetForumPref(object sender, RoutedEventArgs e)
        {
            String temp = "";
            var btn = sender as Button;
            if (btn.Name.Equals("yesButton"))
            {
                temp = yesButton.Content.ToString();
            }
            else { temp = noButton.Content.ToString(); }
            setPref(temp);
        }

        private void setPref(String isDone)
        {
            MyDialog.Focusable = false;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;
            string forumToSet = ForumNameToSet.Text;
            if (!forumToSet.Equals("")) { _fData = _fMC.getForum(forumToSet); }
            if (isDone.Equals("Yes"))
            {
                if (_fData != null)
                {
                    bool toChange = descCheck.IsChecked.Value;
                    if (toChange)
                    {
                        _fData.description = ForumDescToSet.Text;
                    }
                    toChange = policyCheck.IsChecked.Value;
                    if (toChange)
                    {
                        _fData.forumPolicy.policy = ForumPolicyToSet.Text;
                    }
                    toChange = qIdentifying.IsChecked.Value;
                    if (toChange)
                    {
                        _fData.forumPolicy.isQuestionIdentifying = true;
                    }
                    toChange = deleteMessages.IsChecked.Value;
                    if (toChange)
                    {
                        _fData.forumPolicy.isQuestionIdentifying = true;
                    }
                    int passExpirationTime = Int32.Parse(PassCombo.SelectedItem.ToString());
                    _fData.forumPolicy.timeToPassExpiration = passExpirationTime;
                    int seniorityChoosen = Int32.Parse(TimeCombo.SelectedItem.ToString());
                    _fData.forumPolicy.seniorityInForum = seniorityChoosen;
                    int numerOfModerators = Int32.Parse(NumberCombo.SelectedItem.ToString());
                    _fData.forumPolicy.minNumOfModerator = numerOfModerators;
                    toChange = Capital.IsChecked.Value;
                    if (toChange)
                    {
                        _fData.forumPolicy.hasCapitalInPassword = true;
                    }
                    toChange = Number.IsChecked.Value;
                    if (toChange)
                    {
                        _fData.forumPolicy.hasNumberInPassword = true;
                    }
                    int minLengthOfPass = Int32.Parse(LengthCombo.SelectedItem.ToString());
                    _fData.forumPolicy.minLengthOfPassword = minLengthOfPass;
                    _fMC.setForumPreferences(_fData.forumName, _fData.description, _fData.forumPolicy, _myUser.userName);
                    MessageBox.Show("Preferences was successfully changed!");
                    descCheck.IsChecked = false;
                    policyCheck.IsChecked = false;
                    qIdentifying.IsChecked = false;
                    deleteMessages.IsChecked = false;
                    Capital.IsChecked = false;
                    Number.IsChecked = false;
                    PassCombo.Items.Clear();
                    TimeCombo.Items.Clear();
                    NumberCombo.Items.Clear();
                    LengthCombo.Items.Clear();
                    setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
                }
                else { MessageBox.Show("You have to write forum name in order to edit it's preferences"); }
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            SuperUserWindow newWin = new SuperUserWindow(_myUser.userName, _myUser.password, _myUser.email);
            newWin.Show();
            this.Close();
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

        private void descChoose(object sender, RoutedEventArgs e)
        {
            bool toChange = descCheck.IsChecked.Value;
            if (toChange) { ForumDescToSet.IsEnabled = true; }
            else { ForumDescToSet.IsEnabled = false; }
        }

        private void policyChoose(object sender, RoutedEventArgs e)
        {
            bool toChange = policyCheck.IsChecked.Value;
            if (toChange) { ForumPolicyToSet.IsEnabled = true; }
            else { ForumPolicyToSet.IsEnabled = false; }
        }

        private void PassComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            int minDays = 30;
            int maxDays = 365;
            for (int i = minDays; i <= maxDays; i++)
            {
                PassCombo.Items.Add(i);
            }
        }

        private void PassComboBox_OnDropDownClosed(object sender, EventArgs e)
        {

        }

        private void TimeComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            int minDays = 0;
            int maxDays = 365;
            for (int i = minDays; i <= maxDays; i++)
            {
                TimeCombo.Items.Add(i);
            }
        }

        private void TimeComboBox_OnDropDownClosed(object sender, EventArgs e)
        {

        }

        private void NumberComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            int minNumOfModerators = 1;
            int maxNumOfModerators = 10;
            for (int i = minNumOfModerators; i <= maxNumOfModerators; i++)
            {
                NumberCombo.Items.Add(i);
            }
        }

        private void NumberComboBox_OnDropDownClosed(object sender, EventArgs e)
        {

        }

        private void LengthComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            int minPasswordLength = 5;
            int maxPasswordLength = 20;
            for (int i = minPasswordLength; i <= maxPasswordLength; i++)
            {
                LengthCombo.Items.Add(i);
            }
        }

        private void LengthComboBox_OnDropDownClosed(object sender, EventArgs e)
        {

        }

    }
}
        