//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedicWebApp.DB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MedicDB : DbContext
    {
        public MedicDB()
            : base("name=MedicDB")
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblDoctor> tblDoctor { get; set; }
        public virtual DbSet<tblMedDistrict> tblMedDistrict { get; set; }
        public virtual DbSet<tblSpeciality> tblSpeciality { get; set; }
        public virtual DbSet<tblRoom> tblRoom { get; set; }
        public virtual DbSet<tblPatient> tblPatient { get; set; }
    }
}
