using ClienteBlazorWASM.Models;

namespace ClienteBlazorWASM.Services.IServices
{
    public interface IServiceAuthentication
    {
        Task<ResponseRegister> RegisterUser(UserRegister userForRegister);
        Task<ResponseAuthentication> Login(UserAuthentication userFromAuthentication);
        Task Exit();        
    }
}
