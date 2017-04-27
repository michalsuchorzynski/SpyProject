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
        byte[] GetScreenByIdFromDB(int id);
       
        [OperationContract]
        int GetScreenCountFromDB();

        #region GetFromDB
        [OperationContract]
        List<AcceptablePagesGroup> GetPagesGroupFromDB();
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageFromDB();
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageForGroupFromDB(int groupId);
        [OperationContract]
        #endregion


        #region UpdateToDb
        bool AddPagesGroup(AcceptablePagesGroup group);
        [OperationContract]
        bool AddAcceptablePage(AcceptablePage page);
        [OperationContract]
        bool AddAcceptablePageForGroup(AcceptablePage page, AcceptablePagesGroup group);
        #endregion

        #region DeleteFromDB
        [OperationContract]
        bool DeletePagesGroup(AcceptablePagesGroup group);
        [OperationContract]
        bool DeleteAcceptablePage(AcceptablePage page);
        [OperationContract]
        bool DeleteAcceptablePageForGroup(AcceptablePage page, AcceptablePagesGroup group);
        #endregion
    }
}
