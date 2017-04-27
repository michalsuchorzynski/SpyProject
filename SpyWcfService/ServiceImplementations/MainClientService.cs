using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpyWcfService.Messages;
using SpyWcfService.ServiceContracts;


namespace SpyWcfService.ServiceImplementations
{
    public class MainClientService : IClientService
    {
        private SpyEntities context;
        public MainClientService()
        {
            context = new SpyEntities(); 

        }

        public byte[] GetScreenFromDB()
        {
            var sccreanShotList =  context.ScreenShots.ToList();
            return sccreanShotList[3].Data;
        }
        
        public int GetScreenCountFromDB()
        {
            return context.ScreenShots.Count();
        }
        public bool SaveScreenShotToDB(ClientRequest request)
        {
            try
            {
                ScreenShot screentosave = new ScreenShot(){
                    Data = request._data,
                    ScreenDate = request._scrrenDate

                };
                context.ScreenShots.Add(screentosave);
                context.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }

        public byte[] GetScreenByIdFromDB(int id)
        {
            var sccreanShotList = context.ScreenShots.ToList();
            return sccreanShotList[id].Data;
        }

        #region GetFromDB
        public List<AcceptablePagesGroup> GetPagesGroupFromDB()
        {
            List<AcceptablePagesGroup> list = new List<AcceptablePagesGroup>();
            foreach (AcceptablePagesGroup group in context.AcceptablePagesGroups)
            {
                list.Add(new AcceptablePagesGroup()
                {
                    AcceptablePagesGroupId = group.AcceptablePagesGroupId,
                    Name = group.Name
                    
                });
            }
            return list;
        }
        public List<AcceptablePage> GetAcceptablePageFromDB()
        {
            List<AcceptablePage> list = new List<AcceptablePage>();
            foreach (AcceptablePage page in context.AcceptablePages)
            {
                list.Add(new AcceptablePage()
                {
                    AcceptablePageId=page.AcceptablePageId,
                    Url = page.Url,
                    HeadKeyWords = page.HeadKeyWords
                });
            }
            return list;
        }
        public List<AcceptablePage> GetAcceptablePageForGroupFromDB(int GroupId)
        {
            List<AcceptablePage> list = new List<AcceptablePage>();
            var query = from gr in context.PagesForGroups
                        where gr.AcceptablePagesGroupId == GroupId
                        select gr.AcceptablePage;            
            foreach(AcceptablePage page in query.ToList())
            {
                list.Add(new AcceptablePage()
                {
                    AcceptablePageId= page.AcceptablePageId,
                    Url = page.Url,
                    HeadKeyWords = page.HeadKeyWords
                });
            }
            return list;            
        }

        public List<WorkStationsGroup> GetWorkstationsGroupFromDB()
        {
            List<WorkStationsGroup> list = new List<WorkStationsGroup>();
            foreach (WorkStationsGroup group in context.WorkStationsGroups)
            {
                list.Add(new WorkStationsGroup()
                {
                    WorkStationsGroupId = group.WorkStationsGroupId,
                    Name = group.Name

                });
            }
            return list;
        }
        public List<WorkStation> GetWorkstationsFromDB()
        {
            List<WorkStation> list = new List<WorkStation>();
            foreach (WorkStation workStation in context.WorkStations)
            {
                list.Add(new WorkStation()
                {
                    WorkStationId = workStation.WorkStationId,
                    IP = workStation.IP,
                });
            }
            return list;
        }
        public List<WorkStation> GetWorkstationsForGroupFromDB(int GroupId)
        {
            List<WorkStation> list = new List<WorkStation>();
            var query = from gr in context.WorkStationsForGroups
                        where gr.WorkStationsGroupId == GroupId
                        select gr.WorkStation;
            foreach (WorkStation workstation in query.ToList())
            {
                list.Add(new WorkStation()
                {
                    WorkStationId = workstation.WorkStationId,
                    IP = workstation.IP,
                });
            }
            return list;
        }
        #endregion

        #region AddFromDB
        public bool AddPagesGroup(AcceptablePagesGroup group)
        {
            context.AcceptablePagesGroups.Add(group);
            context.SaveChanges();
            return true;
        }
        public bool AddAcceptablePage(AcceptablePage page)
        {
            context.AcceptablePages.Add(page);
            context.SaveChanges();
            return true;
        }
        public bool AddAcceptablePageForGroup(AcceptablePage page, AcceptablePagesGroup group)
        {
            PagesForGroup pagegroup = new PagesForGroup()
            {
                AcceptablePageId = page.AcceptablePageId,
                AcceptablePagesGroupId = group.AcceptablePagesGroupId,
            };
            context.PagesForGroups.Add(pagegroup);
            context.SaveChanges();
            return true;
        }

        public bool AddWorkstationsGroup(WorkStationsGroup group)
        {
            context.WorkStationsGroups.Add(group);
            context.SaveChanges();
            return true;
        }
        public bool AddWorkstation(WorkStation workstation)
        {
            context.WorkStations.Add(workstation);
            context.SaveChanges();
            return true;
        }
        public bool AddWorkstationForGroup(WorkStation workstation, WorkStationsGroup group)
        {
            WorkStationsForGroup workstationgroup = new WorkStationsForGroup()
            {
                WorkStationId = workstation.WorkStationId,
                WorkStationsGroupId = group.WorkStationsGroupId,
            };
            context.WorkStationsForGroups.Add(workstationgroup);
            context.SaveChanges();
            return true;
        }

        #endregion

        #region DeleteFromDB
        public bool DeletePagesGroup(AcceptablePagesGroup group)
        {
            var toDelete = context.AcceptablePagesGroups.FirstOrDefault(x => x.AcceptablePagesGroupId == group.AcceptablePagesGroupId);
            foreach (PagesForGroup pageingroup in context.PagesForGroups.Where(x => x.AcceptablePagesGroupId == toDelete.AcceptablePagesGroupId))
                context.PagesForGroups.Remove(pageingroup);
            context.AcceptablePagesGroups.Remove(toDelete);
            context.SaveChanges();
            return true;
        }
        public bool DeleteAcceptablePage(AcceptablePage page)
        {
            var toDelete = context.AcceptablePages.FirstOrDefault(x => x.AcceptablePageId == page.AcceptablePageId);
            foreach (PagesForGroup pageingroup in context.PagesForGroups.Where(x => x.AcceptablePageId == toDelete.AcceptablePageId))
                context.PagesForGroups.Remove(pageingroup);
            context.AcceptablePages.Remove(toDelete);
            context.SaveChanges();
            return true;
        }
        public bool DeleteAcceptablePageForGroup(AcceptablePage page, AcceptablePagesGroup group)
        {
            var toDelete = context.PagesForGroups.FirstOrDefault(x => x.AcceptablePageId == page.AcceptablePageId && x.AcceptablePagesGroupId == group.AcceptablePagesGroupId);
            context.PagesForGroups.Remove(toDelete);
            context.SaveChanges();
            return true;
        }

        public bool DeleteWorkstationGroup(WorkStationsGroup group)
        {
            var toDelete = context.WorkStationsGroups.FirstOrDefault(x => x.WorkStationsGroupId == group.WorkStationsGroupId);
            foreach (WorkStationsForGroup workstationsingroup in context.WorkStationsForGroups.Where(x => x.WorkStationsGroupId == toDelete.WorkStationsGroupId))
                context.WorkStationsForGroups.Remove(workstationsingroup);
            context.WorkStationsGroups.Remove(toDelete);
            context.SaveChanges();
            return true;
        }
        public bool DeleteWorkstation(WorkStation workstation)
        {
            var toDelete = context.WorkStations.FirstOrDefault(x => x.WorkStationId == workstation.WorkStationId);
            foreach (WorkStationsForGroup workstationingroup in context.WorkStationsForGroups.Where(x => x.WorkStationId == toDelete.WorkStationId))
                context.WorkStationsForGroups.Remove(workstationingroup);
            context.WorkStations.Remove(toDelete);
            context.SaveChanges();
            return true;
        }
        public bool DeleteWorkstationsForGroup(WorkStation workstation, WorkStationsGroup group)
        {
            var toDelete = context.WorkStationsForGroups.FirstOrDefault(x => x.WorkStationId == workstation.WorkStationId && x.WorkStationsGroupId == group.WorkStationsGroupId);
            context.WorkStationsForGroups.Remove(toDelete);
            context.SaveChanges();
            return true;
        }

        #endregion
    }
}
