using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BoilerPlate.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BoilerPlate.Models;

namespace BoilerPlate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotographyController : Controller
    {
        private readonly ILogger<PhotographyController> _logger;
        private readonly IPhotographyService _photographyService;
        private readonly IHelperService _helperService;

        public PhotographyController(ILogger<PhotographyController> logger, IHelperService helperService,
            IPhotographyService photographyService)
        {
            _logger = logger;
            _photographyService = photographyService;
            _helperService = helperService;
        }

        //Sample GET request to get all users

        [ProducesResponseType(typeof(List<Photographer>), 200)]
        [HttpGet("GetAllPhotographers")]
        public async Task<IActionResult> GetAllPhotographers()
        {
            if (!Request.Headers.ContainsKey("Authorization") || !_helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            var users = await _photographyService.GetPhotographers();

            if (users == null || users.Count == 0)
            {
                return new JsonResult(null);
            }

            return new OkObjectResult(users);
        }

        [ProducesResponseType(typeof(Photographer), 200)]
        [HttpGet("GetPhotographerById/{id}")]
        public async Task<IActionResult> GetPhotographerById(int id)
        {
            if (!Request.Headers.ContainsKey("Authorization") || !_helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            var users = await _photographyService.GetSpecificPhotographers(id);

            if (users == null)
            {
                return new JsonResult(null);
            }

            return new OkObjectResult(users);
        }

        [ProducesResponseType(typeof(Photographer), 200)]
        [HttpGet("GetPhotographerByEventType/{eventType}")]
        public async Task<IActionResult> GetPhotographerByEventType(string eventType)
        {
            if (!Request.Headers.ContainsKey("Authorization") || !_helperService.ValidateHeader(Request.Headers["Authorization"]))
            {
                return new UnauthorizedObjectResult(new
                {
                    Error = "Request Not Authenticated" //Anonymous Types
                });
            }

            var users = await _photographyService.GetPhotoGrapherByEventType(eventType);

            if (users == null || users.Count == 0)
            {
                return new JsonResult(null);
            }

            return new OkObjectResult(users);
        }
    }
}