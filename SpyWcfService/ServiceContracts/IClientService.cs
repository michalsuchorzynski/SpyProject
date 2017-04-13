using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using SpyWcfService.Messages;

namespace SpyWcfService.ServiceContracts
{
    [ServiceContract]
    public interface IClientService
    {
        [OperationContract]
        bool SaveScreenShotToDB(ClientRequest request);
        [OperationContract]
        byte[] GetScreenFromDB();
        [OperationContract]
        List<AcceptablePagesGroup> GetPagesGroupFromDB();
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageFromDB();
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageForGroupFromDB(int GroupId);
    }
}
