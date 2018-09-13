namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdvancedExamResult")]
    public partial class AdvancedExamResult
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdvancedExamResult()
        {
            AdvancedExamResultDetails = new HashSet<AdvancedExamResultDetail>();
        }

        public Guid ID { get; set; }

        public Guid? ExamPeriodID { get; set; }

        public Guid? AdvancedModuleRegistrationID { get; set; }

        public Guid? ExaminationQuestionID { get; set; }

        public Guid? StartEndTimeID { get; set; }

        public Guid? ExamineeID { get; set; }

        public Guid? IDxaminee { get; set; }

        public int? ExamCode { get; set; }

        public int? Duration { get; set; }

        public int? TrueQuestion { get; set; }

        public double? Point { get; set; }

        public virtual AdvancedModuleRegistration AdvancedModuleRegistration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancedExamResultDetail> AdvancedExamResultDetails { get; set; }

        public virtual ExaminationQuestion ExaminationQuestion { get; set; }

        public virtual Examinee Examinee { get; set; }

        public virtual ExamPeriod ExamPeriod { get; set; }

        public virtual StartEndTime StartEndTime { get; set; }
    }
}
