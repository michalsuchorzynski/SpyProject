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
using System.Configuration;



namespace SpyClientLibrary
{
    public class Control
    {
        public TaskManager _taskmanager { get; set; }

        private ClientUser _user { get; set; }
        private WorkStation _myWorkstation { get; set; }
        private int _examsessionid;
        private string _ip;

        public Control(string ip)
        {
            _ip = ip;
        }
        public void StartControl()
        {
            User user = new User();
            _user = new ClientUser();
            _user.UserName = user._userName;

            GetCmd();
            Thread ThredScreen = new Thread(new ThreadStart(WaitForScreenCMD));
            ThredScreen.Start();

            _taskmanager = new TaskManager(_examsessionid);
            _user.ExamSessionId = _examsessionid;
            using (ClientServiceClient client = new ClientServiceClient())
            {
                _myWorkstation = new WorkStation();
                _myWorkstation.WorkStationId = client.GetWorkstationByIp(_ip);
                _user.ClientUserId = client.CreateUser(_user, _myWorkstation.WorkStationId);
                
            }
            while (true)
            {
                if (!_taskmanager.CheckOpenPrograms())
                {
                    ScreenShot screen = new ScreenShot(_user);
                    screen.isOffence = 1;
                    var sendedpages = _taskmanager.getSendedPages();
                    screen.Url = sendedpages.Last();
                    screen.GenerateCurrentScreen();
                    SendScreen(screen,_myWorkstation,_user);
                }
            }
        }

        public void WaitForScreenCMD()
        {
            while (true)
            {
                IPAddress ipAd = IPAddress.Parse(_ip);
                TcpListener myList = new TcpListener(ipAd, 8002);
                myList.Start();

                byte[] b = new byte[100];
                Socket s = myList.AcceptSocket();
                int k = s.Receive(b);
                for (int i = 0; i < k; i++)
                    Console.Write(Convert.ToChar(b[i]));

                ScreenShot screen = new ScreenShot(_user);
                screen.GenerateCurrentScreen();
                screen.isOffence = 0;
                var id = SendScreen(screen,_myWorkstation,_user);
                ASCIIEncoding asen = new ASCIIEncoding();
                s.Send(asen.GetBytes(id.ToString()));
                s.Close();
                myList.Stop();
            }
        }
        private void GetCmd()
        {
            IPAddress ipAd = IPAddress.Parse(_ip);
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

            _examsessionid = Convert.ToInt32(id);
            _user.ExamSessionId = _examsessionid;
            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes(_user.UserName));
            s.Close();
            myList.Stop();
        } 

        public int SendScreen(ScreenShot screen, WorkStation station, ClientUser user)
        {
            int id;
            ServiceReference.ScreenShot screentodb = new ServiceReference.ScreenShot()
            {
                Data = screen._data,
                ScreenDate = screen._scrrenDate,
                isOfense = screen.isOffence,
                Url = screen.Url
                
            };


            using (ClientServiceClient client = new ClientServiceClient())
            {

                screen.GenerateCurrentScreen();
                id = client.SaveScreenShotToDB(screentodb, station, user);

            }    
            return id;
        }
    }
}
