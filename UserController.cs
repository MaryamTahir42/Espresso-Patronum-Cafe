using EspressoPatronum.Models.Repositories;
using EspressoPatronum.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using EspressoPatronum.Models.Interfaces;

namespace EspressoPatronum.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IGenericRepository<User> genericUser;
        
        string constring = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDB2;Integrated Security=True;";
        public UserController(IUserRepository userRepository, IGenericRepository<User> genericUser)
        {
            _userRepository = userRepository;
            this.genericUser = genericUser;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(User u)
        {           
            bool verify = _userRepository.CheckUser(u);
            //User loggedInUser = _userRepository.GetUserByCredentials(u.Email, u.Password);
            
            if (verify)
            {
                Console.WriteLine("Yay! You exist.\n");

                //CookieOptions options = new CookieOptions
                //{
                //    Expires = DateTime.Now.AddDays(30) 
                //};
                //Response.Cookies.Append("UserLoggedIn", u.Role, options);

                if (u.Role == "Admin")
                {
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    return RedirectToAction("Menu", "Home");
                }
            }
            else
            {
                Console.WriteLine("You don't exist, gurl.\nPlease sign up first.\n");

                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User u)
        {
            bool verify = _userRepository.CheckUser(u);
            if (verify == true)
            {
                Console.WriteLine("This login Id already exists\n");
                ViewBag.ErrorMessage = "This user already exists";
                return View(u);

            }
            else
            {
                Console.WriteLine("Adding to database\n");
                genericUser.Add(u);
               
                return RedirectToAction("Index", "Home");

            }
        }
    }
}
