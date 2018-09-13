namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Manager")]
    public partial class Manager
    {
        public Guid ID { get; set; }

        [StringLength(100)]
        public string UserName { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
