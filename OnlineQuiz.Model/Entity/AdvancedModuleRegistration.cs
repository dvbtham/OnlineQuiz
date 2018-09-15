namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdvancedModuleRegistration")]
    public partial class AdvancedModuleRegistration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdvancedModuleRegistration()
        {
            ExamScheduleAdvanceds = new HashSet<ExamScheduleAdvanced>();
        }

        public Guid ID { get; set; }

        public Guid? RegistrationID { get; set; }

        public Guid? QuestionModuleID { get; set; }

        [StringLength(50)]
        public string IDExaminee { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public virtual QuestionModule QuestionModule { get; set; }

        public virtual Registration Registration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamScheduleAdvanced> ExamScheduleAdvanceds { get; set; }
    }
}
