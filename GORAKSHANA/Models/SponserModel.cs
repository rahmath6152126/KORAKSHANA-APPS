using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sharedmodels;

namespace GORAKSHANA.Models
{
    public class SponserModel : BaseModel
    {

        public string code { get; set; }
        [BsonElement("firstName")]
        [Required(ErrorMessage = "Shoud be enter first name")]
        [StringLength(100)]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public DateTime regDate { get; set; } = DateTime.Now;
        public string pri_contact_no { get; set; }
        public string photo_path { get; set; }
        public string contact_no { get; set; }
        public string sponser_Type { get; set; }
        public string relation { get; set; }
        public string toSponsername { get; set; }
        public string toSponserContactno { get; set; }
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
        public string refType { get; set; }
        public string refname { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string paymentMode { get; set; }
        public string transactionno { get; set; }
        public string transactionname { get; set; }
        public string amount { get; set; }
        public List<ReminderModel> reminder { get; set; }




        public SponserModel()
        {
            reminder = new List<ReminderModel>();
        }
    }
}
