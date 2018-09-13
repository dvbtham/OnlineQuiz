namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExaminationQuestion")]
    public partial class ExaminationQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExaminationQuestion()
        {
            AdvancedExamResults = new HashSet<AdvancedExamResult>();
            BasicExamResults = new HashSet<BasicExamResult>();
        }

        public Guid ID { get; set; }

        public Guid? ExaminationID { get; set; }

        public int? ExamCode { get; set; }

        public Guid? QuestionID { get; set; }

        [StringLength(450)]
        public string QuestionContent { get; set; }

        [StringLength(255)]
        public string AAnswer { get; set; }

        [StringLength(255)]
        public string BAnswer { get; set; }

        [StringLength(255)]
        public string CAnswer { get; set; }

        [StringLength(255)]
        public string DAnswer { get; set; }

        [StringLength(1)]
        public string Answer { get; set; }

        [StringLength(255)]
        public string ResultAnswer { get; set; }

        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancedExamResult> AdvancedExamResults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasicExamResult> BasicExamResults { get; set; }

        public virtual Examination Examination { get; set; }

        public virtual Question Question { get; set; }
    }
}
