﻿using PL.notificationHost;
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
        private string _forum;

        private SubForumManagerClient _sm;
        private ForumManagerClient _fMC;

        public AddModerator(string subForum, string userName, string forum)
        {
            InitializeComponent();
            _userName = userName;
            _subforum = subForum;
            _forum = forum;
            _sm = new SubForumManagerClient();
            _fMC = new ForumManagerClient(new InstanceContext(new ClientNotificationHost()));

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string moderatorName = moderatortextBox.Text;
            string res = _sm.nominateModerator(moderatorName, _userName, DateTime.Now.AddYears(120), _subforum, _forum);
            if(res.Equals("nominate moderator succeed"))
            {
                MessageBox.Show("moderator was added successfully");
            }
            else
            {
                MessageBox.Show(res);
                SubForumWindow newWin = new SubForumWindow(_forum, _subforum, _userName);
                newWin.Show();
                this.Close();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            SubForumWindow newWin = new SubForumWindow(_forum, _subforum, _userName);
            newWin.Show();
            this.Close();
        }

        private void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            durationComboBox.Items.Clear();
            durationComboBox.Items.Add("Unlimited");
            int minDays = 0;
            int maxDays = 365;
            for (int i = minDays; i <= maxDays; i++)
            {
                durationComboBox.Items.Add(i);
            }
        }
    }
}