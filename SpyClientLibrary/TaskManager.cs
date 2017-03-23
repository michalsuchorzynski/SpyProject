using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Automation;

namespace SpyClientLibrary
{
    public class TaskManager
    {
        private List<Process> _chromeProcess;
        private List<Process> _IEProcess;
        public List<Process> _edgeProcess;
        private List<Process> _firefoxProcess;
        private List<Process> _operaProcess;
        private List<String> _openedPages;
        private List<String> _acceptablePages;

        public TaskManager()
        {
            _chromeProcess = new List<Process>();
            _IEProcess = new List<Process>();
            _edgeProcess = new List<Process>();
            _firefoxProcess = new List<Process>();
            _operaProcess = new List<Process>();
            _openedPages = new List<string>();
            _acceptablePages = new List<string>();

            GetAcceptablePagesFromDB();
        }

        public bool CheckOpenPrograms()
        {
            ClearOpenPrograms();
            GetOpenPrograms();
            foreach(string opened in _openedPages)
            {
                foreach(string acceptabled in _acceptablePages)
                {
                    if(opened!=acceptabled)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public void GetOpenPrograms()
        {
            // TO DO FOR CHROME

            //foreach (var p in Process.GetProcessesByName("chrome"))
            //{
            //    var url = GetChromeUrl(p);
            //    if (url != null)
            //        _chromeProcess.Add(p);
            //}

            // FIND URL ON FIREFOX
            foreach (var p in Process.GetProcessesByName("firefox"))
            {
                var url = GetFirefoxUrl(p);
                if (url != null)
                {                    
                    _firefoxProcess.Add(p);
                    _openedPages.Add(url);                    
                }
            }

            // TO DO FOR EXPLORER
            //foreach (Process p in Process.GetProcessesByName("iexplore"))
            //{
            //    string url = GetInternetExplorerUrl(p);
            //    if (url != null)
            //        _IEProcess.Add(p);
            //}

            // TO DO FOR EDGE
            //foreach (Process p in Process.GetProcessesByName("MicrosoftEdge"))
            //{
            //    string url = GetInternetExplorerUrl(p);
            //    if (url != null)
            //        _IEProcess.Add(p);
            //}

            // TO DO FOR OPERA
            //foreach (Process p in Process.GetProcessesByName("opera"))
            //{
            //    string url = GetInternetExplorerUrl(p);
            //    if (url != null)
            //        _IEProcess.Add(p);
            //}
        }

        public void ClearOpenPrograms()
        {
            _chromeProcess = new List<Process>();
            _IEProcess = new List<Process>();
            _edgeProcess = new List<Process>();
            _firefoxProcess = new List<Process>();
            _operaProcess = new List<Process>();

        }

        #region GET URL FROM BROWSER
        private static string GetChromeUrl(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            if (process.MainWindowHandle == IntPtr.Zero)
                return null;

            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
            if (element == null)
                return null;

            AutomationElement edit = element.FindFirst(TreeScope.Subtree,
                 new AndCondition(
                      new PropertyCondition(AutomationElement.NameProperty, "address and search bar", PropertyConditionFlags.IgnoreCase),
                      new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit)));

            return ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
        }
        private static string GetInternetExplorerUrl(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            if (process.MainWindowHandle == IntPtr.Zero)
                return null;

            AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
            if (element == null)
                return null;

            AutomationElement rebar = element.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.ClassNameProperty, "ReBarWindow32"));
            if (rebar == null)
                return null;

            AutomationElement edit = rebar.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit));

            return ((ValuePattern)edit.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
        }
        private static string GetFirefoxUrl(Process process)
        {
            try
            {
                if (process.MainWindowHandle == IntPtr.Zero)
                {
                    return null;
                }
                AutomationElement element = AutomationElement.FromHandle(process.MainWindowHandle);
                if (element == null)
                    return null;

                AutomationElement custom1 = element.FindFirst(TreeScope.Descendants,
                 new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                AutomationElementCollection custom2 = custom1.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));

                foreach (AutomationElement item in custom2)
                {
                    AutomationElement custom3 = item.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Custom));
                    AutomationElement doc3 = custom3.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Document));

                    if (!doc3.Current.IsOffscreen)
                    {
                        return ((ValuePattern)doc3.GetCurrentPattern(ValuePattern.Pattern)).Current.Value as string;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return null;
        }
        #endregion

        private List<String> GetAcceptablePagesFromDB()
        {
            //TO DO CONNECTION TO DB 
            List<String> acceptablePages = new List<string>();
            acceptablePages.Add("https://www.facebook.com/");
            acceptablePages.Add("http://www.wp.pl/");


            _acceptablePages = acceptablePages;
            return acceptablePages;

        }
        private string GetDomainNameFromURL(string url)
        {
            Uri domainUri = new Uri(url);
            return domainUri.Host;
        }



    }
}