namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Examination")]
    public partial class Examination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Examination()
        {
            ExaminationModules = new HashSet<ExaminationModule>();
            ExaminationQuestions = new HashSet<ExaminationQuestion>();
            ExamResults = new HashSet<ExamResult>();
            ExamPeriods = new HashSet<ExamPeriod>();
        }

        public Guid ID { get; set; }

        public Guid? InformationTechnologySkillID { get; set; }

        public int? Duration { get; set; }

        public int? QuestionNumber { get; set; }

        public int? TestNumber { get; set; }

        public int? ExellentNumber { get; set; }

        public int? VeryGoodNumber { get; set; }

        public int? GoodNumber { get; set; }

        public int? AverageNumber { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExaminationModule> ExaminationModules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExaminationQuestion> ExaminationQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamResult> ExamResults { get; set; }

        public virtual InformationTechnologySkill InformationTechnologySkill { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamPeriod> ExamPeriods { get; set; }
    }
}
