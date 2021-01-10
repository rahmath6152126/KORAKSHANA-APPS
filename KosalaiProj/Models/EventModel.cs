using System;
using System.ComponentModel.DataAnnotations;

namespace GORAKSHANA.Models
{
    public class EventModel
    {
        [Required]
        public string evt_type { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [
            DisplayFormat(
                ApplyFormatInEditMode = true,
                DataFormatString = "{0:dd MM yyyy}")
        ]
        public DateTime event_date { get; set; }

        [Required]
        public string tamil_yr { get; set; }

        [Required]
        public string tamil_month { get; set; }

        public string starname { get; set; }

        public string patcamname { get; set; }

        public string tithiname { get; set; }

        public string gotraname { get; set; }
    }
}
