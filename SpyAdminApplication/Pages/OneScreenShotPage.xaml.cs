using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SpyAdminApplication.ServiceReference1;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace SpyAdminApplication.Pages
{
    /// <summary>
    /// Interaction logic for OneScreenShotPage.xaml
    /// </summary>
    public partial class OneScreenShotPage : Page
    {
        private int screenShotCount;
        public int examSessionId;
        public List<WorkStation> _currentSessionWorkstations;
        public OneScreenShotPage()
        {
            InitializeComponent();
            //using (ClientServiceClient client = new ClientServiceClient())
            //{
            //    screenShotCount = client.GetScreenCountFromDB();
            //    if (screenShotCount > 0)
            //    {
            //        var bytearray = client.GetScreenByIdFromDB(0);
            //        ImageSourceConverter converter = new ImageSourceConverter();
            //        imageScreenShot.Source = ToImage(bytearray);
            //        comboboxScreenNumber.SelectedIndex = 0;
            //    }
            //}
            using (ClientServiceClient client = new ClientServiceClient())
            {
                screenShotCount = client.GetScreenCountFromDB();
                if (screenShotCount > 0)
                {
                    screenShotCount = client.GetScreenCountFromDB();
                }
            }
            
            for (int i = 0;i<screenShotCount;i++)
            {
                comboboxScreenNumber.Items.Add(i.ToString());
            }
            
        }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void comboboxScreenNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboboxScreenNumber.SelectedItem == null)
                return;

            var selectedip = comboboxScreenNumber.SelectedItem.ToString().Substring(0, comboboxScreenNumber.SelectedItem.ToString().IndexOf("-"));
            var selectedWorkstation = _currentSessionWorkstations.Find(x => x.IP == selectedip);
            int selectedUser;

            using (ClientServiceClient client = new ClientServiceClient())
            {
                selectedUser = client.GetUserForWorkstation(examSessionId, selectedWorkstation.WorkStationId);
                var errorsforuser = client.GetOffenceScreenId(selectedUser);
                comboboxOfenceID.Items.Clear();
                for (int i = 0; i < errorsforuser.Length; i++)
                {
                    comboboxOfenceID.Items.Add(errorsforuser[i]);
                }
            }
        }
        private string SendCMD(string wIp, string cmd)
        {
            string request = "";
            TcpClient tcpclnt = new TcpClient();
            tcpclnt.Connect(wIp, 8002);

            Stream stm = tcpclnt.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] ba = asen.GetBytes(cmd);
            stm.Write(ba, 0, ba.Length);

            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);
            for (int i = 0; i < k; i++)
            {
                request += Convert.ToChar(bb[i]);
            }
            tcpclnt.Close();
            return request;
        }
        
        private void buttonGetCurrentScreen_Click(object sender, RoutedEventArgs e)
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                var current_ip = comboboxScreenNumber.SelectedItem.ToString().Substring(0,comboboxScreenNumber.SelectedItem.ToString().IndexOf("-"));
                var id = SendCMD(current_ip, "Screen");
                var bytearray = client.GetScreenByIdFromDB(Convert.ToInt32(id));
                ImageSourceConverter converter = new ImageSourceConverter();
                imageScreenShot.Source = ToImage(bytearray);

            }
        }

        private void buttonGetLastScreen_Click(object sender, RoutedEventArgs e)
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                var bytearray = client.GetScreenByIdFromDB(Convert.ToInt32(comboboxOfenceID.SelectedItem));
                ImageSourceConverter converter = new ImageSourceConverter();
                imageScreenShot.Source = ToImage(bytearray);

            }
        }
    }
}
