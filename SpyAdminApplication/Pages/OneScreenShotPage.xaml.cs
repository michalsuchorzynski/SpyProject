﻿using System;
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


namespace SpyAdminApplication.Pages
{
    /// <summary>
    /// Interaction logic for OneScreenShotPage.xaml
    /// </summary>
    public partial class OneScreenShotPage : Page
    {
        private int screenShotCount;
        public OneScreenShotPage()
        {
            InitializeComponent();
            using (ClientServiceClient client = new ClientServiceClient())
            {
                screenShotCount = client.GetScreenCountFromDB();
                if (screenShotCount > 0)
                {
                    var bytearray = client.GetScreenByIdFromDB(0);
                    ImageSourceConverter converter = new ImageSourceConverter();
                    imageScreenShot.Source = ToImage(bytearray);
                    comboboxScreenNumber.SelectedIndex = 0;
                }
            }
            for(int i = 0;i<screenShotCount;i++)
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
            using (ClientServiceClient client = new ClientServiceClient())
            {
                var id = SendCMD("192.168.1.1", "Screen");
                var bytearray = client.GetScreenByIdFromDB(Convert.ToInt32(id));
                ImageSourceConverter converter = new ImageSourceConverter();
                imageScreenShot.Source = ToImage(bytearray);

            }

        }
        private string SendCMD(string wIp, string cmd)
        {
            string request = "";
            TcpClient tcpclnt = new TcpClient();
            tcpclnt.Connect("95.108.69.237", 8002);

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

    }
}
