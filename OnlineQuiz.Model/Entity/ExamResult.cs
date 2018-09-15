namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamResult")]
    public partial class ExamResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamResult()
        {
            ExamResultDetails = new HashSet<ExamResultDetail>();
        }

        public Guid ID { get; set; }

        public Guid? ExamineeID { get; set; }

        [StringLength(50)]
        public string IDExaminee { get; set; }

        public Guid? ExaminationID { get; set; }

        public int? ExamCode { get; set; }

        public DateTime? DateOfTest { get; set; }

        public int? Duration { get; set; }

        public int? TrueQuestion { get; set; }

        public double? Point { get; set; }

        public virtual Examination Examination { get; set; }

        public virtual Examinee Examinee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamResultDetail> ExamResultDetails { get; set; }
    }
}
