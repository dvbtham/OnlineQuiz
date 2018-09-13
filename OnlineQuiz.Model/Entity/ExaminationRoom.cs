namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExaminationRoom")]
    public partial class ExaminationRoom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExaminationRoom()
        {
            ExamScheduleAdvanceds = new HashSet<ExamScheduleAdvanced>();
            ExamScheduleBasics = new HashSet<ExamScheduleBasic>();
        }

        public Guid ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamScheduleAdvanced> ExamScheduleAdvanceds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamScheduleBasic> ExamScheduleBasics { get; set; }
    }
}
