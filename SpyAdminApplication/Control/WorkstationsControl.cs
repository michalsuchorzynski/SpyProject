using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyAdminApplication.ServiceReference1;
using System.Windows.Controls;

namespace SpyAdminApplication.Control
{
    public class WorkstationsControl
    {
        public List<WorkStation> _workstations;
        public List<WorkStationsGroup> _workstationsGroups;
        public List<WorkStation> _activeGroupWorkstations;

        public WorkstationsControl()
        {
            _workstations = new List<WorkStation>();
            _workstationsGroups = new List<WorkStationsGroup>();
            _activeGroupWorkstations = new List<WorkStation>();
        }
        public bool GenereteListBox(ClientServiceClient client, ListBox allWorkstations, ListBox allgroups, ListBox activegroupworkstations)
        {
            GenerateAcceptableworkstations(client, allWorkstations);
            GenerateAcceptableworkstationsGroups(client, allgroups);
            GenerateactiveGroupworkstations(client, activegroupworkstations, 1);
            return true;
        }
        public void GenerateAcceptableworkstations(ClientServiceClient client, ListBox allWorkstations)
        {
            allWorkstations.Items.Clear();
            _workstations.Clear();
            foreach (var workstation in client.GetWorkstationsFromDB())
            {
                _workstations.Add(workstation);
            }
            foreach (var workstation in _workstations)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = workstation.IP
                };
                allWorkstations.Items.Add(listitem);
            }
        }
        public void GenerateAcceptableworkstationsGroups(ClientServiceClient client, ListBox allgroups)
        {
            allgroups.Items.Clear();
            _workstationsGroups.Clear();
            foreach (var group in client.GetWorkstationsGroupFromDB())
            {
                _workstationsGroups.Add(group);
            }
            foreach (var group in _workstationsGroups)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = group.Name
                };
                allgroups.Items.Add(listitem);
            }
        }
        public void GenerateactiveGroupworkstations(ClientServiceClient client, ListBox activegroupworkstations, int groupIndex)
        {
            activegroupworkstations.Items.Clear();
            _activeGroupWorkstations.Clear();
            foreach (var workstation in client.GetWorkstationsForGroupFromDB(groupIndex))
            {
                _activeGroupWorkstations.Add(workstation);
            }
            foreach (var workstation in _activeGroupWorkstations)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = workstation.IP
                };
                activegroupworkstations.Items.Add(listitem);
            }
        }

    }
}
