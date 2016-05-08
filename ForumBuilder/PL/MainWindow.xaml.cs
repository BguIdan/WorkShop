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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.proxies;
using ForumBuilder.Common.DataContracts;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> _forumsList;
        private String _choosenForum;
        private ForumManagerClient _fMC;

        public MainWindow()
        {
            InitializeComponent();
            _fMC = new ForumManagerClient();
            _forumsList = _fMC.getForums();
            this.Show(); 
        }

        public void updateForums(ForumData newForum)
        {
            _forumsList.Add(newForum.forumName);
            /* if binding doesn't work
            for (int i = 0; i < _forumsList.Count; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = _forumsList.ElementAt(i).forumName;
                comboBox.Items.Add(newItem);
            }*/
        }

        private void LoginPressed(object sender, RoutedEventArgs e)
        {
            string userName = ID.Text;
            string pass = Password.Password;
            /*if (userName.Equals("") || pass.Equals(""))
            {
                MessageBox.Show("Invalid Input");
                return;
            }*/
            if (_choosenForum != null)
            {
                ForumData toSend = _fMC.getForum(_choosenForum);
                ForumWindow fw = new ForumWindow(toSend,userName);
                this.Close();
                fw.Show();
            }
            else
            {
                MessageBox.Show("You have to choose forum from the list");
            }
        }

        // TODO:In the next 2 functions i need to decide how to handle different types of users and to decide rather to know and hold userData class

        private void ForgorPasswordPressed(object sender, RoutedEventArgs e)
        {
            // TODO: know the user class
            // RestorePasswordWindow rpw = new RestorePasswordWindow(itsUserBL);
            RestorePasswordWindow rpw = new RestorePasswordWindow();
            this.Close();
            rpw.ShowDialog();
        }

        private void SignUpUser(object sender, RoutedEventArgs e)
        {
            // TODO: know the admin and user class and create new one in the data base
            // SignUpWindow suw = new SignUpWindow(itsUserBL, iDAL, itsAdminBL);
            SignUpWindow suw = new SignUpWindow(_fMC,_choosenForum);
            suw.ShowDialog();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _choosenForum = comboBox.SelectedItem.ToString();
        }

        private void guestChoose(object sender, RoutedEventArgs e)
        {
            bool toChange = guestCheck.IsChecked.Value;
            if (toChange) 
            { 
                ID.IsEnabled = false;
                Password.IsEnabled = false;
                Forgot.IsEnabled = false;
                signUP.IsEnabled = false;
            }
            else 
            {
                ID.IsEnabled = true;
                Password.IsEnabled = true;
                Forgot.IsEnabled = true;
                signUP.IsEnabled = true;
            }
        }

    }
}
