using EspressoPatronum.Models.Interfaces;
using EspressoPatronum.Models.Entities;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;

namespace EspressoPatronum.Models.Repositories
{

    public class UserRepository: GenericRepository<User>, IUserRepository
    {

        // Constructor that accepts a connection string and passes it to the base class constructor
        public UserRepository(IConfiguration configuration) : base(configuration)
       
        {
        }
        public IEnumerable<User> GetAll()
        {
            return base.GetAll();
        }
        public User GetById(int id)
        {
            return base.GetById(id);
        }
        public User GetUserSignup(User user)
        {
            User u = new User();
            u.Username = user.Username;
            u.Password = user.Password;
            u.Email= user.Email; 
            u.Address= user.Address;
            u.Role = user.Role;
            return u;
        }
        public User GetUserLogin(User user)
        {
            User u = new User();
            u.Password = user.Password;
            u.Email = user.Email;
            return u;
        }
        public bool CheckUser(User u)
        {
            string query = "Select * from Users where Email=@e and Password= @P";

            string constring = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MyDB2;Integrated Security=True;";
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(new SqlParameter("@e", u.Email));
                    cmd.Parameters.Add(new SqlParameter("@P", u.Password));
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;
                        }
                        return false;
                    }
                }


            }
        }


    }
}
