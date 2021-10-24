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

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            if (users == null || users.Count == 0)
            {
                return new JsonResult(null);
            }
            else
            {
                return new OkObjectResult(users);
            }
        }


        
        //Sample GET request based on ID

        //Sample POST request 

        //Sample PUT request

        //Sample Delete request
    }
}
