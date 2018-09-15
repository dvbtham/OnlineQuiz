namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Registration")]
    public partial class Registration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Registration()
        {
            AdvancedModuleRegistrations = new HashSet<AdvancedModuleRegistration>();
            IDExamineeRegistrations = new HashSet<IDExamineeRegistration>();
        }

        public Guid ID { get; set; }

        public Guid? ExamineeID { get; set; }

        public Guid? InformationTechnologySkillID { get; set; }

        public Guid? ExamPeriodID { get; set; }

        public DateTime? RegistrationDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdvancedModuleRegistration> AdvancedModuleRegistrations { get; set; }

        public virtual Examinee Examinee { get; set; }

        public virtual ExamPeriod ExamPeriod { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IDExamineeRegistration> IDExamineeRegistrations { get; set; }

        public virtual InformationTechnologySkill InformationTechnologySkill { get; set; }
    }
}
