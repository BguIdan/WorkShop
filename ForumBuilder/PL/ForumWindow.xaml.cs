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

namespace PL
{
    /// <summary>
    /// Interaction logic for ForumWindow.xaml
    /// </summary>
    public partial class ForumWindow : Window
    {
        //TODO: need to know Forum
        //private Forum _forum;
        private List<String> _subForumNames;
        private String _subForumChosen;

        public ForumWindow(string _forumName)
        {
            InitializeComponent();
            // TODO: Initialize field
            //_forum = getforum;
            forumName.Content = _forumName;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            //TODO: need to know Forum
            // ... Create a List of objects.
            /*var items = new List<Forum>();
            items.Add(new Forum("Fido", "10" , " ", " " , new List<string>()));
            items.Add(new Forum("Spark", "20" , " ", " " , new List<string>()));
            items.Add(new Forum("Fluffy", "4" , " ", " " , new List<string>()));*/
           
            /* Option B:
              var items = new List<String>();
              for(int i=0; i < _subForumNames.Count;i++)
              {
                  items.Add(_subForumNames.ElementAt(i));
              }*/

            // ... Assign ItemsSource of DataGrid.
            var grid = sender as DataGrid;
            //grid.ItemsSource = items;
        }

        private void DataGrid_SelectionChanged(object sender,SelectionChangedEventArgs e)
        {
            // ... Get SelectedItems from DataGrid.
            var grid = sender as DataGrid;
            var selected = grid.SelectedItems;
            _subForumChosen = selected.ToString();

            //TODO: Probably irrelevant (!!!)
            // ... Add all Names to a List.
            List<string> names = new List<string>();
            foreach (var item in selected)
            {
                //TODO: need to know Forum
                //var forum = item as Forum;
                //names.Add(forum.forumName);
                var forum = item as string;
                names.Add(forum);
            }
            // ... Set Title to selected names.
            this.Title = string.Join(", ", names);
        }
    }
}
