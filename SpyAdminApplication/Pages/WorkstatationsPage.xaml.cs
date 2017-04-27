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
using SpyAdminApplication.Windows;
using SpyAdminApplication.ServiceReference1;

namespace SpyAdminApplication.Pages
{
    /// <summary>
    /// Interaction logic for WorkstatationsPage.xaml
    /// </summary>
    public partial class WorkstatationsPage : Page
    {
        public Control.WorkstationsControl _workstationscontrol;

        public WorkstatationsPage()
        {
            InitializeComponent();
            _workstationscontrol = new Control.WorkstationsControl();
            using (ClientServiceClient client = new ClientServiceClient())
            {
                _workstationscontrol.GenereteListBox(client, listBoxPages, listBoxGroups, listBoxPagesForGroup);
            }

        }
        #region Events
        private void buttonPagesForGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            var selectedgroups = _workstationscontrol._workstationsGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            var selectedworkstation = _workstationscontrol._workstations.FirstOrDefault(x => x.IP == ((ListBoxItem)this.listBoxPages.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.AddWorkstationForGroup(selectedworkstation, selectedgroups);
                _workstationscontrol.GenerateactiveGroupworkstations(client, listBoxPagesForGroup, selectedgroups.WorkStationsGroupId);
            }
        }
        private void buttonPagesForGroupDelete_Click(object sender, RoutedEventArgs e)
        {
            var selectedgroups = _workstationscontrol._workstationsGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            var selectedworkstation = _workstationscontrol._workstations.FirstOrDefault(x => x.IP == ((ListBoxItem)this.listBoxPagesForGroup.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.DeleteWorkstationsForGroup(selectedworkstation, selectedgroups);
            }
            this.listBoxPagesForGroup.Items.Remove(this.listBoxPagesForGroup.SelectedItem);
        }

        private void buttonGroupAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addwindow = new AddItemWindow(this, AddWindowType.AddWorkstationGroup, this.listBoxGroups);
            addwindow.Show();

        }
        private void buttonGroupDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = _workstationscontrol._workstationsGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.DeleteWorkstationGroup(selected);
            }
            this.listBoxGroups.Items.Remove(this.listBoxGroups.SelectedItem);
        }

        private void buttonPagesAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addwindow = new AddItemWindow(this, AddWindowType.AddWorkstation, this.listBoxPages);
            addwindow.Show();
        }
        private void buttonPagesDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = _workstationscontrol._workstations.FirstOrDefault(x => x.IP == ((ListBoxItem)this.listBoxPages.SelectedItem).Content.ToString());
            using (ClientServiceClient client = new ClientServiceClient())
            {
               client.DeleteWorkstation(selected);
            }
            this.listBoxPages.Items.Remove(this.listBoxPages.SelectedItem);
        }

        private void listBoxGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedgroups = _workstationscontrol._workstationsGroups[0];
            if (this.listBoxGroups.SelectedItem != null)
            {
                selectedgroups = _workstationscontrol._workstationsGroups.FirstOrDefault(x => x.Name == ((ListBoxItem)this.listBoxGroups.SelectedItem).Content.ToString());
            }
            
            using (ClientServiceClient client = new ClientServiceClient())
            {
                _workstationscontrol.GenerateactiveGroupworkstations(client, listBoxPagesForGroup, selectedgroups.WorkStationsGroupId);
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
            if (listBoxPages.SelectedItem == null)
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
