namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("QuestionModule")]
    public partial class QuestionModule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionModule()
        {
            AdvancedModuleRegistrations = new HashSet<AdvancedModuleRegistration>();
            ExaminationModules = new HashSet<ExaminationModule>();
            Questions = new HashSet<Question>();
        }

        public Guid ID { get; set; }

        public Guid? InformationTechnologySkillID { get; set; }

        [StringLength(50)]
        public string IDQuestionModule { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancedModuleRegistration> AdvancedModuleRegistrations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExaminationModule> ExaminationModules { get; set; }

        public virtual InformationTechnologySkill InformationTechnologySkill { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
