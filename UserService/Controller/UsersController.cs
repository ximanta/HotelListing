using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using UserService.entities;
using UserService.Services;
using AutoMapper;
using UserService.Dtos;
using HotelListing.API.Exceptions;
using Microsoft.AspNetCore.OData.Query;
using UserService.Models;
using MassTransit;

namespace UserService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public UsersController(IUserService userService, ILogger<UsersController> logger, IMapper mapper, IBus bus)
        {
            this._userService = userService;
            this._logger = logger;
            this._mapper = mapper;
            this._bus = bus;
        }

        // GET: api/Users
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            _logger.LogInformation("Request received for getting all countries at {DT}",
            DateTime.UtcNow.ToLongTimeString());
            var userProfiles = await _userService.GetAll();
            var userProfileDTOs = _mapper.Map<List<OutgoingUserProfileDto>>(userProfiles);
            return Ok(userProfileDTOs);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(int id)
        {
            var user = await _userService.GetById(id);  
            var userDto = _mapper.Map<OutgoingUserProfileDto>(user);
            return Ok(userDto);

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserProfile(int id, UserProfile user)
        {
            if (id != user.Id)
            {
                return BadRequest("Invalid User ID");
            }
                               
            try
            {
                await _userService.Update(user.Id, user);
            }
            catch (DbUpdateConcurrencyException dce)
            {
                if (UserProfileExists(id))
                {
                    throw new NotFoundException(nameof(PutUserProfile), id);
                }
                else
                {
                    _logger.LogError(dce, "Exception thrown at {DT}", DateTime.UtcNow.ToLongTimeString());

                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public  async Task<ActionResult<UserProfile>> PostUserProfile(IncommingUserProfileDto incommingUserDto)
        {
            var user = _mapper.Map<UserProfile>(incommingUserDto);
            var uri = new Uri("rabbitmq://localhost/user-send");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(user);
            return Ok("User save and sent successfully");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public  IActionResult DeleteUserProfile(IncommingUserProfileDto incommingUserDto)
        {
            var user = _mapper.Map<UserProfile>(incommingUserDto);
             _userService.Delete(user);
            return NoContent();
        }

        private bool UserProfileExists(int id)
        {
            return _userService.Exists(id);
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            //   _logger.LogInformation("Login attempted by {EmailId} at {DT}",model.EmailId,DateTime.UtcNow.ToLongTimeString());
            AuthenticateResponse response = _userService.Authenticate(model);

            if (response == null)
            {
          //      _logger.LogError("Invalid Login attempted by {EmailId} at {DT}", model.EmailId, DateTime.UtcNow.ToLongTimeString());

                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
    }
}
