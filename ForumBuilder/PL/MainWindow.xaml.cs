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
        private List<ForumData> _forumsList;
        private String _choosenForum;
        private ForumManagerClient _fMC;

        public MainWindow()
        {
            InitializeComponent();
            _forumsList = new List<ForumData>();
            _fMC = new ForumManagerClient();
            /*//TODO client server communication POC delete later
             * ForumManagerClient fmc = new ForumManagerClient();
            fmc.addSubForum("a", "b", null, "c");
            PostManagerClient pmc = new PostManagerClient();
            pmc.addPost("", "", "", 0);
            SubForumManagerClient sfmc = new SubForumManagerClient();
            sfmc.createThread("", "", "", "", "");
            SuperUserManagerClient sumc = new SuperUserManagerClient();
            sumc.createForum("", "", "", "", null, "");
            UserManagerClient umc = new UserManagerClient();
            umc.addFriend("", "");*/
            this.Show(); 
        }

        public void updateForums(ForumData newForum)
        {
            _forumsList.Add(newForum);

            for (int i = 0; i < _forumsList.Count; i++)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = _forumsList.ElementAt(i).forumName;
                comboBox.Items.Add(newItem);
            }
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
                ForumData toSend = _fMC.getForum(_choosenForum);
                ForumWindow fw = new ForumWindow(toSend);
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
