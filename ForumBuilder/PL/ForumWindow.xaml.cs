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
using PL.proxies;

namespace PL
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window
    {
        
        private ForumData _myforum;
        //private List<String> _subForumNames;
        private String _subForumChosen;
        private ForumManagerClient _fMC;
        private string _userName;

        public ForumWindow(ForumData forum)
        {
            InitializeComponent();
            //_subForumNames = new List<string>();
            _myforum = forum;
            _fMC = new ForumManagerClient();
        }

        public ForumWindow(ForumData forum, string userName)
        {
            InitializeComponent();
            //_subForumNames = new List<string>();
            _myforum = forum;
            _fMC = new ForumManagerClient();
            _userName = userName;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: need to know Forum
            // ... Create a List of objects.
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

            // ... Assign ItemsSource of DataGrid.
            var grid = sender as DataGrid;
            //grid.ItemsSource = items;
        }

        private void DataGrid_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            // ... Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            _subForumChosen = selected.ToString();

            //TODO: Probably irrelevant (!!!)
            // ... Add all Names to a List.
            List<string> names = new List<string>();
            foreach (var item in selected)
            {
                //TODO: need to know Forum
                //var forum = item as Forum;
                //names.Add(forum.forumName);
                var forum = item as string;
                names.Add(forum);
            }
            // ... Set Title to selected names.
            this.Title = string.Join(", ", names);
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
          //TODO: addForum.Visibility = System.Windows.Visibility.Visible; ;
        }

        private void setNotifications(object sender, RoutedEventArgs e)
        {
            //TODO: need to do this func?
        }

        private void setPreferences()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            mainGrid.Visibility = System.Windows.Visibility.Collapsed;
            MyDialog.Visibility = System.Windows.Visibility.Collapsed;
            //TODO: addForum.Visibility = System.Windows.Visibility.Collapsed;
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
                MessageBox.Show("Preferences was successfully changed! ");
                setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
            }            
        }

        private void privateMessages_Click(object sender, RoutedEventArgs e)
        {
            privateMessagesWindow newWin = new privateMessagesWindow(_userName, this);
            this.Visibility = Visibility.Collapsed;
            newWin.Show();
        }
    }
}
