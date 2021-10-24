using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Models;

namespace BoilerPlate.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetUserById(int id);
    }
}
