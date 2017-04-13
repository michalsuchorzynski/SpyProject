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
            using (ClientServiceClient client = new ClientServiceClient())
            {
                foreach(var page in client.GetAcceptablePageFromDB())
                {
                    _acceptablePages.Add(page);
                }
                foreach (var group in client.GetPagesGroupFromDB())
                {
                    _acceptablePagesGroups.Add(group);
                }
                foreach (var page in client.GetAcceptablePageForGroupFromDB(1))
                {
                    _activeGroupPages.Add(page);
                }
                

            }
        }
        public bool GenereteListBox(ListBox allPages, ListBox allgroups, ListBox activegrouppages)
        {
            foreach (var page in _acceptablePages)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = GetDomainNameFromURL(page.Url)
                };
                allPages.Items.Add(listitem);
            }
            foreach (var group in _acceptablePagesGroups)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = group.Name
                };
                allgroups.Items.Add(listitem);
            }
            foreach (var page in _activeGroupPages)
            {
                ListBoxItem listitem = new ListBoxItem()
                {
                    Content = GetDomainNameFromURL(page.Url)
                };
                activegrouppages.Items.Add(listitem);
            }
            return true;
        }
        private string GetDomainNameFromURL(string url)
        {
            Uri domainUri = new Uri(url);
            return domainUri.Host;
        }

    }
}
