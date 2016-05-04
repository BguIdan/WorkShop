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
    /// Interaction logic for SuperUserWindow.xaml
    /// </summary>
    public partial class SuperUserWindow : Window
    {
        private SuperUserManagerClient _sUMC;

        public SuperUserWindow()
        {
            InitializeComponent();
            _sUMC = new SuperUserManagerClient();
            this.Show();
        }

        private void MenuItem_Actions(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "CreateForum": { createNewForum(); } break;
                case "Set": { setPreferences(); } break;
                case "CreateSub": { createSub(); } break;
                case "Del": { Delete(); } break;
                case "Exit": { this.Visibility = System.Windows.Visibility.Collapsed; System.Environment.Exit(1); } break;
            }
        }

        private void createNewForum()
        {
            MainMenu.Visibility = System.Windows.Visibility.Collapsed;
            createForum.Visibility = System.Windows.Visibility.Visible;
            string forumName = newForumName.Text;
            string desc = newForumDescription.Text;
            string rules = newForumRules.Text;
            string policy = newForumPolicy.Text;
            string administrators = newAdminUserName.Text;
            List<string> admins = new List<string>();
            //_sUMC.createForum(forumName,desc,policy,rules,admin
        }

        private void setPreferences()
        {
            
        }

        private void createSub()
        {
           
        }

        private void Delete()
        {
           
        }

        private void btn_addCouponFromSocial(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_View(object sender, RoutedEventArgs e)
        {

        }

    }
}
        