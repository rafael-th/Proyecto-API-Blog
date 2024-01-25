using ApiBlog.Data;
using ApiBlog.Models;
using ApiBlog.Models.DTOs;
using ApiBlog.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MD5CryptoServiceProvider = XSystem.Security.Cryptography.MD5CryptoServiceProvider;


namespace ApiBlog.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public string secretPassword;

        public UserRepository(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            secretPassword = config.GetValue<string>("ApiSettings:Secret");
        }

        public User GetUser(int userId)
        {
            return _db.User.FirstOrDefault(u => u.Id == userId);
        }

        public ICollection<User> GetUsers()
        {
            return _db.User.OrderBy(u => u.Id).ToList();
        }

        public bool IsUniqueUser(string userName)
        {
            var userDb = _db.User.FirstOrDefault(u => u.UserName == userName);
            if (userDb == null)
            {
                return true;
            }
            return false;
        }

        public async Task<UserLoginResponseDTO> Login(UserLoginDTO userLoginDTO)
        {
            var passwordEncoded = getMd5(userLoginDTO.Password);
            var user = _db.User.FirstOrDefault(
                u => u.UserName.ToLower() == userLoginDTO.UserName.ToLower()
                && u.Password == passwordEncoded
                );
            //validamos si el usuario no existe con la combinacion de usuario y contraseño correcta
            if (user == null)
            {
                return new UserLoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            //Aqui si existe el usuario entonces podemos procesar el login
            var handlerToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretPassword);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = handlerToken.CreateToken(tokenDescriptor);

            UserLoginResponseDTO userLoginResponseDTO = new UserLoginResponseDTO()
            {
                Token = handlerToken.WriteToken(token),
                User = user
            };

            return userLoginResponseDTO;
        }

        public async Task<User> Register(UserRegisterDTO userRegisterDTO)
        {
            var passwordEncode = getMd5(userRegisterDTO.Password);

            User user = new User()
            {
                UserName = userRegisterDTO.UserName,
                Name = userRegisterDTO.Name,
                Email = userRegisterDTO.Email,
                Password= userRegisterDTO.Password,
            };

            _db.User.Add(user);
            user.Password = passwordEncode;
            await _db.SaveChangesAsync();
            return user;
        }

        public static string getMd5(string value)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(value);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }
    }
}
