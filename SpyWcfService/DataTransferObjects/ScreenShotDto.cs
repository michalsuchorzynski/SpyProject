using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace SpyWcfService.DataTransferObjects
{
    [DataContract(Name ="ScreenShot", Namespace ="ClientService.Types")]
    class ScreenShotDto
    {
        [DataMember]
        public string data;

    }
}
