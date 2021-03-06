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
    
    public partial class ClientUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientUser()
        {
            this.ClientUserForWorkstations = new HashSet<ClientUserForWorkstation>();
            this.ScreenShotsForWorkstations = new HashSet<ScreenShotsForWorkstation>();
        }
    
        public int ClientUserId { get; set; }
        public string UserLogin { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public Nullable<int> ExamSessionId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientUserForWorkstation> ClientUserForWorkstations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScreenShotsForWorkstation> ScreenShotsForWorkstations { get; set; }
        public virtual ExamSession ExamSession { get; set; }
    }
}
