namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StartEndTime")]
    public partial class StartEndTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StartEndTime()
        {
            AdvancedExamResults = new HashSet<AdvancedExamResult>();
            BasicExamResults = new HashSet<BasicExamResult>();
            ExamScheduleAdvanceds = new HashSet<ExamScheduleAdvanced>();
            ExamScheduleBasics = new HashSet<ExamScheduleBasic>();
        }

        public Guid ID { get; set; }

        [StringLength(100)]
        public string TimePeriod { get; set; }

        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancedExamResult> AdvancedExamResults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasicExamResult> BasicExamResults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamScheduleAdvanced> ExamScheduleAdvanceds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamScheduleBasic> ExamScheduleBasics { get; set; }
    }
}
