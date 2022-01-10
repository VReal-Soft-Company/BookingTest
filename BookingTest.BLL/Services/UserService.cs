using BookingTest.BLL.Automapper;
using BookingTest.BLL.Data.Exceptions;
using BookingTest.BLL.Helper;
using BookingTest.BLL.Services.Base;
using BookingTest.DLL.Data;
using BookingTest.DLL.Database;
using BookingTest.DLL.Entities;
using BookingTest.DTO.User;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookingTest.BLL.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<UserSuccessLoginDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserSuccessLoginDTO> RegisterAsync(RegisterDTO registerDTO, string role = null);
    }
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IJWTHelper _JWTHelper;
        public UserService(ApplicationDataContext contex, IJWTHelper jWTHelper) : base(contex)
        {
            _JWTHelper = jWTHelper;
        }

        public async Task<UserSuccessLoginDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Email == loginDTO.Email);
            if (user == null)
            {
                throw new AppException("User are not exist", 404);
            }
            if (!PasswordHelper.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new AppException("Wrong credentials");
            }
            return ConvertToUserSuccessDTO(user);
        }

        public async Task<UserSuccessLoginDTO> RegisterAsync(RegisterDTO registerDTO, string role = null)
        {
            if (await _context.Users.AnyAsync(f => f.Email == registerDTO.Email))
            {
                throw new AppException("User are already exist");
            }
            var user = registerDTO.ToEntity();
            user.Role = role;
            if (string.IsNullOrEmpty(user.Role))
            {
                user.Role = Role.USER;
            }
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
            return ConvertToUserSuccessDTO(user);
        }
        private UserSuccessLoginDTO ConvertToUserSuccessDTO(User user)
        {
            return new UserSuccessLoginDTO() {  Email = user.Email, UserId = user.Id, Token = _JWTHelper.GenerateJwtToken(user)};
        }
    }
}
