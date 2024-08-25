using EspressoPatronum.Models.Entities;
using EspressoPatronum.Models.Interfaces;
using EspressoPatronum.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EspressoPatronum.Controllers
{
    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
       
        public IActionResult ShowFood()
        {
            var productsList = _productRepository.GetAllFood();
            Console.WriteLine("ayooo");
            return View(productsList);
        }
        public IActionResult ShowDrink()
        {
            var productsList = _productRepository.GetAll();
            Console.WriteLine("ayooo");
            return View(productsList);
        }
        public IActionResult ShowDetails()
        {
            return View();
        }
        
        public IActionResult ProceedToCheckOut()
        {
            return View();
        }
    }
}
