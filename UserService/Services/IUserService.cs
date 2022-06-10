using UserService.entities;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {

        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<IEnumerable<UserProfile>> GetAll();
        Task<UserProfile> GetById(int id);
        Task<UserProfile> Update(int id, UserProfile user);
        UserProfile Add(UserProfile user);
        UserProfile Delete(UserProfile user);
        UserProfile GetByEmail(String emailId);
        public bool Exists(int id);
      

    }
}
