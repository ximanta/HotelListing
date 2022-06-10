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

namespace UserService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, ILogger<UsersController> logger, IMapper mapper)
        {
            this._userService = userService;
            this._logger = logger;
            this._mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogInformation("Request received for getting all countries at {DT}",
            DateTime.UtcNow.ToLongTimeString());
            var users = await _userService.GetAll();
            var userDTOs = _mapper.Map<List<OutgoingUserDto>>(users);
            return Ok(userDTOs);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetById(id);  
            var userDto = _mapper.Map<OutgoingUserDto>(user);
            return Ok(userDto);

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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
                if (UserExists(id))
                {
                    throw new NotFoundException(nameof(PutUser), id);
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
        public  ActionResult<User> PostUser(IncommingUserDto incommingUserDto)
        {
            var user = _mapper.Map<User>(incommingUserDto);
            _userService.Add(user);
            return CreatedAtAction("PostUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public  IActionResult DeleteUser(IncommingUserDto incommingUserDto)
        {
            var user = _mapper.Map<User>(incommingUserDto);
             _userService.Delete(user);
            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _userService.Exists(id);
        }
    }
}
