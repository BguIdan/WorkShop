using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class AddNewSubForum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       /* private void btn_createSub(object sender, RoutedEventArgs e)
        {
            int time = 0;
            int unlimited = 120;
            DateTime timeToSend = DateTime.Now;
            String sub_ForumName = subForumName.Text;
            // TODO: add to option for more than one moderator
            String userName = comboBox.Text;
            String timeDuration = comboBoxDuration.Text;
            if (timeDuration == null || userName == null || sub_ForumName == null || sub_ForumName.Equals(""))
            {
                MessageBox.Show("error has accured");
                return;
            }
            if (!timeDuration.Equals("UnLimited"))
            {
                time = int.Parse(timeDuration);
                timeToSend = DateTime.Now.AddDays(time);
            }
            else
            {
                timeToSend = DateTime.Now.AddYears(unlimited);
            }
            Dictionary<String, DateTime> dic = new Dictionary<string, DateTime>();
            dic.Add(userName, timeToSend);
            Boolean isAdded = _fMC.addSubForum(_myforum.forumName, sub_ForumName, dic, _userName);
            if (isAdded == false)
            {
                MessageBox.Show(userName + " can not be a moderator, try someone else.");
            }
            else
            {
                MessageBox.Show("Sub-Forum " + sub_ForumName + " was successfully created and " + userName + " is the Sub-Forum moderator.");
                ForumWindow newWin = new ForumWindow(_fMC.getForum(_myforum.forumName), _userName);
                this.Close();
                newWin.Show();

                //MainMenu.Visibility = System.Windows.Visibility.Visible;
                //mainGrid.Visibility = System.Windows.Visibility.Visible;
                //AddSubForum.Visibility = System.Windows.Visibility.Collapsed;
            }
        }*/

    }
}