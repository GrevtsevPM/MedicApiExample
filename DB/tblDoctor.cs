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
    using System.Collections.Generic;
    
    public partial class tblDoctor
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public Nullable<int> Room { get; set; }
        public Nullable<int> Speciality { get; set; }
        public Nullable<int> MedDistrict { get; set; }
    
        public virtual tblMedDistrict tblMedDistrict { get; set; }
        public virtual tblSpeciality tblSpeciality { get; set; }
        public virtual tblRoom tblRoom { get; set; }
    }
}
