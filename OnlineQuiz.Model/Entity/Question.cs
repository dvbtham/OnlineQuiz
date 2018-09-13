namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            AdvancedExamResultDetails = new HashSet<AdvancedExamResultDetail>();
            BasicExamResultDetails = new HashSet<BasicExamResultDetail>();
            ExaminationQuestions = new HashSet<ExaminationQuestion>();
        }

        public Guid ID { get; set; }

        public Guid? QuestionModuleID { get; set; }

        public Guid? QuestionClassificationID { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancedExamResultDetail> AdvancedExamResultDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BasicExamResultDetail> BasicExamResultDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExaminationQuestion> ExaminationQuestions { get; set; }

        public virtual QuestionModule QuestionModule { get; set; }

        public virtual QuestionClassification QuestionClassification { get; set; }
    }
}
