using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BoilerPlate.Interfaces;
using BoilerPlate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static BoilerPlate.Configuration.ModelValidation;

namespace BoilerPlate.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IHelperService _helperService;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IHelperService helperService, 
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _helperService = helperService;
        }

        //Sample GET request to get all users

        [ProducesResponseType(typeof(List<User>), 200)]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            if (!Request.Headers.ContainsKey("Authorization") || !_helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new 
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }
            
            var users = await _userService.GetAllUsers();

            if (users == null || users.Count == 0)
            {
                return new JsonResult(null);
            }

            return new OkObjectResult(users);
        }

        //Sample GET request based on ID
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetAllUsers(int? id)
        {
            if (!Request.Headers.ContainsKey("Authorization") || _helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            if (id == null || id.Value <= 0)
            {
                return new BadRequestObjectResult("Id cannot be null or lesser than zero");
            }

            var users = await _userService.GetUserById(id.Value);

            if (users == null)
            {
                return new JsonResult(null);
            }

            return new OkObjectResult(users);
        }

        //Sample POST request 
        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] User user)
        {
            if (!Request.Headers.ContainsKey("Authorization") || _helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            if (!user.ValidateNewUser().ValidationResult)
            {
                return new BadRequestObjectResult(new
                {
                    Error = user.ValidateNewUser().Reason
                });
            }

            var userId = await _userService.CreateNewUser(user);

            if (userId == -1)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(new
            {
                UserId = userId.ToString()
            });
        }

        //Sample PUT request 
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (!Request.Headers.ContainsKey("Authorization") || _helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            if (user.Id == null)
            {
                return new BadRequestObjectResult(new
                {
                    Error = "Id has to be present"
                });
            }

            if (!user.ValidateNewUser().ValidationResult)
            {
                return new BadRequestObjectResult(new
                {
                    Error = user.ValidateNewUser().Reason
                });
            }

            var userId = await _userService.UpdateUser(user, Convert.ToInt32(user.Id));

            if (userId == -1)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(new
            {
                UserId = userId.ToString()
            });
        }

        //Sample Delete request
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (!Request.Headers.ContainsKey("Authorization") || _helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            if (id == null)
            {
                return new BadRequestObjectResult(new
                {
                    Error = "Id has to be present"
                });
            }

            var response = await _userService.DeleteUser(Convert.ToInt32(id));

            if (!response)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(new
            {
                Response = true
            });
        }
    }
}
