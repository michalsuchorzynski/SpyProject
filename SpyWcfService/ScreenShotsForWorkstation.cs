//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SpyWcfService
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScreenShotsForWorkstation
    {
        public int ScreenShotsForWorkstationsId { get; set; }
        public Nullable<int> ScreenShotId { get; set; }
        public Nullable<int> WorkStationId { get; set; }
        public Nullable<int> ClientUserId { get; set; }
    
        public virtual ScreenShot ScreenShot { get; set; }
        public virtual WorkStation WorkStation { get; set; }
        public virtual ClientUser ClientUser { get; set; }
    }
}
