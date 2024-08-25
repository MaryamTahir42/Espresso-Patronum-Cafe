using EspressoPatronum.Models.Entities;

namespace EspressoPatronum.Models.Interfaces
{
    public interface IUserRepository
    {
        public bool CheckUser(User u);
        public User GetUserLogin(User user);
        public User GetUserSignup(User user);
    }
}
