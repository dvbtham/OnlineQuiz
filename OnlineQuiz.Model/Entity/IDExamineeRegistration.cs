namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IDExamineeRegistration")]
    public partial class IDExamineeRegistration
    {
        [Key]
        [Column(Order = 0)]
        public Guid RegistrationID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string IDExaminee { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        public bool? Status { get; set; }

        public virtual Registration Registration { get; set; }
    }
}
