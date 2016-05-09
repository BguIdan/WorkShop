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

namespace PL
{
    /// <summary>
    /// Interaction logic for DismissModerator.xaml
    /// </summary>
    public partial class DismissModerator : Window
    {
        private SubForumManagerClient _sf;
        private Window _oldWin;
        private string _userName;
        private string _forumName;
        private string _subForumName;
        private bool _whatToDo;//false = dismiss true = add

        public DismissModerator(Window oldWin, string userName, string forumName, string subForumName, bool whatoDo)
        {
            InitializeComponent();
            _sf = new SubForumManagerClient();
            _oldWin = oldWin;
            _userName = userName;
            _forumName = forumName;
            _subForumName = subForumName;
            _whatToDo = whatoDo;
            duration.Visibility = Visibility.Collapsed;
            if (whatoDo)
            {
                duration.Visibility = Visibility.Visible;
                textBox.Text = "moderator to add";
                dismissButton.Content = "Add";
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            _oldWin.Show();
            this.Close();
        }

        private void dismissButton_Click(object sender, RoutedEventArgs e)
        {
            if (_whatToDo)
            {
                int durationAsInt = 120;
                try
                {
                    durationAsInt = int.Parse(duration.Text);
                }
                catch
                {
                    MessageBox.Show("error");
                    return;
                }
                if (_sf.nominateModerator(textBox.Text, _userName, DateTime.Now.AddDays(durationAsInt), _subForumName, _forumName))
                {
                    MessageBox.Show("moderator was added");
                    _oldWin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
            else
            {
                if (_sf.dismissModerator(textBox.Text, _userName, _subForumName, _forumName))
                {
                    MessageBox.Show("moderator was dismissed");
                    _oldWin.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("error");
                }
            }
        }
    }
}
