using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyAdminApplication.ServiceReference1;
using System.Windows.Controls;
using System.Threading;
using System.Net.Sockets;
using System.IO;
namespace SpyAdminApplication.Control
{
    public class ExamSessionControl
    {
        public List<AcceptablePagesGroup> _acceptablePagesGroups;
        public List<WorkStationsGroup> _workstationsGroups;
        public List<AcceptablePage> _acceptablePages;
        public List<WorkStation> _workstations;

        public List<WorkStation> _currentSessionWorkstations;
        public List<Student> _currentSessionStudents;
        public int _currentSessionPagesGroupId;

        public Thread _examThread;
        public int _currentExamSessionID { get; set; }

        public DataGrid _currentdataGridStudents;
        Action<System.Collections.IEnumerable> updateAction;
        public ExamSessionControl()
        {
            _acceptablePagesGroups = new List<AcceptablePagesGroup>();
            _workstationsGroups = new List<WorkStationsGroup>();
            _examThread = new Thread(new ThreadStart(ExamWorkstations));
        }

        public void StartExamSession(ClientServiceClient client ,AcceptablePagesGroup acceptablePagesgroups, WorkStationsGroup workstationsgroups, DataGrid dataGridStudents)
        {
            _currentSessionWorkstations = new List<WorkStation>();
            _currentSessionStudents = new List<Student>();
            _currentSessionPagesGroupId = acceptablePagesgroups.AcceptablePagesGroupId;
            _currentExamSessionID = client.CreateExamSession(acceptablePagesgroups, workstationsgroups);
            foreach (var workstation in client.GetWorkstationsForGroupFromDB(workstationsgroups.WorkStationsGroupId))
            {
                _currentSessionWorkstations.Add(workstation);
                _currentSessionStudents.Add(new Student
                {
                    Ip = workstation.IP,
                    User = "Nowy",
                    Status = "Nowy",
                    WrongPageCount = "0"
                });
            }
            dataGridStudents.ItemsSource = null;
            dataGridStudents.ItemsSource = _currentSessionStudents;
            ConnectExamWorkstations(dataGridStudents);
            _examThread.Start();
                        
        }

        private void ConnectExamWorkstations(DataGrid dataGridStudents)
        {
            _currentdataGridStudents = dataGridStudents;
            foreach (WorkStation station in _currentSessionWorkstations)
            {
                var wIp = station.IP;
                try
                {
                    foreach (Student s in _currentSessionStudents)
                    {
                        if (s.Ip == station.IP)
                        {
                            s.Status = "Łączenie...";

                        }
                    }
                    dataGridStudents.ItemsSource = null;
                    dataGridStudents.ItemsSource = _currentSessionStudents;
                    var response = SendCMD(wIp, "start|"+Convert.ToInt32(_currentExamSessionID));
                    foreach(Student s in _currentSessionStudents)
                    {
                        if(s.Ip==station.IP)
                        {
                            s.Status = "Połączono";
                            s.User = response;
                            
                        }
                    }
                }
                catch(Exception e)
                {
                    foreach (Student s in _currentSessionStudents)
                    {
                        if (s.Ip == station.IP)
                        {
                            s.Status = "Błąd połaczenia";

                        }
                    }
                }
                dataGridStudents.ItemsSource = null;
                dataGridStudents.ItemsSource = _currentSessionStudents;
            }
        }
        public void ExamWorkstations()
        {
            while(true)
            {
                Thread.Sleep(2000);
                foreach (WorkStation station in _currentSessionWorkstations)
                {
                    var wIp = station.IP;

                    using (ClientServiceClient client = new ClientServiceClient())
                    {
                        var selectedUser = client.GetUserForWorkstation(_currentExamSessionID, station.WorkStationId);
                        var errorsforuser = client.GetOffenceScreenId(selectedUser);
                        foreach(Student s in _currentSessionStudents )
                        {
                            if(s.Ip==wIp)
                            {
                                s.WrongPageCount = errorsforuser.Length.ToString();
                            }
                        }                        
                    }

                    _currentdataGridStudents.Dispatcher.Invoke(() => _currentdataGridStudents.ItemsSource = null);
                    _currentdataGridStudents.Dispatcher.Invoke(() => _currentdataGridStudents.ItemsSource = _currentSessionStudents);
                }
            }
        }
        private string SendCMD(string wIp, string cmd)
        {
            string request = "";
            TcpClient tcpclnt = new TcpClient();
            tcpclnt.Connect(wIp, 8001);
            
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


        public bool GenereteSelects(ClientServiceClient client, ComboBox acceptablePagesgroups, ComboBox workstationsgroups)
        {
            GenerateAcceptablePagesGroups(client, acceptablePagesgroups);
            GenerateAcceptableworkstationsGroups(client, workstationsgroups);
            return true;
        }
        public void GenerateAcceptablePagesGroups(ClientServiceClient client, ComboBox allgroups)
        {
            allgroups.Items.Clear();
            _acceptablePagesGroups.Clear();
            foreach (var group in client.GetPagesGroupFromDB())
            {
                _acceptablePagesGroups.Add(group);
            }
            foreach (var group in _acceptablePagesGroups)
            {               
                allgroups.Items.Add(group.Name);
            }
        }
        public void GenerateAcceptableworkstationsGroups(ClientServiceClient client, ComboBox allgroups)
        {
            allgroups.Items.Clear();
            _workstationsGroups.Clear();
            foreach (var group in client.GetWorkstationsGroupFromDB())
            {
                _workstationsGroups.Add(group);
            }
            foreach (var group in _workstationsGroups)
            {                
                allgroups.Items.Add(group.Name);
            }
        }
        
        public class Student
        {
            public string Ip { get; set; }
            public string User { get; set; }
            public string WrongPageCount { get; set; }
            public string Status { get; set; }
        }
    }
}
