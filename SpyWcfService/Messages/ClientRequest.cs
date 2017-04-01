using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SpyWcfService.Messages
{
    [DataContract]
    public class ClientRequest
    {
        [DataMember]
        public string _username { get; set; }
        [DataMember]
        public DateTime _scrrenDate { get; set; }
        [DataMember]
        public string _scrrenName { get; set; }
        [DataMember]
        public byte[] _data { get; set; }
    }
}
