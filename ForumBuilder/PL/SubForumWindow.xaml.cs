using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ForumBuilder.Common.DataContracts;
using PL.proxies;
using System.ServiceModel;
using PL.notificationHost;

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

        public dataContainer()
        {

        }

        public dataContainer(int id, string title, string writer, string time)
        {
            _id = id;
            _time = time;
            _title = title;
            _writer = writer;
        }

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
        private ForumManagerClient _fm;
        private string _userName;
        private int _patentId;//used for adding post;
        private List<dataContainer> dataOfEachPost;

        public SubForumWindow(string fName, string sfName, string userName)//forum subforum names and userName
        {
            InitializeComponent();
            _fm = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _pm = new PostManagerClient();
            forumName.Content = fName;
            sForumName.Content = sfName;
            _userName = userName;
            _patentId = -1;
            dataOfEachPost = new List<dataContainer>();
        }

        private void ThreadView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            var selected = grid.SelectedItem as dataContainer;
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
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
                                Expander exp = new Expander();
                                exp.Header = pd2.title + "                                                          " + pd2.timePublished + "\n pulished by:" + pd2.writerUserName;
                                exp.Content = pd2.content;
                                listBox.Items.Add(exp);
                                dataContainer dt = new dataContainer(pd2.id, pd2.title, pd2.writerUserName, pd2.timePublished.ToString());
                                dataOfEachPost.Add(dt);
                            }
                        }
                    }
                }
            }
            threadView.Visibility = Visibility.Collapsed;
            threadTextBox.Visibility = Visibility.Collapsed;
            addPostButton.Header = "add thread";
            listBox.Visibility = Visibility.Visible;
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
            listBox.Visibility = Visibility.Collapsed;
            threadView.Visibility = Visibility.Visible;
        }

        private void singleThread_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (listBox.Visibility == Visibility.Visible)
            {
                listBox.Visibility = Visibility.Collapsed;
                addPostButton.Header = "add post";
                threadView.Visibility = Visibility.Visible;
                threadTextBox.Visibility = Visibility.Visible;
                _patentId = -1;
            }
            else//needs to go back to previous page
            {

            }
        }

        private void deleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            int index = listBox.SelectedIndex;
            dataContainer selected = dataOfEachPost[index];
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
                listBox.Items.RemoveAt(index);
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = e.Source as MenuItem;
            switch (menuItem.Name)
            {
                case "addPostButton": { addPostButton_Click(sender,e); } break;
                case "deleteMessageButton": { deleteMessageButton_Click(sender, e); } break;
                case "backButton": { back_Click(sender, e); } break;
                case "logOutButton": { logOut(sender, e); } break;
            }
        }

        private void logOut(object sender, RoutedEventArgs e)
        {
            _fm.logout(_userName, forumName.Content.ToString());
            MainWindow newWin = new MainWindow();
            newWin.Show();
            this.Close();
        }
    }
}
