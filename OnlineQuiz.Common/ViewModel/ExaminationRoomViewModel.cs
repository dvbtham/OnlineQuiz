using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Common.ViewModel
{
    public class ExaminationRoomViewModel
    {
        public Guid ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }
    }
}
