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
    }
}
