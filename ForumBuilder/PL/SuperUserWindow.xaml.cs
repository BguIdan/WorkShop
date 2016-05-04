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

namespace PL
{
    /// <summary>
    /// Interaction logic for SuperUserWindow.xaml
    /// </summary>
    public partial class SuperUserWindow : Window
    {
        public SuperUserWindow()
        {
            InitializeComponent();
        }
    }
}
        public AdminWindow(DAL.IDAL dalimp, int userID, UserBL isUserBL)
        {
            InitializeComponent();
            this.userID = userID;
            itsUserBL = isUserBL;
            TextBlock.Visibility = System.Windows.Visibility.Visible;
            adminBL = new AdminBL(dalimp);
            /* collaspe any other window: */
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            this.Show();

        }

        private void MenuItem_Actions(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "CreateForum":    { AddCouponFromSocial(); } break;
                case "Set":          { AddRegularCoupon(); }    break;
                case "CreateSub":      { ConfirmCoupon(); } break;
                case "Delete":         { EditCoupon(); } break;
                case "Exit": { this.Visibility = System.Windows.Visibility.Collapsed; System.Environment.Exit(1); } break;
            }
        }

        private void ViewCoupon()
        {
            /* collaspe any other window: */
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            /* show only this window: */
            ViewCouponWindow.Visibility = System.Windows.Visibility.Visible;
            couponsView.ItemsSource = adminBL.showAllCoupons();
        }