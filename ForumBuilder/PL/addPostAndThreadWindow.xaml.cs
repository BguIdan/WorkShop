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
using ForumBuilder.Common.DataContracts;

namespace PL
{
    /// <summary>
    /// Interaction logic for addPostAndThreadWindow.xaml
    /// </summary>
    public partial class addPostAndThreadWindow : Window
    {
        private Window _prevWindow;
        private int _parentID;
        private PostManagerClient _pm;
        private string _userName;
        private string _forumName;
        private string _subForumName;

        public addPostAndThreadWindow(Window prevWindow, int parrentId, string userName, string forumName, string subForumName)//-1 for new thread
        {
            InitializeComponent();
            _forumName = forumName;
            _subForumName = subForumName;
            _prevWindow = prevWindow;
            _parentID = parrentId;
            if (parrentId == -1)
            {
                whatToAdd.Content = "add new thread";
            }
            else
            {
                whatToAdd.Content = "add new post";
            }
            _pm = new PostManagerClient();
            _userName = userName;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (_pm.addPost(title.Text, content.Text, _userName, _parentID))
            {
                MessageBox.Show("post was added succesfully");
            }
            else
            {
                MessageBox.Show("could not add post");
            }

            _prevWindow.Close();
            SubForumWindow newWin = new SubForumWindow(_forumName, _subForumName, _userName);
            this.Close();
            newWin.Show();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            _prevWindow.Visibility = System.Windows.Visibility.Visible;
            this.Close();
        }
    }
}
