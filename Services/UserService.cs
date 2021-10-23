using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Interfaces;
using BoilerPlate.Models;

namespace BoilerPlate.Services
{
    public class UserService : IUserService
    {
        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
