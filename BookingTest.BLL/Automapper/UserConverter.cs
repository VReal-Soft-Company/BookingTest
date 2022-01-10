using AutoMapper;
using BookingTest.BLL.Helper;
using BookingTest.DLL.Entities;
using BookingTest.DTO.User;
using BookingTest.DTO.User.Basic;

namespace BookingTest.BLL.Automapper
{
    public static class UserConverter 
    {

        public static User ToEntity<T> (this T item) where  T : BasicAuthDTO
        {
            var user = Mapper.Map<User>(item);
            SetPassword(user, item.Password);
            return user;
        } 
        private static void SetPassword(User user, string password)
        {
            PasswordHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
        }
    }
}
