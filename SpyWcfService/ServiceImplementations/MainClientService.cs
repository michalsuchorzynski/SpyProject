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
        

        #endregion
    }
}
