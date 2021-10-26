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
        Task<User> GetUserById(int id);

        Task<int> CreateNewUser(User user);

        Task<int> UpdateUser(User user, int userId);

        Task<bool> DeleteUser(int userId);

        Task<bool> CreateNewPoll(Poll pollData);
    }
}
