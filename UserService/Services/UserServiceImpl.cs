
using HotelListing.API.Exceptions;
using HotelListing.API.Repository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.entities;
using UserService.Models;

namespace UserService.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly HotelListing.API.Repository.IGenericRepository<UserProfile> _repository;
        private readonly ILogger<UserServiceImpl> _logger;
        private readonly AppSettings _appSettings;

        public UserServiceImpl(IGenericRepository<UserProfile> repository, ILogger<UserServiceImpl> logger, IOptions<AppSettings> appSettings)
        {
            this._repository = repository;
            this._logger = logger;
            this._appSettings = appSettings.Value;
        }
        public UserProfile Add(UserProfile user)
        {
            _repository.AddAsync(user);
            return user;
        }

     

        public UserProfile Delete(UserProfile user)
        {
            _logger.LogInformation("Received Delete call for all User {USER} at {DT}", user, DateTime.UtcNow.ToLongTimeString());

            if (GetById(user.Id) is null)
            {
                throw new NotFoundException(nameof(Delete), user.Id);
            }
            _repository.DeleteAsync(user.Id);
            return user;
        }

        public  async Task<IEnumerable<UserProfile>> GetAll()
        {
            _logger.LogInformation("Received GET call for all User Profiles at {DT}", DateTime.UtcNow.ToLongTimeString());
            return await _repository.GetAllAsync();
        }
        public UserProfile GetByEmail(string emailid) {
            return _repository.GetByEmail(emailid);
       }
        public async Task<UserProfile> GetById(int id)
        {
            _logger.LogInformation("Received GET call for User with {id}", id);
            UserProfile user = await _repository.GetAsync(id);
            if (user == null) {
                throw new NotFoundException(nameof(GetById), id);
            }
            return user;
        }

        public async Task<UserProfile> Update(int id, UserProfile user)

        {
            if (GetById(user.Id) is null)
            {
                _logger.LogError("User to be updates not found at {DT}", DateTime.UtcNow.ToLongTimeString());
                throw new NotFoundException(nameof(Update), user.Id);
            }
            return await _repository.AddAsync(user);
       
        }
        public bool Exists(int id)
        {
            var entity = _repository.Exists(id);
            return entity != null;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            string token=null;
            UserProfile userProfile = GetByEmail(model.EmailId);
            if (userProfile == null)
            {
                throw new NotFoundException(nameof(Authenticate), 0);
            }

            // authentication successful so generate jwt token
            if (userProfile.EmailId == model.EmailId && userProfile.Password == model.Password)
            {
                 token = generateJwtToken(userProfile);
            }

            return new AuthenticateResponse(userProfile, token);
        }
        private string generateJwtToken(UserProfile user)
        {
          
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                // Generated token is valid for 1 Hour
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
