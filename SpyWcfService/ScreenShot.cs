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
    
    public partial class ScreenShot
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScreenShot()
        {
            this.ScreenShotsForWorkstations = new HashSet<ScreenShotsForWorkstation>();
        }
    
        public int ScreenShotId { get; set; }
        public byte[] Data { get; set; }
        public Nullable<System.DateTime> ScreenDate { get; set; }
        public Nullable<int> isOfense { get; set; }
        public string Url { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenShotsForWorkstation> ScreenShotsForWorkstations { get; set; }
    }
}
