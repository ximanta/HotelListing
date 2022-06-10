
using HotelListing.API.Exceptions;
using HotelListing.API.Repository;
using UserService.entities;
using UserService.Models;

namespace UserService.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly HotelListing.API.Repository.IGenericRepository<UserProfile> _repository;
        private readonly ILogger<UserServiceImpl> _logger;

        public UserServiceImpl(IGenericRepository<UserProfile> repository, ILogger<UserServiceImpl> logger)
        {
            this._repository = repository;
            this._logger = logger;
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
            throw new NotImplementedException();
        }

    }
}
