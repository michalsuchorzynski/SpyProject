﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SpyEntities : DbContext
    {
        public SpyEntities()
            : base("name=SpyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ScreenShot> ScreenShots { get; set; }
        public virtual DbSet<AcceptablePage> AcceptablePages { get; set; }
        public virtual DbSet<AcceptablePagesGroup> AcceptablePagesGroups { get; set; }
        public virtual DbSet<ExamSession> ExamSessions { get; set; }
        public virtual DbSet<PagesForGroup> PagesForGroups { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<WorkStation> WorkStations { get; set; }
        public virtual DbSet<WorkStationsForGroup> WorkStationsForGroups { get; set; }
        public virtual DbSet<WorkStationsGroup> WorkStationsGroups { get; set; }
        public virtual DbSet<ClientUser> ClientUsers { get; set; }
        public virtual DbSet<ClientUserForWorkstation> ClientUserForWorkstations { get; set; }
        public virtual DbSet<ScreenShotsForWorkstation> ScreenShotsForWorkstations { get; set; }
    }
}
