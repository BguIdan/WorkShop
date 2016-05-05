using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ForumBuilder.Common.DataContracts;
using PL.proxies;

namespace PL
{
    /// <summary>
    /// Interaction logic for SubForumWindow.xaml
    /// </summary>
    /// 
    public class dataContainer
    {
        private int Id;
        private string Title;
        private string Writer;
        private string Time;

        public string _title
        {
            get
            {
                return Title;
            }

            set
            {
                Title = value;
            }
        }

        public string _writer
        {
            get
            {
                return Writer;
            }

            set
            {
                Writer = value;
            }
        }

        public string _time
        {
            get
            {
                return Time;
            }

            set
            {
                Time = value;
            }
        }

        public int _id
        {
            get
            {
                return Id;
            }

            set
            {
                Id = value;
            }
        }
    }

    public partial class SubForumWindow : Window
    {
        private PostManagerClient _pm;
        private string _userName;
        private int _patentId;//used for adding post;

        public SubForumWindow(string fName, string sfName, string userName)//forum subforum names and userName
        {
            InitializeComponent();
            _pm = new PostManagerClient();
            forumName.Content = fName;
            sForumName.Content = sfName;
            _userName = userName;
            _patentId = -1;
        }

        private void ThreadView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            var selected = grid.SelectedItem as dataContainer;
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            var commentTable = new List<dataContainer>();
            foreach (PostData pd1 in posts)
            {
                if (pd1.id == selected._id)//needs to show the thread of this post
                {
                    List<int> commentsIds = pd1.commentsIds;
                    //going over all comments to make a new table
                    _patentId = pd1.id;
                    foreach (int singleCommentId in commentsIds)
                    {
                        foreach (PostData pd2 in posts)
                        {
                            if (pd2.id == singleCommentId)
                            {
                                dataContainer dt = new dataContainer();
                                dt._id = pd2.id;
                                dt._title = pd2.title;
                                dt._writer = pd2.writerUserName;
                                dt._time = pd2.timePublished.ToString();
                                commentTable.Add(dt);
                            }
                        }
                    }
                }
            }
            singleThread.ItemsSource = commentTable;
            threadView.Visibility = Visibility.Collapsed;
            threadTextBox.Visibility = Visibility.Collapsed;
            addThreadButton.Visibility = Visibility.Collapsed;
            addPostButton.Visibility = Visibility.Visible;
            singleThread.Visibility = Visibility.Visible;

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

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            var table = new List<dataContainer>();
            foreach (PostData pd in posts)
            {
                if (pd.parentId == -1)//if its the first message in thread
                {
                    dataContainer dt = new dataContainer();
                    dt._id = pd.id;
                    dt._title = pd.title;
                    dt._writer = pd.writerUserName;
                    dt._time = pd.timePublished.ToString();
                    table.Add(dt);
                }
            }
            var grid = sender as DataGrid;
            grid.ItemsSource = table;
            addPostButton.Visibility = Visibility.Collapsed;
            singleThread.Visibility = Visibility.Collapsed;
            threadView.Visibility = Visibility.Visible;
        }

        private void singleThread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (singleThread.Visibility == Visibility.Visible)
            {
                singleThread.Visibility = Visibility.Collapsed;
                addPostButton.Visibility = Visibility.Collapsed;
                addThreadButton.Visibility = Visibility.Visible;
                threadView.Visibility = Visibility.Visible;
                threadTextBox.Visibility = Visibility.Visible;
                _patentId = -1;
            }
            else//needs to go back to previous page
            {

            }
        }

        private void openMessage_Click(object sender, RoutedEventArgs e)
        {
            var grid = singleThread;
            var selected = grid.SelectedItem as dataContainer;
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            foreach (PostData pd in posts)
            {
                if (pd.id == selected._id)
                {
                    MessageBox.Show(pd.content);
                }
            }
        }

        private void deleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            var grid = singleThread;
            var selected = grid.SelectedItem as dataContainer;
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            PostData postToDelete = null;
            foreach (PostData pd in posts)
            {
                if (pd.id == selected._id)
                {
                    postToDelete = pd;
                }
            }
            if (postToDelete != null)
            {
                _pm.deletePost(postToDelete.id, _userName);
            }
            else
            {
                MessageBox.Show("error: couldn't find message");
            }
        }

        private void addPostButton_Click(object sender, RoutedEventArgs e)
        {
            addPostAndThreadWindow win = new addPostAndThreadWindow(this, _patentId, _userName, forumName.Content.ToString(), sForumName.Content.ToString());
            this.Visibility = Visibility.Collapsed;
            win.Show();
        }
    }
}
