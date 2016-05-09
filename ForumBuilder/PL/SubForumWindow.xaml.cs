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
        private int _id;
        private string _title;
        private string _writer;
        private string _time;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        public string Writer
        {
            get
            {
                return _writer;
            }

            set
            {
                _writer = value;
            }
        }

        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }

        public dataContainer()
        {

        }

        public dataContainer(int id, string title, string writer, string time)
        {
            Id = id;
            Title = title;
            Writer = writer;
            Time = time;
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
            if (selected == null)
            {
                return;
            }
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            foreach (PostData pd1 in posts)
            {
                if (pd1.id == selected.Id)//needs to show the thread of this post
                {
                    List<int> commentsIds = new List<int>();
                    commentsIds.Add(selected.Id);
                    foreach (PostData tempPostData in posts)
                    {
                        if (tempPostData.parentId == pd1.id)
                        {
                            commentsIds.Add(tempPostData.id);
                        }
                    }
                    //going over all comments to make a new table
                    _patentId = pd1.id;
                    foreach (int singleCommentId in commentsIds)
                    {
                        foreach (PostData pd2 in posts)
                        {
                            if (pd2.id == singleCommentId)
                            {
                                ListBox innerListBox = new ListBox();
                                Expander exp = new Expander();
                                exp.Header = pd2.title + "                                     " + pd2.timePublished + "\n pulished by:" + pd2.writerUserName;
                                exp.Content = pd2.content;
                                CheckBox cb = new CheckBox();
                                innerListBox.Items.Add(exp);
                                innerListBox.Items.Add(cb);
                                listBox.Items.Add(innerListBox);
                                dataContainer dt = new dataContainer(pd2.id, pd2.title, pd2.writerUserName, pd2.timePublished.ToString());
                                dataOfEachPost.Add(dt);
                            }
                        }
                    }
                }
            }
            threadView.Visibility = Visibility.Collapsed;
            threadTextBox.Text = "   Posts";
            addPostButton.Header = "add post";
            addPostButton.Visibility = Visibility.Visible;
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
                    dt.Id = pd.id;
                    dt.Title = pd.title;
                    dt.Writer = pd.writerUserName;
                    dt.Time = pd.timePublished.ToString();
                    table.Add(dt);
                }
            }
            threadView.ItemsSource = table;
            addPostButton.Header = "Add Thread";
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
                addPostButton.Header = "add Thread";
                threadView.Visibility = Visibility.Visible;
                threadTextBox.Text = "   Threads";
                _patentId = -1;
            }
            else//needs to go back to previous page
            {
                ForumWindow newWin = new ForumWindow(_fm.getForum(forumName.Content.ToString()), _userName);
                newWin.Show();
                this.Close();
            }
        }

        private void deleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            int index = -1;
            int tempIndex = 0;
            bool found = false;
            foreach (ListBox item in listBox.Items)
            {
                if (!found)
                {
                    if (item != null && item.Items[0] != null && item.Items[1] != null)
                    {
                        CheckBox cb = (CheckBox)(item.Items[1]);
                        if (cb.IsChecked.Value)
                        {
                            found = true;
                            index = tempIndex;
                        }
                    }
                    tempIndex++;
                }
            }
            if (index == -1)
            {
                MessageBox.Show("no box is checked");
                return;
            }
            dataContainer selected = dataOfEachPost[index];
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            PostData postToDelete = null;
            foreach (PostData pd in posts)
            {
                if (pd.id == selected.Id)
                {
                    postToDelete = pd;
                }
            }
            if (postToDelete != null)
            {
                _pm.deletePost(postToDelete.id, _userName);
                listBox.Items.RemoveAt(index);
                if (index == 0)
                {
                    SubForumWindow newWin = new SubForumWindow(forumName.Content.ToString(), sForumName.Content.ToString(), _userName);
                    newWin.Show();
                    this.Close();
                }
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
                case "addPostButton": { addPostButton_Click(sender, e); } break;
                case "deleteMessageButton": { deleteMessageButton_Click(sender, e); } break;
                case "backButton": { back_Click(sender, e); } break;
                case "logOutButton": { logOut(sender, e); } break;
                case "privateMessages": { privateMessages_Click(sender, e); } break;
                case "editMassege": { editMessage_Click(sender, e); } break;
                case "dismissModerator": { dismissModerator_Click(sender, e, false); } break;
                case "nominateModerator": { dismissModerator_Click(sender, e, true); } break;

            }
        }
        private void dismissModerator_Click(object sender, RoutedEventArgs e, bool whaToDo)
        {
            DismissModerator newWin = new DismissModerator(this, _userName, forumName.Content.ToString(), sForumName.Content.ToString(), whaToDo);
            newWin.Show();
            this.Visibility = Visibility.Collapsed;
        }

        private void editMessage_Click(object sender, RoutedEventArgs e)
        {
            int index = -1;
            int tempIndex = 0;
            bool found = false;
            foreach (ListBox item in listBox.Items)
            {
                if (!found)
                {
                    if (item != null && item.Items[0] != null && item.Items[1] != null)
                    {
                        CheckBox cb = (CheckBox)(item.Items[1]);
                        if (cb.IsChecked.Value)
                        {
                            found = true;
                            index = tempIndex;
                        }
                    }
                    tempIndex++;
                }
            }
            if (index == -1)
            {
                MessageBox.Show("no box is checked");
                return;
            }
            dataContainer selected = dataOfEachPost[index];
            List<PostData> posts = _pm.getAllPosts(forumName.Content.ToString(), sForumName.Content.ToString());
            PostData postToEdit = null;
            foreach (PostData pd in posts)
            {
                if (pd.id == selected.Id)
                {
                    postToEdit = pd;
                }
            }
            if (postToEdit != null)//if the wanted post exists
            {
                addPostAndThreadWindow newWin = new addPostAndThreadWindow(postToEdit, _userName, forumName.Content.ToString(), sForumName.Content.ToString());
                newWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("error: couldn't find message");
            }
        }

        private void privateMessages_Click(object sender, RoutedEventArgs e)

        {
            privateMessagesWindow newWin = new privateMessagesWindow(_userName, this);
            this.Visibility = Visibility.Collapsed;
            newWin.Show();
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
