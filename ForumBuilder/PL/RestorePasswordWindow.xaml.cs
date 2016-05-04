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
using BL;
using DAL;

namespace PL
{
    /// <summary>
    /// Interaction logic for RestorePasswordWindow.xaml
    /// </summary>
    public partial class RestorePasswordWindow : Window
    {
        //private UserBL itsUserBL;
        public RestorePasswordWindow()
        {
            InitializeComponent();
            //itsUserBL = itsUser;
        }

        private void SendDetails(object sender, RoutedEventArgs e)
        {
            int userID = 0;
            string email = null;

            try
            {
                userID = Convert.ToInt32(ID.Text);
                //email = itsUserBL.getMail(userID);
            }
            catch
            {
                MessageBox.Show("ID must be digits");
            }
            if (email == null || userID == 0 )
            {
                MessageBox.Show("Please fill all the required details");
            }

            //itsUserBL.restorePassword(userID, email);

            MessageBox.Show("Mail sended");

            MainWindow aw = new MainWindow();
            this.Close();
            aw.Show();

        }
    }
}
