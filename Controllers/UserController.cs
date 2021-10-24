using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using BoilerPlate.Interfaces;
using BoilerPlate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BoilerPlate.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        //Sample GET request to get all users

        [ProducesResponseType(typeof(List<User>), 200)]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
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
            if (id == null || id.Value <= 0)
            {
                return new BadRequestObjectResult("Id cannot be null or lesser than zero");
            }

            var users = await _userService.GetUserById(id.Value);

            if (users == null || users.Count == 0)
            {
                return new JsonResult(null);
            }

            return new OkObjectResult(users);
        }

        //Sample POST request 

        //Sample PUT request

        //Sample Delete request
    }
}
