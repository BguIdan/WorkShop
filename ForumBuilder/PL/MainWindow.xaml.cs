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
using Service;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // TODO: know Forum!!! or Forum name
        //private List<Forum> _forumsList;
        private List<string> _forumsList;
        private String _choosenForum;

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < _forumsList.Count; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                // TODO: insert all forums names to the combo box list
                //newItem.Content = _forumsList.ElementAt(i).Name;
                comboBox.Items.Add(newItem);
            }
            this.Show(); 
        }

        private void LoginPressed(object sender, RoutedEventArgs e)
        {
            /* TODO: validate input is it enough? */

            string userName = ID.Text;
            string pass = Password.Password;
            if (userName.Equals("") || pass.Equals(""))
            {
                MessageBox.Show("Invalid Input");
                return;
            }
            if (_choosenForum != null)
            {
                ForumWindow fw = new ForumWindow(_choosenForum);
                this.Close();
                fw.ShowDialog();
            }
            else
            {
                MessageBox.Show("wrong user name or password");
                return;
            }
            
        }

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
            SignUpWindow suw = new SignUpWindow();
            this.Close();
            suw.ShowDialog();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _choosenForum = comboBox.SelectedItem.ToString();
        }

    }
}
