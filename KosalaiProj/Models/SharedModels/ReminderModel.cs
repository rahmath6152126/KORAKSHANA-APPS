using System;
using System.ComponentModel.DataAnnotations;

namespace Sharedmodels
{
    public class ReminderModel
    {
        public int Slno { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [
            DisplayFormat(
                ApplyFormatInEditMode = true,
                DataFormatString = "{0:dd MM yyyy}")
        ]
        public DateTime eng_date { get; set; }

        public string tamil_date { get; set; }
    }
}
