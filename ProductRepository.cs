using EspressoPatronum.Models.Interfaces;
using EspressoPatronum.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace EspressoPatronum.Models.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IGenericRepository<Product>
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public new void Add(Product entity)
        {
            base.Add(entity); 
        }

        public new IEnumerable<Product> GetAll()
        {
            return base.GetAll(); // Calls the method from GenericRepository to get all
        }

        public new Product GetById(int id)
        {
            return base.GetById(id); // Calls the method from GenericRepository to get product by ID
        }
    }
}
