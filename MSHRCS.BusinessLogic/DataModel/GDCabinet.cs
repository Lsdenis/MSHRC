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
    
    public partial class GDCabinet
    {
        public int Id { get; set; }
        public int GDId { get; set; }
        public int CabinetId { get; set; }
        public int LessonId { get; set; }
        public Nullable<int> SubstituteDGId { get; set; }
        public System.DateTime Date { get; set; }
        public int LessonTypeId { get; set; }
    
        public virtual Cabinet Cabinet { get; set; }
        public virtual GroupDiscipline GroupDiscipline { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual LessonType LessonType { get; set; }
        public virtual GroupDiscipline SubstitutedGroupDiscipline { get; set; }
    }
}
