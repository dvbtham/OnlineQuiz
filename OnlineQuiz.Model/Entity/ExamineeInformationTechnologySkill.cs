namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ExamineeInformationTechnologySkill")]
    public partial class ExamineeInformationTechnologySkill
    {
        [Key]
        [Column(Order = 0)]
        public Guid ExamineeID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid InformationTechnologySkillID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfCertification { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public virtual Examinee Examinee { get; set; }

        public virtual InformationTechnologySkill InformationTechnologySkill { get; set; }
    }
}
