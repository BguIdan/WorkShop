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
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private UserBL itsUserBL;
        private SQL_DAL_implementation iDAL;
        private AdminBL itsAdminBL;

        public SignUpWindow(UserBL itsUser, SQL_DAL_implementation itsDal,   AdminBL itsadminBL)
        {
            InitializeComponent();
            itsAdminBL = itsadminBL;
            itsUserBL = itsUser;
            iDAL = itsDal; 
        }

        private void UserRegistration(object sender, RoutedEventArgs e)
        {
            int userID = 0;
            string name = null;
            string pass = null;
            string pref = null;
            string mail = null;
            try
            {
                userID = Convert.ToInt32(ID.Text);
                name = nAame.Text;
                pass = Password.Password;
                pref = Pref.Text;
                mail = Mail.Text;
            }
            catch
            {
                MessageBox.Show("ID must be digits");
            }
            if (mail==null || pref == null || userID == 0 || pass == null  || name == null)
            {
                MessageBox.Show("Please fill all the required details, make sure password include at least 5 letters");
            }
            bool suc = itsUserBL.createNewUser(userID, name, pass,mail);
            if (suc == false)
            {
                MessageBox.Show("Fail");
                return;
            }
            itsUserBL.setPreferences(userID, pref);
            MessageBox.Show("Welcome " +name+ "!");
            UserWindow uw = new UserWindow(iDAL, userID,itsAdminBL);
            this.Close();
            uw.Show();

        } 
    }
}
