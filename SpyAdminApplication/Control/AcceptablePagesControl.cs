using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyAdminApplication.ServiceReference1;
using System.Windows.Controls;

namespace SpyAdminApplication.AcceptablePages
{
    public class AcceptablePagesControl
    {
        public List<AcceptablePage> _acceptablePages;
        public List<AcceptablePagesGroup> _acceptablePagesGroups;
        public List<AcceptablePage> _activeGroupPages;

        

        public AcceptablePagesControl()
        {
            _acceptablePages = new List<AcceptablePage>();
            _acceptablePagesGroups = new List<AcceptablePagesGroup>();
            _activeGroupPages = new List<AcceptablePage>();
        }
        public bool GenereteListBox(ClientServiceClient client, ListBox allPages, ListBox allgroups, ListBox activegrouppages)
        {
            GenerateAcceptablePages(client, allPages);
            GenerateAcceptablePagesGroups(client, allgroups);
            GenerateactiveGroupPages(client, activegrouppages, 1);
            return true;
        }
        public void GenerateAcceptablePages(ClientServiceClient client, ListBox allPages)
        {
            allPages.Items.Clear();
            _acceptablePages.Clear();
            foreach (var page in client.GetAcceptablePageFromDB())
            {
                _acceptablePages.Add(page);
            }
            foreach (var page in _acceptablePages)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = page.Url
                };
                allPages.Items.Add(listitem);
            }
        }
        public void GenerateAcceptablePagesGroups(ClientServiceClient client, ListBox allgroups)
        {
            allgroups.Items.Clear();
            _acceptablePagesGroups.Clear();
            foreach (var group in client.GetPagesGroupFromDB())
            {
                _acceptablePagesGroups.Add(group);
            }
            foreach (var group in _acceptablePagesGroups)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = group.Name
                };
                allgroups.Items.Add(listitem);
            }
        }
        public void GenerateactiveGroupPages(ClientServiceClient client, ListBox activegrouppages, int groupIndex)
        {
            activegrouppages.Items.Clear();
            _activeGroupPages.Clear();
            foreach (var page in client.GetAcceptablePageForGroupFromDB(groupIndex))
            {
                _activeGroupPages.Add(page);
            }
            foreach (var page in _activeGroupPages)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = page.Url
                };
                activegrouppages.Items.Add(listitem);
            }
        }
       

    }
}
