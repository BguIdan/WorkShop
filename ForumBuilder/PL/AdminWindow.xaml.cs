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
using BL_BackEnd;

namespace PL
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminBL adminBL;
        private UserBL itsUserBL;
        private int userID;

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

        public void Main()
        {

        }

        private void MenuItem_Coupon(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "AddSocial":    { AddCouponFromSocial(); } break;
                case "Add":          { AddRegularCoupon(); }    break;
                case "Confirm":      { ConfirmCoupon(); } break;
                case "Edit":         { EditCoupon(); } break;
                case "Delete":       { DeleteCoupon(); } break;
                case "Look":         { LookForAcoupon(); } break;
                case "Rank":         { RankACoupon(); }  break;
                case "View":         { ViewCoupon(); } break;
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

        private void RankACoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
         
            /* show only this window: */
            RankCouponWindow.Visibility = System.Windows.Visibility.Visible;
            boughtCouponsGrid.ItemsSource = adminBL.viewOrders(userID);
        }

        private void LookForAcoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
 	        LookCoupon.Visibility = System.Windows.Visibility.Visible;
        } 
        
        private void DeleteCoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            DeleteCouponWin.Visibility = System.Windows.Visibility.Visible;
            deleteCouponGrid.ItemsSource = adminBL.showCouponstoDelete();
        }

        private void BuyCoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            LookCoupon.Visibility = System.Windows.Visibility.Visible;
            //couponsToBuyGrid.ItemsSource = adminBL.showAuthorizedCoupons(); // need o write that one for userBL
            // show empty grid
        }

        private void EditCoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            EditCouponWin.Visibility = System.Windows.Visibility.Visible;
            EditChooseCouponWindow.Visibility = System.Windows.Visibility.Visible;
            couponsToEdit.ItemsSource = adminBL.showAllCoupons();
        }

        private void ConfirmCoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Visible;
            couponsToApproveView.ItemsSource = adminBL.showUnauthorizedCoupons();
        }

        private void AddRegularCoupon()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            AddCoupon.Visibility = System.Windows.Visibility.Visible;
        }

        private void AddCouponFromSocial()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            LookStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            AddCoupon.Visibility = System.Windows.Visibility.Visible;
        }      
        
        void MenuItem_Store(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "AddStore":        { AddAStore(); } break;
                case "LookStore":       { LookForAStore(); } break;
                case "DeleteStore":     { DeleteAStore(); } break;
            }
        }

        private void DeleteAStore()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            LookStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Visible;
            storesToDelete.ItemsSource = adminBL.searchStores(userID, false, null);
        }

        private void LookForAStore()
        {          
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            LookStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            LookStoreWindow.Visibility = System.Windows.Visibility.Visible;
            
        }

        private void AddAStore()
        {
            /* collaspe any other window: */
            TextBlock.Visibility = System.Windows.Visibility.Collapsed;
            AddCoupon.Visibility = System.Windows.Visibility.Collapsed;
            LookCoupon.Visibility = System.Windows.Visibility.Collapsed;
            DeleteCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            EditCouponWin.Visibility = System.Windows.Visibility.Collapsed;
            ConfirmCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            ViewCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            LookStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            AddStoreWindow.Visibility = System.Windows.Visibility.Collapsed;
            RankCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            DeleteStoreWindow.Visibility = System.Windows.Visibility.Collapsed;

            /* show only this window: */
            AddStoreWindow.Visibility = System.Windows.Visibility.Visible;
           
        }

        private void MenuItem_Account(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "Change": break;
                case "ViewPurchased": break;
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

                bool legal= adminBL.addCoupon(name, dueDate, description, quantity, category, originalPrice, newPrice);
                if (!legal) throw new Exception();
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
                return;
            }
            MessageBox.Show("Success!");
            
        }

        private void btn_approveCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (clsCoupon)this.couponsToApproveView.SelectedItem;
                adminBL.confirmCoupon(selected.getStore(), selected.getDueDate(), selected.getDescription());
                couponsToApproveView.ItemsSource = adminBL.showUnauthorizedCoupons();
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
            }
        }

        private void btn_editCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (clsCoupon)this.couponsToEdit.SelectedItem;
                QuantityEdit.Text = selected.getAvailableQuntity().ToString();
                nameEdit.Text = selected.getStore();
                CategoryEdit.Text = selected.getCatagory();
                NewPriceEdit.Text = selected.getNewPrice().ToString();
                OrgPriceEdit.Text = selected.getOldPrice().ToString();
                nameDescEdit.Text = selected.getDescription().ToString();
                dueDateEdit.Text = selected.getDueDate().ToString();
                lastdate.Text = selected.getDueDate().ToString();
                nameDescEdit.Text = selected.getDescription().ToString();
                EditChooseCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
                EditCouponWindow.Visibility = System.Windows.Visibility.Visible;
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
            }
        }

        private void btn_edit(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = nameEdit.Text;
                DateTime dueDate = DateTime.Parse(dueDateEdit.Text);
                string description = nameDescEdit.Text;
                int quantity = Int32.Parse(QuantityEdit.Text);
                string category = CategoryEdit.Text;
                double originalPrice = Double.Parse(OrgPriceEdit.Text);
                int newPrice = Int32.Parse(NewPriceEdit.Text);

                bool legal = adminBL.editCoupon(name, DateTime.Parse(lastdate.Text), nameDescEdit.Text, dueDate, description, quantity, newPrice);
                    //(name, dueDate, description, quantity, category, originalPrice, newPrice);
                if (!legal) throw new Exception();
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
                return;
            }
            MessageBox.Show("Success!");
            EditChooseCouponWindow.Visibility = System.Windows.Visibility.Visible;
            EditCouponWindow.Visibility = System.Windows.Visibility.Collapsed;
            
        }

        private void btn_deleteCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (clsCoupon)this.deleteCouponGrid.SelectedItem;
                string storeName = selected.getStore();
                DateTime due = selected.getDueDate();
                string desc = selected.getDescription();
                adminBL.removeCoupon(storeName, due, desc);
                deleteCouponGrid.ItemsSource = adminBL.showCouponstoDelete();
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
                couponsToBuyGrid.ItemsSource = adminBL.searchCoupon(userID, preferences, storeName, location);
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
            }
        }

        private void btn_buyCoupon(object sender, RoutedEventArgs e)
        {
            try
            {
                var selected = (clsCoupon)this.couponsToBuyGrid.SelectedItem;
                string storeName = selected.getStore();
                DateTime due = selected.getDueDate();
                string desc = selected.getDescription();
                string email = itsUserBL.getMail(userID);
                if (email == "")
                {
                    throw new Exception();
                }
                adminBL.buyCoupon(userID, storeName, due, desc, email);
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
                couponsToBuyGrid.ItemsSource = adminBL.searchCoupon(userID, preferences, storeName, location);
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
            }
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
                int success = adminBL.rateCoupon(userID, selected.getStore(), selected.getDueDate(), selected.getDescription(), rate);
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
            boughtCouponsGrid.ItemsSource = adminBL.viewOrders(userID);
        }

        private void btn_addStore(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = storeName.Text;
                string address = Address.Text;
                int storeId = Int32.Parse(storeID.Text); 
                string city = City.Text;
                string mail = Mail.Text;
                string pass = Password.Text;
                string description = Description.Text;
                string category = Category.Text;
                if (name.Equals("") || address.Equals("") || mail == null || (int?)storeId == null || pass ==null|| city.Equals("") || description.Equals("") || category.Equals(""))
                {
                    throw new Exception();
                }
                // check category validity
                adminBL.createNewStore(storeId, name, city, pass, address, description, category, mail);
                // add new store
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
                return;
            }
            MessageBox.Show("Store added!");
        }

        private void btn_searchStore(object sender, RoutedEventArgs e)
        {
            bool preferences = false;
            if (prefs.IsChecked == true)
            {
                preferences = true;
            }
            string city = StoreCity.Text;
            if (city.Equals(""))
            {
                city = null;
            }
            foundStoresGrid.ItemsSource = adminBL.searchStores(userID, preferences, city);
        }

        private void btn_deleteStore(object sender, RoutedEventArgs e)
        {
            string name;
            try
            {
                var selected = (clsStore)this.storesToDelete.SelectedItem;
                name = selected.getName();
                adminBL.removeStore(name);
            }
            catch
            {
                MessageBox.Show("sorry wrong input!");
                return;
            }
            MessageBox.Show("Store " + name + " has been deleted!");
        }


    }
}
