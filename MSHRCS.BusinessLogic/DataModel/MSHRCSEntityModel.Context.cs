﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MSHRCS.BusinessLogic.DataModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MSHRCSchedulerContext : DbContext
    {
        public MSHRCSchedulerContext()
            : base("name=MSHRCSchedulerContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AcademicDiscipline> AcademicDisciplines { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<GDCabinet> GDCabinets { get; set; }
        public DbSet<GDTeacher> GDTeachers { get; set; }
        public DbSet<GroupDiscipline> GroupDisciplines { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
    }
}