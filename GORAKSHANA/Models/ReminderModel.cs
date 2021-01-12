using System;
using System.ComponentModel.DataAnnotations;

namespace Sharedmodels
{
    public class ReminderModel
    {
        public int Slno { get; set; }
        public string engDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd MM yyyy}")]
        public DateTime tamilDate { get; set; }
    }
}
