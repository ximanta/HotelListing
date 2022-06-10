using UserService.entities;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {

        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Update(int id, User user);
        User Add(User user);
        User Delete(User user);
        User GetByEmail(String emailId);
        public bool Exists(int id);
      

    }
}
