using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class SetPreferences : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*
 * TODO gal: the enabled text fields in the set prefs is currently muted
private void descChoose(object sender, RoutedEventArgs e)
{
    bool toChange = descCheck.IsChecked.Value;
    if (toChange) { ForumDescToSet.IsEnabled = true; }
    else { ForumDescToSet.IsEnabled = false; }
}

private void policyChoose(object sender, RoutedEventArgs e)
{
    bool toChange = policyCheck.IsChecked.Value;
    if (toChange) { ForumPolicyToSet.IsEnabled = true; }
    else { ForumPolicyToSet.IsEnabled = false; }
}

private void rulesChoose(object sender, RoutedEventArgs e)
{
    bool toChange = rulesCheck.IsChecked.Value;
    if (toChange) { ForumRulesToSet.IsEnabled = true; }
    else { ForumRulesToSet.IsEnabled = false; }
}

private void btn_SetForumPref(object sender, RoutedEventArgs e)
{
    MyDialog.Visibility = System.Windows.Visibility.Visible;
    MyDialog.Focusable = true;
}

private void btn_toSetPref(object sender, RoutedEventArgs e)
{TODO gal: confirmation for the change
    String temp = "";
    var btn = sender as Button;
    if (btn.Name.Equals("yesBtn"))
    {
        temp = yesBtn.Content.ToString();
    }
    else { temp = noBtn.Content.ToString(); }
    setPref(temp);
}

private void setPref(String isDone)
{
    MyDialog.Focusable = false;
    MyDialog.Visibility = System.Windows.Visibility.Collapsed;

    if (isDone.Equals("Yes"))
    {
        string temp = "";
        bool toChange = descCheck.IsChecked.Value;
        if (toChange)
        {
            temp = ForumDescToSet.Text;
            _myforum.description = temp;
        }
        toChange = policyCheck.IsChecked.Value;
        if (toChange)
        {
            temp = ForumPolicyToSet.Text;
            _myforum.forumPolicy = temp;
        }
        toChange = rulesCheck.IsChecked.Value;
        if (toChange)
        {
            temp = ForumRulesToSet.Text;
            _myforum.forumRules = temp;
        }
        MessageBox.Show("Preferences was successfully changed!");
        setPreferencesWin.Visibility = System.Windows.Visibility.Collapsed;
    }
}*/

    }
}