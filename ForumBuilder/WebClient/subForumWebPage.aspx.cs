using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ServiceModel;
using ForumBuilder.Common.DataContracts;
using System.Threading;
using ForumBuilder.Common.ClientServiceContracts;
using WebClient.proxies;
using PL.notificationHost;

namespace WebClient
{
    public partial class subForumWebPage : System.Web.UI.Page
    {
        private PostManagerClient _pm;
        private ForumManagerClient _fm;
        private string _forumName;
        private string _subForumName;

        protected void Page_Load(object sender, EventArgs e)
        {
            _fm = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));
            _pm = new PostManagerClient();
            while (Session["forumName"] == null)
            {

            }
            _forumName = Session["forumName"].ToString();
            _subForumName = Session["subForumName"].ToString();
            forumNameLabel.Text = _forumName;
            subForumNameLabel.Text = _subForumName;
            List<PostData> posts = _pm.getAllPosts(_forumName, _subForumName);
            int num = 1;
            foreach(PostData post in posts)
            {
                if (post.parentId == -1)
                {
                    //1 field
                    TableRow tRow1 = new TableRow();
                    TableCell tCell1 = new TableCell();
                    Label lb1 = new Label();
                    lb1.Text = "#"+num;
                    tCell1.Controls.Add(lb1);
                    tRow1.Cells.Add(tCell1);
                    //field 2
                    TableRow tRow2 = new TableRow();
                    TableCell tCell2 = new TableCell();
                    Label lb2 = new Label();
                    lb2.Text = post.title;
                    tCell2.Controls.Add(lb2);
                    tRow2.Cells.Add(tCell2);
                    //field 3
                    TableRow tRow3 = new TableRow();
                    TableCell tCell3 = new TableCell();
                    Label lb3 = new Label();
                    lb3.Text = post.writerUserName;
                    tCell3.Controls.Add(lb3);
                    tRow3.Cells.Add(tCell3);
                    //field 4
                    TableRow tRow4 = new TableRow();
                    TableCell tCell4 = new TableCell();
                    Label lb4 = new Label();
                    lb4.Text = post.timePublished.ToString();
                    tCell4.Controls.Add(lb4);
                    tRow4.Cells.Add(tCell4);
                    //ad all rows
                    ThreadTable.Rows.Add(tRow1);
                    ThreadTable.Rows.Add(tRow2);
                    ThreadTable.Rows.Add(tRow3);
                    ThreadTable.Rows.Add(tRow4);
                }
                num++;
            }

        }
    }
}