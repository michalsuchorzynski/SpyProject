using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyClientLibrary
{
    public class User
    {
        public string _userName { get; set; }

        public User()
        {
            GetWindowsUser();
        }

        private void GetWindowsUser()
        {
            var computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (!string.IsNullOrEmpty(computerName))
                _userName = computerName.Substring(computerName.IndexOf("\\") + 1);
        }
    }
}
