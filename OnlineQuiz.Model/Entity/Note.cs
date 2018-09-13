namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Note")]
    public partial class Note
    {
        public Guid ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool? State { get; set; }
    }
}
