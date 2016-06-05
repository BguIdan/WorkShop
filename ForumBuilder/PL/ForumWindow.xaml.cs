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
using ForumBuilder.Common.DataContracts;
using System.ServiceModel;
using PL.notificationHost;
using PL.proxies;

namespace PL
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    /// 

    public partial class ForumWindow : Window
    {
        
        private ForumData _myforum;
        private String _subForumChosen;
        private ForumManagerClient _fMC;
        private string _userName;
        private SuperUserManagerClient _sUMC;
              
        public ForumWindow(ForumData forum, string userName)
        {
            InitializeComponent();
            _myforum = forum;
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _userName = userName;
            ForumName.Content = "ForumName:  " + _myforum.forumName;
            _sUMC = new SuperUserManagerClient();
            InitializePermissons(userName);
            //initializing the subForumListBox
            foreach(string subForum in _myforum.subForums)
            {
                subForumsListBox.Items.Add(subForum);
            }
        }

        private void InitializePermissons(string userName)
        {
            // a guest
            if (userName.Equals("Guest"))
            {
                AddSub.IsEnabled = false;
                Set.IsEnabled = false;
            }
            // a member but not an admin
            else if (!_fMC.isAdmin(userName, _myforum.forumName) && !_sUMC.isSuperUser(userName))
            {
                AddSub.IsEnabled = false;
                Set.IsEnabled = false;
                Sign.IsEnabled = false;
            }
            // an admin
            else if (!_sUMC.isSuperUser(userName))
            {
                Sign.IsEnabled = false;
            }
            //  a super user
            else
            {
               // all open 
            }
        }
                
        private void MenuItem_Forums(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "AddSub": { addNewSubForum(); } break;
                case "Set": { setPreferences(); } break;
                case "SignUP": { SignUP(); } break;
                case "menuLogout": { logout(_userName); } break;
                case "privateMessages": { privateMessages_Click(sender, e); } break;
            }
        }



        private void logout(String nameLogout)
        {
            MainWindow mw = new MainWindow();
            // a guest
            if (nameLogout.Equals("Guest"))
            {
                mw.Show();
                this.Close();
            }
            // an fourom member
            else
            {
                _fMC.logout(nameLogout, _myforum.forumName);
                mw.Show();
                this.Close();
            }
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            _subForumChosen = selected.ToString();
            SubForumWindow sfw = new SubForumWindow(_myforum.forumName, _subForumChosen, _userName);
            sfw.ShowDialog();
        }
        
        private void addNewSubForum()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            mainGrid.Visibility = System.Windows.Visibility.Collapsed;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            for (int i = 0; i < _myforum.members.Count; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = _myforum.members.ElementAt(i);
                comboBox.Items.Add(newItem);
            }
            ComboBoxItem newFirstItem = new ComboBoxItem();
            newFirstItem.Content = "UnLimited";
            comboBoxDuration.Items.Add(newFirstItem);
            for (int i = 1; i < 31; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = i;
                comboBoxDuration.Items.Add(newItem);
            }
            AddSubForum.Visibility = System.Windows.Visibility.Visible;
            backButton.Visibility = System.Windows.Visibility.Visible;

        }

        private void setPreferences()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            mainGrid.Visibility = System.Windows.Visibility.Collapsed;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;
            AddSubForum.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Visible;
            backButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void SignUP()
        {
            SignUpWindow sU = new SignUpWindow(_fMC,_myforum.forumName);
            sU.Show();
            this.Close();
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

        private void btn_SetForumPref(object sender, RoutedEventArgs e)
        {
            MyDialog.Visibility = System.Windows.Visibility.Visible;
            MyDialog.Focusable = true;
        }

        private void btn_toSetPref(object sender, RoutedEventArgs e)
        {
            String temp = "";
            var btn = sender as Button;
            if (btn.Name.Equals("yesBtn"))
            {
                temp = yesBtn.Content.ToString();
            }
            else { temp = noBtn.Content.ToString(); }
            setPref(temp);
        }

        private void setPref(String isDone)
        {
            MyDialog.Focusable = false;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;

            if (isDone.Equals("Yes"))
            {
                bool toChange = descCheck.IsChecked.Value;
                if (toChange)
                {
                    _myforum.description = ForumDescToSet.Text;
                }
                toChange = policyCheck.IsChecked.Value;
                if (toChange)
                {
                    _myforum.forumPolicy.policy = ForumPolicyToSet.Text;
                }
                toChange = qIdentifying.IsChecked.Value;
                if (toChange)
                {
                    _myforum.forumPolicy.isQuestionIdentifying = true;
                }
                toChange = deleteMessages.IsChecked.Value;
                if (toChange)
                {
                    _myforum.forumPolicy.isQuestionIdentifying = true;
                }
                int passExpirationTime = Int32.Parse(PassCombo.SelectedItem.ToString());
                _myforum.forumPolicy.timeToPassExpiration = passExpirationTime;
                int seniorityChoosen = Int32.Parse(TimeCombo.SelectedItem.ToString());
                _myforum.forumPolicy.seniorityInForum = seniorityChoosen;
                int numerOfModerators = Int32.Parse(NumberCombo.SelectedItem.ToString());
                _myforum.forumPolicy.minNumOfModerator = numerOfModerators;
                toChange = Capital.IsChecked.Value;
                if (toChange)
                {
                    _myforum.forumPolicy.hasCapitalInPassword = true;
                }
                toChange = Number.IsChecked.Value;
                if (toChange)
                {
                    _myforum.forumPolicy.hasNumberInPassword = true;
                }
                int minLengthOfPass = Int32.Parse(LengthCombo.SelectedItem.ToString());
                _myforum.forumPolicy.minLengthOfPassword = minLengthOfPass;
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
                MainMenu.Visibility = System.Windows.Visibility.Visible;
            }            
        }


        private void btn_createSub(object sender, RoutedEventArgs e)
        {
            int time = 0;
            int unlimited = 120;
            DateTime timeToSend = DateTime.Now;
            String sub_ForumName = subForumName.Text;
            // TODO: add to option for more than one moderator
            String userName = comboBox.Text;
            String timeDuration = comboBoxDuration.Text;
            if (timeDuration == null || userName == null || sub_ForumName == null || sub_ForumName.Equals(""))
            {
                MessageBox.Show("error has accured");
                return;
            }
            if (!timeDuration.Equals("UnLimited"))
            {
                time = int.Parse(timeDuration);
                timeToSend = DateTime.Now.AddDays(time);
            }
            else
            {
                timeToSend = DateTime.Now.AddYears(unlimited);
            }
            Dictionary<String, DateTime> dic = new Dictionary<string, DateTime>();
            dic.Add(userName, timeToSend);
            Boolean isAdded = _fMC.addSubForum(_myforum.forumName, sub_ForumName, dic, _userName);
            if (isAdded == false)
            {
                MessageBox.Show(userName + " can not be a moderator, try someone else.");
            }
            else
            {
                MessageBox.Show("Sub-Forum " + sub_ForumName + " was successfully created and " + userName + " is the Sub-Forum moderator.");
                ForumWindow newWin = new ForumWindow(_fMC.getForum(_myforum.forumName), _userName);
                this.Close();
                newWin.Show();

                //MainMenu.Visibility = System.Windows.Visibility.Visible;
                //mainGrid.Visibility = System.Windows.Visibility.Visible;
                //AddSubForum.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void subForumsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get SelectedItems from DataGrid.

            _subForumChosen = subForumsListBox.SelectedItem.ToString();
            SubForumWindow sfw = new SubForumWindow(_myforum.forumName, _subForumChosen, _userName);
            sfw.Show();
            this.Close();
        }


        private void privateMessages_Click(object sender, RoutedEventArgs e)

        {
            privateMessagesWindow newWin = new privateMessagesWindow(_myforum.forumName, _userName, this);
            this.Visibility = Visibility.Collapsed;
            newWin.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            ForumWindow newWin = new ForumWindow(_myforum, _userName);
            newWin.Show();
            this.Close();
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
            int minNumOfModerators = 0;
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

        private void addFriend_Click(object sender, RoutedEventArgs e)
        {
            AddFriendWindow newWin = new AddFriendWindow(_userName, _myforum);
            newWin.Show();
            this.Close();
        }
    }
}
