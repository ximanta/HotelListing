
using HotelListing.API.Exceptions;
using HotelListing.API.Repository;
using UserService.entities;
using UserService.Models;

namespace UserService.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly HotelListing.API.Repository.IGenericRepository<User> _repository;
        private readonly ILogger<UserServiceImpl> _logger;

        public UserServiceImpl(IGenericRepository<User> repository, ILogger<UserServiceImpl> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
        public User Add(User user)
        {
            _repository.AddAsync(user);
            return user;
        }

     

        public User Delete(User user)
        {
            _logger.LogInformation("Received Delete call for all User {USER} at {DT}", user, DateTime.UtcNow.ToLongTimeString());

            if (GetById(user.Id) is null)
            {
                throw new NotFoundException(nameof(Delete), user.Id);
            }
            _repository.DeleteAsync(user.Id);
            return user;
        }

        public  async Task<IEnumerable<User>> GetAll()
        {
            _logger.LogInformation("Received GET call for all Users at {DT}", DateTime.UtcNow.ToLongTimeString());
            return await _repository.GetAllAsync();
        }
        public User GetByEmail(string emailid) {
            return _repository.GetByEmail(emailid);
       }
        public async Task<User> GetById(int id)
        {
            _logger.LogInformation("Received GET call for User with {id}", id);
            User user = await _repository.GetAsync(id);
            if (user == null) {
                throw new NotFoundException(nameof(GetById), id);
            }
            return user;
        }

        public async Task<User> Update(int id, User user)

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
