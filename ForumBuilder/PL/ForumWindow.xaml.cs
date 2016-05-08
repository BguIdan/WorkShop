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
    public partial class ForumWindow : Window
    {
        
        private ForumData _myforum;
        private String _subForumChosen;
        private ForumManagerClient _fMC;
        private string _userName;

        /*public ForumWindow(ForumData forum)
        {
            InitializeComponent();
            _myforum = forum;
            _fMC = new ForumManagerClient();
        }*/

        // TODO: ASK Tomer about this constructor !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public ForumWindow(ForumData forum, string userName)
        {
            InitializeComponent();
            _myforum = forum;
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            //_fMC.login(userName, forum.forumName);
            _userName = userName;
            ForumName.Content = "ForumName: " + _myforum.forumName;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // ... Create a List of objects. (Example)
            /*var items = new List<Forum>();
            items.Add(new Forum("Fido", "10" , " ", " " , new List<string>()));
            items.Add(new Forum("Spark", "20" , " ", " " , new List<string>()));
            items.Add(new Forum("Fluffy", "4" , " ", " " , new List<string>()));*/
           
            /* Option B:
              var items = new List<String>();
              for(int i=0; i < _subForumNames.Count;i++)
              {
                  items.Add(_subForumNames.ElementAt(i));
              }*/

            // ... Assign ItemsSource of DataGrid. (Should do the job)
            var grid = sender as DataGrid;
            grid.ItemsSource = _myforum.subForums;
        }

        private void DataGrid_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            // ... Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            _subForumChosen = selected.ToString();
            SubForumWindow sfw = new SubForumWindow(_myforum.forumName, _subForumChosen, _userName);
            sfw.ShowDialog();
        }

        private void MenuItem_Forums(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "AddSub": { addNewSubForum(); } break;
                case "Set": { setPreferences(); } break;
                case "SignUP": { SignUP(); } break;
                case "Exit": { this.Visibility = System.Windows.Visibility.Collapsed; System.Environment.Exit(1); } break;
            }
        }

        private void addNewSubForum()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            mainGrid.Visibility = System.Windows.Visibility.Collapsed;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            /* if binding doesn't work
            for (int i = 0; i < _myforum.members.Count; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = _myforum.members.ElementAt(i);
                comboBox.Items.Add(newItem);
            }*/
            ComboBoxItem newFirstItem = new ComboBoxItem();
            newFirstItem.Content = "UnLimited";
            comboBoxDuration.Items.Add(newFirstItem);
            for (int i = 1; i < 31; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = i;
                comboBoxDuration.Items.Add(newItem);
            }
            AddSubForum.Visibility = System.Windows.Visibility.Visible; ;
        }

        /*private void setNotifications(object sender, RoutedEventArgs e)
        {
            //TODO: need to do this func?
        }*/

        private void setPreferences()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            mainGrid.Visibility = System.Windows.Visibility.Collapsed;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;
            AddSubForum.Visibility = System.Windows.Visibility.Collapsed;
            setPreferencesWin.Visibility = System.Windows.Visibility.Visible;
        }

        private void SignUP()
        {
            SignUpWindow sU = new SignUpWindow(_fMC,_myforum.forumName);
            sU.ShowDialog();
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

        private void rulesChoose(object sender, RoutedEventArgs e)
        {
            bool toChange = rulesCheck.IsChecked.Value;
            if (toChange) { ForumRulesToSet.IsEnabled = true; }
            else { ForumRulesToSet.IsEnabled = false; }
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
                string temp = "";
                bool toChange = descCheck.IsChecked.Value;
                if (toChange)
                {
                    temp = ForumDescToSet.Text;
                    _myforum.description = temp;
                }
                toChange = policyCheck.IsChecked.Value;
                if (toChange)
                {
                    temp = ForumPolicyToSet.Text;
                    _myforum.forumPolicy = temp;
                }
                toChange = rulesCheck.IsChecked.Value;
                if (toChange)
                {
                    temp = ForumRulesToSet.Text;
                    _myforum.forumRules = temp;
                }
                MessageBox.Show("Preferences was successfully changed!");
                setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            }            
        }


        private void btn_createSub(object sender, RoutedEventArgs e)
        {
            int time = 0;
            //TODO: check what to do with unlimited time
            DateTime timeToSend = DateTime.Now;
            String sub_ForumName = subForumName.Text;
            // TODO: add to option for more than one moderator
            String userName = comboBox.SelectedItem.ToString();
            String timeDuration = comboBoxDuration.SelectedItem.ToString();
            if (!timeDuration.Equals("UnLimited"))
            {
                time = Convert.ToInt32(timeDuration);
                timeToSend = DateTime.Now.AddDays(time);
            }
            Dictionary<String, DateTime> dic = new Dictionary<string, DateTime>();
            dic.Add(userName, timeToSend);
            //TODO: check what to do with type of users(admin, member etc.)
            Boolean isAdded = _fMC.addSubForum(_myforum.forumName, sub_ForumName, dic, "");
            if (isAdded == false)
            {
                MessageBox.Show(userName + "can not be a moderator, try someone else.");
            }
            else
            {
                MessageBox.Show("Sub-Forum " + sub_ForumName + " was successfully created and " + userName + " is the Sub-Forum moderator.");
            }
        }

        /*private void privateMessages_Click(object sender, RoutedEventArgs e)

        {
            privateMessagesWindow newWin = new privateMessagesWindow(_userName, this);
            this.Visibility = Visibility.Collapsed;
            newWin.Show();


        }

        }*/

    }
}
