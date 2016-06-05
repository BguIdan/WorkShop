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
using System.ServiceModel;
using PL.notificationHost;
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
            Thread.Sleep(1000);
			_fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _forumsList = _fMC.getForums();
            this.Show();
        }

        private void LoginPressed(object sender, RoutedEventArgs e)
        {
            string userName = ID.Text;
            string pass = Password.Password;
            if (_choosenForum != null)
            {
                ForumData toSend = _fMC.getForum(_choosenForum);
                if (guestCheck.IsChecked.Value)
                {
                    ForumWindow fw = new ForumWindow(toSend, "Guest");
                    this.Close();
                    fw.Show();
                }
                else if (_fMC.login(userName, _choosenForum, pass))
                {
                    ForumWindow fw = new ForumWindow(toSend, userName);
                    this.Close();
                    fw.Show();
                }
                else
                {
                    MessageBox.Show("Oops... can't login!");
                }
            }
            else
            {
                MessageBox.Show("You have to choose forum from the list");
            }
        }
        
        private void SignUpUser(object sender, RoutedEventArgs e)
        {
            SignUpWindow suw = null;
            try
            {
                suw = new SignUpWindow(_fMC,_choosenForum);
            }
            catch
            {
                MessageBox.Show("please choose a forum to register", "error");
            }
            try
            {
                suw.Show();
                this.Close();
            }
            catch(NullReferenceException)
            {
                //if the forum was not successfully instantiated it will throw null reference exception
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _choosenForum = comboBox.SelectedItem.ToString();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.ToString());
            }
        }

        private void guestChoose(object sender, RoutedEventArgs e)
        {
            bool toChange = guestCheck.IsChecked.Value;
            if (toChange) 
            { 
                ID.IsEnabled = false;
                Password.IsEnabled = false;
                superUserViewButton.IsEnabled = false;
                signUP.IsEnabled = false;
            }
            else 
            {
                ID.IsEnabled = true;
                Password.IsEnabled = true;
                superUserViewButton.IsEnabled = true;
                signUP.IsEnabled = true;
            }
        }
        private void superUserViewButton_Click(object sender, RoutedEventArgs e)
        {
            SuperUserLogInWindow newWin = new SuperUserLogInWindow();
            newWin.Show();
            this.Close();
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
                _forumsList = _fMC.getForums();
                if (!_fMC.getForum(comboBox.SelectedItem.ToString()).forumPolicy.isQuestionIdentifying)
                {
                    restore.IsEnabled = false;
                }
            }
            catch (Exception)
            {
                
                MessageBox.Show("Please choose a forum from the list");
            }
        }

        private void restorePass(object sender, RoutedEventArgs e)
        {
            if (_choosenForum != null)
            {
                RestorePasswordWindow rpw = new RestorePasswordWindow(_fMC, _choosenForum);
                this.Close();
                rpw.Show();
            }
            else
            {
                MessageBox.Show("You have to choose forum from the list");
            }
        }

    }
}
