using ApiBlog.Models;
using ApiBlog.Models.DTOs;

namespace ApiBlog.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int userId);
        bool IsUniqueUser(string userName);
        Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO);
        Task<User> Register(UserRegisterDTO userRegisterDTO);

    }
}
