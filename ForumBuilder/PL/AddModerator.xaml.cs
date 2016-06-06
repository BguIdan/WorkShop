using PL.notificationHost;
using PL.proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddModerator.xaml
    /// </summary>
    public partial class AddModerator : Window
    {
        private string _userName;
        private string _subforum;
        private SubForumManagerClient _sm;
        private ForumManagerClient _fMC;

        public AddModerator(string subForum, string userName)
        {
            InitializeComponent();
            _userName = userName;
            _subforum = subForum;
            _sm = new SubForumManagerClient();
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string moderatorName = moderatortextBox.Text;
        }
    }
}
