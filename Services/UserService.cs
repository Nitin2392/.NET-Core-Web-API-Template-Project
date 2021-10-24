using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BoilerPlate.Configuration;
using BoilerPlate.Interfaces;
using BoilerPlate.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BoilerPlate.Services
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings,
            ILogger<UserService> log)
        {
            _appSettings = appSettings.Value;
            _logger = log;
        }
        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var userList = new List<User>();
                var dataAccess = new BaseDataAccess(_appSettings);

                var usersTable = dataAccess.ExecuteDataSet("sp_GetAllUsers", null);
                
                foreach(DataRow row in usersTable.Tables[0].Rows)
                {
                    var user = new User()
                    {
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Id = row["Id"].ToString(),
                    };

                    userList.Add(user);
                }

                return userList;


            }
            catch (Exception e)
            {
               _logger.LogError("Something went wrong in GetAllUsers - ", e);
               return null;
            }
        }

        public async Task<List<User>> GetUserById(int id)
        {
            try
            {
                var userList = new List<User>();
                var dataAccess = new BaseDataAccess(_appSettings);

                var paramDict = new Dictionary<string, object>
                {
                    {"@UserId", id}
                };
                var usersTable = dataAccess.ExecuteDataSet("sp_GetSpecificUser", dataAccess.GenerateParameters(paramDict));

                foreach (DataRow row in usersTable.Tables[0].Rows)
                {
                    var user = new User()
                    {
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString(),
                        Id = row["Id"].ToString(),
                        Gender = row["Gender"].ToString(),
                    };

                    userList.Add(user);
                }

                return userList;
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong in GetUserById - ", e);
                return null;
            }
        }

        public async Task<int> CreateNewUser(User user)
        {
            try
            {
                var dataAccess = new BaseDataAccess(_appSettings);

                var paramDict = new Dictionary<string, object>
                {
                    {"@FName", user.FirstName},
                    {"@LName", user.LastName},
                    {"@UserName", user.UserName},
                    {"@pass", user.Password},
                    {"@gender", user.Gender},
                };

                var userID = dataAccess.ExecuteDataSet("sp_CreateNewUser", dataAccess.GenerateParameters(paramDict));

                return Convert.ToInt32(userID.Tables[0].Rows[0]["UserId"].ToString());
            }
            catch (Exception e)
            {
                _logger.LogError("Something went wrong in CreateNewUser", e);
                return -1;
            }
        }
    }
}
