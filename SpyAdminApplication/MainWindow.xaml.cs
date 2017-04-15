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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using SpyAdminApplication.Windows;
using SpyAdminApplication.ServiceReference1;

namespace SpyAdminApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AcceptablePages.AcceptablePagesControl _pagescontrol;
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            InitializeComponent();

        }

        #region Events
        private void MenuItemStart_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.PanelStart.Height = (GridLength)gridLengthConverter.ConvertFrom("1*");
            this.PanelPages.Height = (GridLength)gridLengthConverter.ConvertFrom("0*");
        }
        private void MenuItemPages_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            this.PanelStart.Height = (GridLength)gridLengthConverter.ConvertFrom("0");
            this.PanelPages.Height = (GridLength)gridLengthConverter.ConvertFrom("1*");
            _pagescontrol = new AcceptablePages.AcceptablePagesControl();
            using (ClientServiceClient client = new ClientServiceClient())
            {
                _pagescontrol.GenereteListBox(client, listBoxPages, listBoxGroups, listBoxPagesForGroup);
            }
        }

        private void buttonPagesForGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedgroups = _pagescontrol._acceptablePagesGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            var selectedpage = _pagescontrol._acceptablePages.FirstOrDefault(x => x.Url == ((ListBoxItem)this.listBoxPages.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.AddAcceptablePageForGroup(selectedpage, selectedgroups);
                _pagescontrol.GenerateactiveGroupPages(client, listBoxPagesForGroup, selectedgroups.AcceptablePagesGroupId);
            }
        }
        private void buttonPagesForGroupDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedgroups = _pagescontrol._acceptablePagesGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            var selectedPage = _pagescontrol._acceptablePages.FirstOrDefault(x => x.Url == ((ListBoxItem)this.listBoxPagesForGroup.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.DeleteAcceptablePageForGroup(selectedPage, selectedgroups);
            }
            this.listBoxPagesForGroup.Items.Remove(this.listBoxPagesForGroup.SelectedItem);
        }

        private void buttonGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addwindow = new AddItemWindow(this,AddWindowType.AddGroup, this.listBoxGroups);
            addwindow.Show();

        }
        private void buttonGroupDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = _pagescontrol._acceptablePagesGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
              client.DeletePagesGroup(selected);
            }
            this.listBoxGroups.Items.Remove(this.listBoxGroups.SelectedItem);
        }        

        private void buttonPagesAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addwindow = new AddItemWindow(this ,AddWindowType.AddPage, this.listBoxPages);
            addwindow.Show();
        }
        private void buttonPagesDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = _pagescontrol._acceptablePages.FirstOrDefault(x => x.Url == ((ListBoxItem)this.listBoxPages.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.DeleteAcceptablePage(selected);
            }
            this.listBoxPages.Items.Remove(this.listBoxPages.SelectedItem);
        }       
            
        private void listBoxGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedgroups = _pagescontrol._acceptablePagesGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                _pagescontrol.GenerateactiveGroupPages(client, listBoxPagesForGroup, selectedgroups.AcceptablePagesGroupId);
            }            
            if (CheckIsPageInGroup())
                this.buttonPagesForGroupAdd.IsEnabled = true;
            else
                this.buttonPagesForGroupAdd.IsEnabled = false;
            if (listBoxGroups.SelectedItem != null && listBoxPages.SelectedItem != null)
            {
                this.buttonPagesForGroupAdd.IsEnabled = false;
            }
            else
            {
                this.buttonPagesForGroupAdd.IsEnabled = true;
            }
        }
        private void listBoxPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxPages.SelectedItem == null)
            { 
                this.buttonPagesForGroupAdd.IsEnabled = false;
                return;
            }
            if (CheckIsPageInGroup())
                this.buttonPagesForGroupAdd.IsEnabled = true;
            else
                this.buttonPagesForGroupAdd.IsEnabled = false;
        }
        private void listBoxPagesForGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.buttonPagesForGroupDelete.IsEnabled = true;
        }
        #endregion

        private bool CheckIsPageInGroup()
        {
            if(listBoxPages.SelectedItem==null)
            {
                this.buttonPagesForGroupAdd.IsEnabled = false;
                return false;
            }
            foreach (ListBoxItem l_item in listBoxPagesForGroup.Items)
            {
                if (l_item.Content.ToString() == ((ListBoxItem)listBoxPages.SelectedItem).Content.ToString())
                {
                    this.buttonPagesForGroupAdd.IsEnabled = false;
                    return false;
                }
            }
            return true;
        }

        
    }
}
