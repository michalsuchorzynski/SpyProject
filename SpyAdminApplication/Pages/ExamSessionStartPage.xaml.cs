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
using SpyAdminApplication.Windows;
using SpyAdminApplication.ServiceReference1;
using SpyAdminApplication.Pages;
using SpyAdminApplication.Control;
using System.Threading;

namespace SpyAdminApplication.Pages
{
    /// <summary>
    /// Interaction logic for ExamSessionStartPage.xaml
    /// </summary>
    public partial class ExamSessionStartPage : Page
    {
        public ExamSessionControl _examsessioncontrol;
        public ExamSessionStartPage()
        {            
            InitializeComponent();
            _examsessioncontrol = new ExamSessionControl();

            using (ClientServiceClient client = new ClientServiceClient())
            {
                _examsessioncontrol.GenereteSelects(client, comboBoxAcceptablePagesGroup, comboBoxWorkstationGroups);
            }
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            var selectedpagegroup = _examsessioncontrol._acceptablePagesGroups.FirstOrDefault(x => x.Name == (this.comboBoxAcceptablePagesGroup.SelectedValue.ToString()));
            var selectedworkgroup = _examsessioncontrol._workstationsGroups.FirstOrDefault(x => x.Name == (this.comboBoxWorkstationGroups.SelectedValue.ToString()));
            using (ClientServiceClient client = new ClientServiceClient())
            {
                client.CreateExamSession(selectedpagegroup, selectedworkgroup);
                _examsessioncontrol.StartExamSession(client, selectedpagegroup, selectedworkgroup, dataGridStudents);
            }
           // dataGridStudents.ItemsSource = _examsessioncontrol._currentSessionStudents;
            


        }
    }
}
