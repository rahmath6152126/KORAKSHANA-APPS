using System;
using System.ComponentModel.DataAnnotations;

namespace Sharedmodels
{
    public class SearchModel
    {
        public string Id { get; set; }

        public string name { get; set; }

        public string contactno { get; set; }

        public string sponserType { get; set; }

        public string toSponser { get; set; }

        public string event_Type {get   ;set;}

        [
            DisplayFormat(
                ApplyFormatInEditMode = true,
                DataFormatString = "{0:dd MM yyyy}")
        ]
        public DateTime evetDate { get; set; }
    }
}
