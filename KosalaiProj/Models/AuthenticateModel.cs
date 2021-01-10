using System;
using MongoDB.Bson.Serialization.Attributes;

namespace GORAKSHANA.Models
{
    public class AuthenticateModel 
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}