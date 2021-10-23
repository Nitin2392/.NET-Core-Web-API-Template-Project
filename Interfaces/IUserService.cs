using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Models;

namespace BoilerPlate.Interfaces
{
    interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
    }
}
