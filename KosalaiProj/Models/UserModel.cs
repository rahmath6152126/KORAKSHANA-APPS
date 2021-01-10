using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GORAKSHANA.Models
{
    public class UserModel : DataBaseModel
    {


        public string userName { get; set; }

        public string password { get; set; }

        

    }
}
