//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        public Teacher()
        {
            this.GDCabinets = new HashSet<GDCabinet>();
            this.GDTeachers = new HashSet<GDTeacher>();
            this.Groups = new HashSet<Group>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    
        public virtual ICollection<GDCabinet> GDCabinets { get; set; }
        public virtual ICollection<GDTeacher> GDTeachers { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}
