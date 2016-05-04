using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PL
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        /*private UserBL uBL;
        private int ID;
        private AdminBL itsAdminBL;
        string message;
        bool location = true;
        bool prefer = false;
        int numOFNotifications = 0;
        int index = 0;
        int numOfCoupToNotify = 0;
        private DAL.SQL_DAL_implementation itDAL;


        public UserWindow(DAL.SQL_DAL_implementation itsDAL,int id, AdminBL isAdminBL)
        {
            InitializeComponent();
            ID = id;
            itDAL = itsDAL;
            TextBlock1.Visibility = System.Windows.Visibility.Visible;
            uBL = new UserBL(itsDAL);
            itsAdminBL = isAdminBL;
            
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            this.Show();
            
        }

        public UserWindow()
        {
            // TODO: Complete member initialization
        }

        private void MenuItem_Coupon(object sender, RoutedEventArgs e)
        {
            notifictions();
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "Add": { AddRegularCoupon(); } break;
                case "Look": { LookForAcoupon(); } break;
                case "Rank": { RankACoupon(); } break;
                case "Exit": { this.Visibility = System.Windows.Visibility.Collapsed; System.Environment.Exit(1); break; } 
            }
        }

        private void setNotifications(object sender, RoutedEventArgs e)
        {
            
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            Notifier.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock1.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            
            Notifier.Visibility = System.Windows.Visibility.Visible;
            if (loca.IsChecked==true )
            {
                location = true;
            }
            if (cata.IsChecked==true)
            {
                prefer = true;
            }
            
        }


        private void RankACoupon()
        {
            notifictions();
            updateNotify();
            
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            Notifier.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock1.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            
            RankCouponWindow.Visibility = System.Windows.Visibility.Visible;
            couponsView.ItemsSource = uBL.viewOrders(ID);
        }

        private void AddRegularCoupon()
        {
            
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            Notifier.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock1.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
          
            AddCoupon.Visibility = System.Windows.Visibility.Visible;
        }

        private void LookForAcoupon()
        {
            notifictions();
            updateNotify();
            
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            Notifier.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock1.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            
            LookCoupon.Visibility = System.Windows.Visibility.Visible;
        }


        private void viewCoupons(object sender, RoutedEventArgs e)
        {
            notifictions();
            updateNotify();
            
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            TextBlock1.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            Notifier.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            
            ViewCouponWindow.Visibility = System.Windows.Visibility.Visible;
            couponsView.ItemsSource = uBL.viewOrders(ID);
        }

        private void btn_buyCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (clsCoupon)this.couponsToBuyGrid.SelectedItem;
                string storeName = selected.getStore();
                DateTime due = selected.getDueDate();
                string desc = selected.getDescription();
                string email = uBL.getMail(ID);
                if (email == "")
                {
                    throw new Exception();
                }
                bool flag = uBL.buyCoupon(ID, storeName, due, desc, email);
                if (flag == false)
                {
                    MessageBox.Show("sorry wrong input!");
                    return;        
                }

                MessageBox.Show("Mail sent with receipt and serial key!");
                storeName = lookStore.Text;
                if (storeName.Equals(""))
                {
                    storeName = null;
                }
                string location = lookStore.Text;
                if (location.Equals(""))
                {
                    location = null;
                }
                bool preferences = false;
                if (lookByPref.IsChecked == true)
                {
                    preferences = true;
                }
                couponsToBuyGrid.ItemsSource = uBL.searchCoupon(ID, preferences, storeName, location);
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
            }
        }

        private void btn_searchCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                string storeName = lookStore.Text;
                if (storeName.Equals(""))
                {
                    storeName = null;
                }
                string location = lookStore.Text;
                if (location.Equals(""))
                {
                    location = null;
                }
                bool preferences = false;
                if (lookByPref.IsChecked == true)
                {
                    preferences = true;
                }
                couponsToBuyGrid.ItemsSource = uBL.searchCoupon(ID, preferences, storeName, location);
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
            }
        }

        private void btn_addCouponFromSocial(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = newCouponSocialName.Text;
                DateTime dueDate = DateTime.Parse(newCouponSocialDateEnd.Text);
                string description = newCouponSocialDesc.Text;
                int quantity = Int32.Parse(newCouponSocialQuantity.Text);
                string category = newCouponSocialCategory.Text;
                double originalPrice = Double.Parse(newCouponSocialOrgPrice.Text);
                double newPrice = Double.Parse(newCouponSocialNewPrice.Text);
                bool legal = itsAdminBL.addCoupon( name, dueDate, description, quantity, category, originalPrice, newPrice);
                if (!legal) throw new Exception();
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
                return;
            }
            MessageBox.Show("Success!");
        }

        private void btn_rateCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (clsUsersCoupon)this.boughtCouponsGrid.SelectedItem;
                int rate = Int32.Parse(ratingCouponBox.Text);
                if (rate < 1 | 5 < rate)
                {
                    throw new Exception();
                }
                int success = uBL.rateCoupon(ID, selected.getStore(), selected.getDueDate(), selected.getDescription(), rate);
                if (success == -1)
                {
                    throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
                return;
            }
            boughtCouponsGrid.ItemsSource = uBL.viewOrders(ID);
        }



        private void notifictions()  // after authorisation of coupon that of is interests
        {
            // no more than 10 notifications for a day 
           
            // according to users pref:
            List<clsCoupon> allCoupons = itDAL.getAllCoupons();
            List<clsCoupon> ans = new List<clsCoupon>();
            foreach (clsCoupon c in allCoupons)
            {  // a week before due date of the coupon 
                if (c.getAuthByAdmin() )
                {
                    if (c.getDueDate().DayOfYear <= DateTime.Now.DayOfYear + 7)
                    {
                        ans.Add(c);
                    }else if  ( prefer==true && c.getCatagory()==uBL.getCategories(ID).ToString() )
                    {
                        ans.Add(c);
                    }
                    if ( location ==true &&  c.getStore() == itDAL.getUserByID(ID).getInitialLocation())
                    {
                        if ( prefer==false)
                        {
                            ans.Add(c);
                        }
                        
                    }
                
               }
            }
            //notify:
            UserWindow uw = new UserWindow();
            numOfCoupToNotify = ans.Count;
            if (ans.Count>index && numOFNotifications<=10) // there is at least one coupon 
            {
                message = ans.ElementAt(index).getDescription();
                notifyMessage();
                index++;
                numOFNotifications++;
            }
        }
        private void updateNotify()
        {
            if (index >= numOfCoupToNotify)
            {
                index = 0;
            }
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        private void notifyMessage()
        {
            // a notificationis on for no more than a minute
            AutoClosingMessageBox.Show(message, "Notification about coupon nearby/ending soon ", 60000);
        }*/
        private void btn_addCouponFromSocial(object sender, RoutedEventArgs e)
        {

        }

        private void btn_buyCoupon(object sender, RoutedEventArgs e)
        {

        }

        private void btn_rateCoupon(object sender, RoutedEventArgs e)
        {

        }

        private void btn_searchCoupon(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Coupon(object sender, RoutedEventArgs e)
        {

        }

        private void setNotifications(object sender, RoutedEventArgs e)
        {

        }

        private void viewCoupons(object sender, RoutedEventArgs e)
        {

        }
    }
}
