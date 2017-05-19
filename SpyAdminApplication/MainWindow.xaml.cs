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
using static SpyAdminApplication.Control.ExamSessionControl;

namespace SpyAdminApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AcceptablePages.AcceptablePagesControl _pagescontrol;
        public AcceptablePagesPage app = new AcceptablePagesPage();
        public WorkstatationsPage wp = new WorkstatationsPage();
        public ExamSessionStartPage es = new ExamSessionStartPage();
        public OneScreenShotPage showedPage = new OneScreenShotPage();

        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        #region Events
        private void MenuItemStart_Click(object sender, RoutedEventArgs e)
        {
            showedPage.comboboxScreenNumber.Items.Clear();
            if (es._examsessioncontrol._currentSessionStudents != null)
            {
                foreach (Student s in es._examsessioncontrol._currentSessionStudents)
                {
                    if (s.Status.ToString() == "Połączono")
                    {
                        showedPage.comboboxScreenNumber.Items.Add(s.Ip.ToString() + "-"+s.User);
                        showedPage.comboboxScreenNumber.SelectedIndex = 0;
                    }
                }
            }
            if(showedPage.comboboxScreenNumber.Items.Count>0)
            {
                showedPage.buttonGetCurrentScreen.IsEnabled = true;
                showedPage.buttonGetLastScreen.IsEnabled = true;
            }
            else
            {
                showedPage.buttonGetCurrentScreen.IsEnabled = false;
                showedPage.buttonGetLastScreen.IsEnabled = false;
            }

            firstFrame.Navigate(showedPage);
        }
        private void MenuItemPages_Click(object sender, RoutedEventArgs e)
        {
            firstFrame.Navigate(app);
        }

        private void MenuItemWorkstations_Click(object sender, RoutedEventArgs e)
        {
            firstFrame.Navigate(wp);
        }
        private void MenuItemExamSession_Click(object sender, RoutedEventArgs e)
        {
            using (ClientServiceClient client = new ClientServiceClient())
            {
                es._examsessioncontrol.GenereteSelects(client, es.comboBoxAcceptablePagesGroup, es.comboBoxWorkstationGroups);
            }
            firstFrame.Navigate(es);
            
        }


        #endregion


    }
}
