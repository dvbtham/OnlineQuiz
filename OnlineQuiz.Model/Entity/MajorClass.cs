namespace OnlineQuiz.Model.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MajorClass")]
    public partial class MajorClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MajorClass()
        {
            Students = new HashSet<Student>();
        }

        public Guid ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Course { get; set; }

        public int? Quantity { get; set; }

        public Guid? FacultyID { get; set; }

        public virtual Faculty Faculty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
