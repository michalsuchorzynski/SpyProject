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
        int SaveScreenShotToDB(ClientRequest request);
        [OperationContract]
        byte[] GetScreenFromDB();
        [OperationContract]
        byte[] GetScreenByIdFromDB(int id);       
        [OperationContract]
        int GetScreenCountFromDB();

        [OperationContract]
        int CreateExamSession(AcceptablePagesGroup pagegorup, WorkStationsGroup worksgroup);


        #region GetFromDB
        [OperationContract]
        List<AcceptablePagesGroup> GetPagesGroupFromDB();
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageFromDB();
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageForGroupFromDB(int groupId);
        [OperationContract]
        List<AcceptablePage> GetAcceptablePageForExamFromDB(int ExamId);
        [OperationContract]
        List<WorkStationsGroup> GetWorkstationsGroupFromDB();
        [OperationContract]
        List<WorkStation> GetWorkstationsFromDB();
        [OperationContract]
        List<WorkStation> GetWorkstationsForGroupFromDB(int groupId);
        #endregion

        #region UpdateToDb
        [OperationContract]
        bool AddPagesGroup(AcceptablePagesGroup group);
        [OperationContract]
        bool AddAcceptablePage(AcceptablePage page);
        [OperationContract]
        bool AddAcceptablePageForGroup(AcceptablePage page, AcceptablePagesGroup group);
        [OperationContract]
        bool AddWorkstationsGroup(WorkStationsGroup group);
        [OperationContract]
        bool AddWorkstation(WorkStation workstation);
        [OperationContract]
        bool AddWorkstationForGroup(WorkStation workstation, WorkStationsGroup group);
        #endregion

        #region DeleteFromDB
        [OperationContract]
        bool DeletePagesGroup(AcceptablePagesGroup group);
        [OperationContract]
        bool DeleteAcceptablePage(AcceptablePage page);
        [OperationContract]
        bool DeleteAcceptablePageForGroup(AcceptablePage page, AcceptablePagesGroup group);
        [OperationContract]
        bool DeleteWorkstationGroup(WorkStationsGroup group);
        [OperationContract]
        bool DeleteWorkstation(WorkStation workstation);
        [OperationContract]
        bool DeleteWorkstationsForGroup(WorkStation workstation, WorkStationsGroup group);
        #endregion
    }
}
