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
      
        public MainWindow()
        {
            this.Show(); 
        }

        private void LoginPressed(object sender, RoutedEventArgs e)
        {
            /* validate input */

            string userName = ID.Text;
            string pass = Password.Password;
            if (userName.Equals("") || pass.Equals(""))
            {
                MessageBox.Show("Invalid Input");
                return;
            }
            if (/*login()*/true)
            {

            }
            else
            {
                MessageBox.Show("wrong user name or password");
                return;
            }
            
        }

        private void ForgorPasswordPressed(object sender, RoutedEventArgs e)
        {
            RestorePasswordWindow rpw = new RestorePasswordWindow(itsUserBL);
            this.Close();
            rpw.ShowDialog();
        }

        private void SignUpUser(object sender, RoutedEventArgs e)
        {
            SignUpWindow suw = new SignUpWindow(itsUserBL, iDAL, itsAdminBL);
            this.Close();
            suw.ShowDialog();
        }

    }
}
