using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.proxies;
using WebClient.notificationHost;
using System.ServiceModel;
using ForumBuilder.Common.DataContracts;
using System.Threading;
using ForumBuilder.Common.ClientServiceContracts;

namespace WebClient
{
    public partial class MainWindow : System.Web.UI.Page, IUserNotificationsService
    {
        private List<string> _forumsList;
        private String _choosenForum;
        private ForumManagerClient _fMC;

        protected void Page_Load(object sender, EventArgs e)
        {
            //TODO gal: remove later(including the sleep)
            //popup example
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert();</script>");

            Thread.Sleep(1000);

            _fMC = new ForumManagerClient(new InstanceContext(this));
            _forumsList = _fMC.getForums();
            //     forum_dropList.DataSource = this._forumsList;
            //      forum_dropList.DataBind();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            //TODO gal: consider removal
        }

        protected void Btn_ImSuperUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("SuperUserLoginWindow.aspx");
        }

        protected void Btn_Login_Click(object sender, EventArgs e)
        {
            Session["UserName"] = ID.Text;
            if (Password.Text != "")
            {
                Session["Password"] = Password.Text;
                try
                {
                    _choosenForum = forum_dropList.SelectedItem.Text;
                }
                catch
                {
                    showAlert("choose a forum");
                    return;
                }
                int sessionKey = -1;//general login error code
                if (_choosenForum != null)
                {
                    ForumData toSend = _fMC.getForum(_choosenForum);
                    if (CheckBox_Guest.Checked)
                    {
                        Session["forumName"] = _choosenForum;
                        Session["userName"] = "Guest";
                        Session["ForumManagerClient"] = _fMC;
                        Response.Redirect("ForumWindow.aspx");
                    }
                    else if ((sessionKey = _fMC.login(Session["UserName"].ToString(), _choosenForum, Session["Password"].ToString())) > 0)
                    //TODO gal consider additional error codes for informative error messages
                    {
                        Session["forumName"] = _choosenForum;
                        Session["userName"] = Session["UserName"];
                        Session["ForumManagerClient"] = _fMC;
                        Session["sessionKey"] = sessionKey;
                        Response.Redirect("ForumWindow.aspx");
                    }
                    else
                    {
                        switch (sessionKey)
                        {
                            case -1:
                                showAlert("login failed");
                                break;

                            case -2:
                                showAlert("user name \\ password are invalid");
                                break;

                            case -3:
                                showAlert("you already connected via another client, " +
                                            "please login using your session key");
                                break;

                            default:
                                showAlert("login failed");
                                break;
                        }
                    }

                }
                else
                {
                    showAlert("choose a forum");
                }
            }
            else if (Password.Text == "" && SessionKeyTextField.Text != "")
            {
                int insertedSessionKeyByInt = -1;
                String result = "";
                try
                {
                    insertedSessionKeyByInt = Int32.Parse(SessionKeyTextField.Text);
                }
                catch
                {
                    showAlert("invalid session key!, digits only");
                }
                try
                {
                    _choosenForum = forum_dropList.SelectedItem.Text;
                }
                catch
                {
                    showAlert("choose a forum");
                    return;
                }
                if (_choosenForum != null)
                {
                    ForumData toSend = _fMC.getForum(_choosenForum);
                    if (CheckBox_Guest.Checked)
                    {
                        showAlert("please clear the session key field");
                        return;
                    }
                    else if ((result = _fMC.loginBySessionKey(insertedSessionKeyByInt, Session["UserName"].ToString(), _choosenForum)) == "success")
                    //TODO gal consider additional error codes for informative error messages
                    {
                        Session["forumName"] = _choosenForum;
                        Session["userName"] = Session["UserName"];
                        Session["ForumManagerClient"] = _fMC;
                        Session["sessionKey"] = -1;//TODO make sure this is the correct change(used to be "sessionKey")
                        Response.Redirect("ForumWindow.aspx");
                    }
                    else
                    {
                        showAlert(result);
                        return;
                    }

                }
                else
                {
                    showAlert("choose a forum");
                    return;
                }

            }
            else
            {
                showAlert("please fill the required fields");
                return;
            }

            /* _choosenForum = forum_dropList.SelectedItem.Text;
             if (_choosenForum != null)
             {
                 ForumData toSend = _fMC.getForum(_choosenForum);
                 if (CheckBox_Guest.Checked)
                 {
                     Session["forumName"] = _choosenForum;
                     Session["userName"] = "Guest";
                     Session["ForumManagerClient"] = _fMC;
                     Response.Redirect("ForumWindow.aspx");
                 }
                 else if (_fMC.login(userName, _choosenForum, pass))
                 {
                     Session["forumName"] = _choosenForum;
                     Session["userName"] = userName;
                     Session["ForumManagerClient"] = _fMC;
                     Response.Redirect("ForumWindow.aspx");
                 }
                 else
                 {
                     showAlert("login failed");
                 }
             }*/
            //  else
            //  {
            //      showAlert("You have to choose forum from the list");
            //  }

        }

        protected void Btn_signUp_Click(object sender, EventArgs e)
        {
            showAlert("sign up");
        }

        private void showAlert(String content)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "popup", "<script>alert(\"" + content + "\");</script>");
        }

        protected void refreshForumList(object sender, EventArgs e)
        {
            //TODO gal: make the forum list refresh
        }

        public void applyPostPublishedInForumNotification(String forumName, String subForumName, String publisherName)
        {
            showAlert("new post<br>" + publisherName + " published a post in " + forumName +
                    "'s sub-forum " + subForumName);
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

        protected void forum_dropList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _choosenForum = forum_dropList.SelectedItem.ToString();
        }

        protected void forum_dropList_Load(object sender, EventArgs e)
        {
            forum_dropList.ClearSelection();
            while (_forumsList == null) { Thread.Sleep(20); }
            foreach (String forumName in this._forumsList)
                forum_dropList.Items.Add(forumName);
        }
    }
}