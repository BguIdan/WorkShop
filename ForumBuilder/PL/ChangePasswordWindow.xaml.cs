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
using PL.proxies;

namespace PL
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        private UserManagerClient _uMC;
        private String _forum;
        private String _userName;

        public ChangePasswordWindow(String forum, String usrName)
        {
            InitializeComponent();
            _forum = forum;
            _userName = usrName;
        }

        private void backToMain(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void changeBtn(object sender, RoutedEventArgs e)
        {
            string pass = NewPassword.Password;
            string ans = _uMC.setNewPassword(_userName, _forum, pass);
            if (!ans.Equals("change password succeed"))
            {
                MessageBox.Show(ans + " Please try again!");
                NewPassword.Clear();
            }
            else
            {
                MessageBox.Show(ans);
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }
    }
}
