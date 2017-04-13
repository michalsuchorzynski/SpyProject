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
            var a =  context.ScreenShots.ToList();
            return a[3].Data;
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
    }
}
