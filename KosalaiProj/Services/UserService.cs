using System;
using System.Collections.Generic;
using System.Linq;
using GORAKSHANA.IServices;
using GORAKSHANA.Models;
using MongoDB.Driver;
using GORAKSHANA.Helpers;

namespace GORAKSHANA.Services
{
    public class UserService : DataBaseService<UserModel>,IUserService
    {
        

        public UserService(IDatabaseSettings settings) : base(settings, "UserModel")
        {
        }


        public override bool Equals(object obj)
        {
            return obj is UserService services &&
            EqualityComparer<object>.Default.Equals(Builder, services.Builder);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public UserModel Authenticate(string username, string password)
        {

            var user =  Get(Builders<UserModel>.Filter.Eq(nameof(UserModel.userName),username));

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so return user details without password
            return user.WithoutPassword();
        }

    }
}
