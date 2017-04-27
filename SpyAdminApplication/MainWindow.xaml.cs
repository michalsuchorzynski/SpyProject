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
using System.Collections.ObjectModel;
using SpyAdminApplication.Windows;
using SpyAdminApplication.ServiceReference1;
using SpyAdminApplication.Pages;

namespace SpyAdminApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AcceptablePages.AcceptablePagesControl _pagescontrol;
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        #region Events
        private void MenuItemStart_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            OneScreenShotPage showedPage = new OneScreenShotPage();
            firstFrame.Navigate(showedPage);
        }
        private void MenuItemPages_Click(object sender, RoutedEventArgs e)
        {
            GridLengthConverter gridLengthConverter = new GridLengthConverter();
            AcceptablePagesPage app = new AcceptablePagesPage();
            firstFrame.Navigate(app);
        }

        #endregion



    }
}
