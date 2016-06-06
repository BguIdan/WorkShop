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
using ForumBuilder.Common;
using PL.proxies;
using ForumBuilder.Common.DataContracts;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddFriendWindow.xaml
    /// </summary>
    public partial class AddFriendWindow : Window
    {
        private string _userName;
        private ForumData _forumData;

        public AddFriendWindow(string userName, ForumData forumData)
        {
            InitializeComponent();
            _userName = userName;
            _forumData = forumData;

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            UserManagerClient uc = new UserManagerClient();
            if (uc.addFriend(_userName, friendName.Text))
            {
                MessageBox.Show("friend was added successfuly");
                ForumWindow newWin = new ForumWindow(_forumData, _userName);
                newWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("couldn't add the friend");
            }
        }

        private void to_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void button_back(object sender, RoutedEventArgs e)
        {
            ForumWindow mw = new ForumWindow(_forumData, _userName);
            mw.Show();
            this.Close();
        }
    }
}
