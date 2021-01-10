using GORAKSHANA.Models;
using System.Collections.Generic;
namespace GORAKSHANA.IServices
{
    public interface IUserService : IFactoryServices<UserModel>
    {
        UserModel Authenticate(string username, string password);
    }
}