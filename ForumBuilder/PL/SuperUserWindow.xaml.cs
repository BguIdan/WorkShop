using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private string _currentForum;
        private List<string> _forumsList;

        public SuperUserWindow(String userName, String password, String email)
        {
            InitializeComponent();
            _sUMC = new SuperUserManagerClient();
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _myUser = new UserData(userName, password, email);
            _currentForum = "";
            _forumsList = _fMC.getForums();
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
            beforeSetPref.Visibility = System.Windows.Visibility.Visible;
            beforeSetPref.Focusable = false;
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
            _currentForum = newForumName.Text;
            string desc = newForumDescription.Text;
            string administrators = newAdminUserName.Text;
            List<string> admins = administrators.Split(',').ToList();
            string created = _sUMC.createForum(_currentForum, desc, new ForumPolicyData(), admins, _myUser.userName);
            Boolean isCreated = created.Equals("Forum " + _currentForum + " creation success");
            if (isCreated)
            {
                MessageBox.Show("Forum " + _currentForum + " creation success");
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
                ForumData fd = _fMC.getForum(_currentForum);
                qIdentifying.IsChecked = fd.forumPolicy.isQuestionIdentifying;
                deleteMessages.IsChecked = fd.forumPolicy.deletePostByModerator;
                Capital.IsChecked = fd.forumPolicy.hasCapitalInPassword;
                Number.IsChecked = fd.forumPolicy.hasNumberInPassword;
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
            if (!_currentForum.Equals("")) { _fData = _fMC.getForum(_currentForum); }
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
                    toChange = (qIdentifying.IsChecked.Value != _fData.forumPolicy.isQuestionIdentifying);
                    if (toChange)
                    {
                        _fData.forumPolicy.isQuestionIdentifying = qIdentifying.IsChecked.Value;
                    }
                    toChange = (deleteMessages.IsChecked.Value != _fData.forumPolicy.deletePostByModerator);
                    if (toChange)
                    {
                        _fData.forumPolicy.isQuestionIdentifying = deleteMessages.IsChecked.Value;
                    }
                    if (!PassCombo.Text.Equals(""))
                    {
                        int passExpirationTime = Int32.Parse(PassCombo.SelectedItem.ToString());
                        _fData.forumPolicy.timeToPassExpiration = passExpirationTime;
                    }
                    if (!TimeCombo.Text.Equals(""))
                    {
                        int seniorityChoosen = Int32.Parse(TimeCombo.SelectedItem.ToString());
                        _fData.forumPolicy.seniorityInForum = seniorityChoosen;
                    }
                    if (!NumberCombo.Text.Equals(""))
                    {
                        int numerOfModerators = Int32.Parse(NumberCombo.SelectedItem.ToString());
                        _fData.forumPolicy.minNumOfModerator = numerOfModerators;
                    }
                    toChange = (Capital.IsChecked.Value != _fData.forumPolicy.hasCapitalInPassword);
                    if (toChange)
                    {
                        _fData.forumPolicy.hasCapitalInPassword = Capital.IsChecked.Value;
                    }
                    toChange = (Number.IsChecked.Value != _fData.forumPolicy.hasNumberInPassword);
                    if (toChange)
                    {
                        _fData.forumPolicy.hasNumberInPassword = Number.IsChecked.Value;
                    }
                    if (!LengthCombo.Text.Equals(""))
                    {
                        int minLengthOfPass = Int32.Parse(LengthCombo.SelectedItem.ToString());
                        _fData.forumPolicy.minLengthOfPassword = minLengthOfPass;
                    }
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
            string addUser = _sUMC.addUser(name, pass, userMail, _myUser.userName);
            bool succ = addUser.Equals("Register user " + name + "Complited");
            if (!succ)
            {
                MessageBox.Show(addUser);
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
            PassCombo.Items.Clear();
            int minDays = 30;
            int maxDays = 365;
            for (int i = minDays; i <= maxDays; i++)
            {
                PassCombo.Items.Add(i);
            }
        }

        private void TimeComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            TimeCombo.Items.Clear();
            int minDays = 0;
            int maxDays = 365;
            for (int i = minDays; i <= maxDays; i++)
            {
                TimeCombo.Items.Add(i);
            }
        }

        private void NumberComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            NumberCombo.Items.Clear();
            int minNumOfModerators = 1;
            int maxNumOfModerators = 10;
            for (int i = minNumOfModerators; i <= maxNumOfModerators; i++)
            {
                NumberCombo.Items.Add(i);
            }
        }

        private void LengthComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            LengthCombo.Items.Clear();
            int minPasswordLength = 5;
            int maxPasswordLength = 20;
            for (int i = minPasswordLength; i <= maxPasswordLength; i++)
            {
                LengthCombo.Items.Add(i);
            }
        }

        private void btn_back(object sender, RoutedEventArgs e)
        {
            beforeSetPref.Visibility = System.Windows.Visibility.Collapsed;
            beforeSetPref.Focusable = false;
        }

        private void btn_continue(object sender, RoutedEventArgs e)
        {
            ForumData fd = _fMC.getForum(_currentForum);
            if (fd != null)
            {
                beforeSetPref.Visibility = System.Windows.Visibility.Collapsed;
                beforeSetPref.Focusable = false;
                setPreferencesWin.Visibility = System.Windows.Visibility.Visible;
                qIdentifying.IsChecked = fd.forumPolicy.isQuestionIdentifying;
                deleteMessages.IsChecked = fd.forumPolicy.deletePostByModerator;
                Capital.IsChecked = fd.forumPolicy.hasCapitalInPassword;
                Number.IsChecked = fd.forumPolicy.hasNumberInPassword;
            }
            else { MessageBox.Show("Please choose a forum in order to edit it"); }
        }

        private void ComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            comboBox.Items.Clear();
            while (_forumsList == null) { Thread.Sleep(20); }
            foreach (String forumName in this._forumsList)
                comboBox.Items.Add(forumName);
        }

        private void ComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            try
            {
                _currentForum = comboBox.SelectedItem.ToString();
            }
            catch (Exception)
            {
                MessageBox.Show("Please choose a forum from the list");
            }
        }
    }
}
        