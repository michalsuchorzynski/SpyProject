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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime startTime = DateTime.Now;


            using (ClientServiceClient client = new ClientServiceClient())
            {
                var bytearray = client.GetScreenFromDB();
                ImageSourceConverter converter = new ImageSourceConverter();
                imageScreenShot.Source = ToImage(bytearray);
            }
            DateTime stopTime = DateTime.Now;
            TimeSpan roznica = stopTime - startTime;
            MessageBox.Show(roznica.TotalMilliseconds.ToString());

        }

        
        private void comboboxScreenNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                var bytearray = client.GetScreenByIdFromDB(Convert.ToInt32(comboboxScreenNumber.SelectedItem));
                ImageSourceConverter converter = new ImageSourceConverter();
                imageScreenShot.Source = ToImage(bytearray);

            }

        }
    }
}
