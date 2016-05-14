using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using ForumBuilder.Common.DataContracts;
using WebClient.proxies;
using WebClient.notificationHost;
using System.ServiceModel;
using ForumBuilder.Common.ClientServiceContracts;


namespace WebClient
{
    public partial class ForumWindow : System.Web.UI.Page, IUserNotificationsService
    {
        public static int ADD_SUB_FORUM_INDEX = 0;
        private static int PRIVATE_MESSAGES_INDEX = 1;
        private static int SET_PREFERENCES_INDEX = 2;
        private static int SIGNUP_INDEX = 3;
        private static int LOGOUT_INDEX = 4;

        private ForumData _myforum;
        private String _subForumChosen;
        private ForumManagerClient _fMC;
        private string _userName;
        private SuperUserManagerClient _sUMC;

        public Boolean newPostNotification = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            _fMC = (ForumManagerClient)Session["ForumManagerClient"];
            _fMC.InnerDuplexChannel.CallbackInstance = new InstanceContext(this);
            String _chosenForum = (String)Session["forumName"];
            this._myforum = _fMC.getForum(_chosenForum);
            lbl_forumName.Text = "ForumName:  " + _chosenForum;
            _userName = (String)Session["userName"];
            _sUMC = new SuperUserManagerClient();
            InitializePermissons(_userName);
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            foreach (string subForum in _myforum.subForums)
            {
                Button btn = new Button();
                btn.Text = subForum;
                btn.Click += new EventHandler(clickOnSubForum);
                cell.Controls.Add(btn);
                row.Cells.Add(cell);
                tbl_subForumList.Rows.Add(row);
            }
        }
        
        private void InitializePermissons(string userName)
        {
            if (userName == null)
                return;//TODO userName was not passed in the session, how should we handle
            // a guest
            if (userName.Equals("Guest"))
            {
                menu.Items[ADD_SUB_FORUM_INDEX].Enabled = false;
                menu.Items[SET_PREFERENCES_INDEX].Enabled = false;
            }
            // a member but not an admin
            else if (!_fMC.isAdmin(userName, _myforum.forumName) && !_sUMC.isSuperUser(userName))
            {
                menu.Items[ADD_SUB_FORUM_INDEX].Enabled = false;
                menu.Items[SET_PREFERENCES_INDEX].Enabled = false;
                menu.Items[SIGNUP_INDEX].Enabled = false;
            }
            // an admin
            else if (!_sUMC.isSuperUser(userName))
            {
                menu.Items[SIGNUP_INDEX].Enabled = false;
            }
            //  a super user
            else
            {
                // all open 
            }
        }

        protected void NavigationMenu_MenuItemClick(Object sender, MenuEventArgs e)
        {
            switch (e.Item.Value)
            {
                case "AddSub": 
                {
                    Response.Redirect("AddNewSubForum.aspx");
                } break;
                case "Set": 
                { 
                    Response.Redirect("SetPreferences.aspx");
                } break;
                case "SignUP": 
                { 
                    SignUP(); 
                } break;
                case "menuLogout": 
                { 
                    logout(_userName); 
                } break;
                case "privateMessages": 
                {
                    Session["userName"] = this._userName;
                    Response.Redirect("PrivateMessagesWindow.aspx");
                } break;
            }
        }



        private void logout(String nameLogout)
        {
            // a guest
            if (nameLogout.Equals("Guest"))
            {
                Response.Redirect("MainWindow.aspx");
            }
            // an fourom member
            else
            {
                _fMC.logout(nameLogout, _myforum.forumName);
                Response.Redirect("MainWindow.aspx");
            }
        }



        private void SignUP()
        {
            Session["ForumController"] = _fMC;
            Session["forumName"] = _myforum.forumName;
            Response.Redirect("SignUpWindow.aspx");
        }

        protected void clickOnSubForum(Object sender, EventArgs e)
        {//TODO gal: redirect to the subforum
            showAlert(((Button)sender).Text + _userName);
        }
        /*
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {//TODO gal
            // Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            _subForumChosen = selected.ToString();
            SubForumWindow sfw = new SubForumWindow(_myforum.forumName, _subForumChosen, _userName);
            sfw.ShowDialog();
        }



        
        private void subForumsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get SelectedItems from DataGrid.

            _subForumChosen = subForumsListBox.SelectedItem.ToString();
            SubForumWindow sfw = new SubForumWindow(_myforum.forumName, _subForumChosen, _userName);
            sfw.Show();
            this.Close();
        }
*/


        /*back button back to the forum window
         * TODO gal: should the forum data be passed?
        protected void backButton_Click(object sender, EventArgs e)
        {
            Session["forumData"] = _myforum;
            ForumWindow newWin = new ForumWindow(_myforum, _userName);
            newWin.Show();
            this.Close();
        }*/

        public void applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {
            //TODO failed to effect the web page dynamically (gal)
        }

        public void applyPostModificationNotification(String forumName, String publisherName, String title, String content)
        {
            //MessageBox.Show(publisherName + "'s post you were following in " + forumName + "was modified (" + title + ")", "post modified");
        }

        public void applyPostDelitionNotification(String forumName, String publisherName)
        {
            //MessageBox.Show(publisherName + "'s post you were following in " + forumName + "was deleted", "post deleted");
        }

        public void sendUserMessage(String senderName, String content)
        {
            //MessageBox.Show(content, senderName + " set you a message");
        }

        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }

    }
}