namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamScheduleAdvanced")]
    public partial class ExamScheduleAdvanced
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExamScheduleAdvanced()
        {
            Examinees = new HashSet<Examinee>();
        }

        public Guid ID { get; set; }

        public Guid? AdvancedModuleRegistrationID { get; set; }

        public Guid? ExamPeriodID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExaminationDate { get; set; }

        public Guid? StartEndTimeID { get; set; }

        public Guid? ExaminationRoomID { get; set; }

        public virtual AdvancedModuleRegistration AdvancedModuleRegistration { get; set; }

        public virtual ExaminationRoom ExaminationRoom { get; set; }

        public virtual ExamPeriod ExamPeriod { get; set; }

        public virtual StartEndTime StartEndTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Examinee> Examinees { get; set; }
    }
}
