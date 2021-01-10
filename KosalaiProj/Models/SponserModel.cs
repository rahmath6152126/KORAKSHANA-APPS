using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GORAKSHANA.Models
{
    public class SponserModel : DataBaseModel
    {


        public string code { get; set; }

        [BsonElement("firstName")]
        [Required(ErrorMessage = "Shoud be enter first name")]
        [StringLength(100)]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string pri_contact_no { get; set; }

        public string contact_no { get; set; }

        public string sponser_Type { get; set; }

        public string toSponsername { get; set; }

        public string toSponserContactno { get; set; }

        public string photo_path { get; set; }

        public EventModel event_data { get; set; }

        public List<ReminderModel> reminder { get; set; }

        public SponserModel()
        {
            reminder = new List<ReminderModel>();
        }
    }
}
