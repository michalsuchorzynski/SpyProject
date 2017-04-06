using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SpyWcfService.Messages
{
    
    [DataContract]
    public class AcceptablePagesResponse
    {
        [DataMember]
        public List<AcceptablePage> AcceptablePages { get; set; }
    }
}
