using EspressoPatronum.Models.Interfaces;
using Microsoft.Data.SqlClient;

namespace EspressoPatronum.Models.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly string _connectionString;
        private IConfiguration configuration;

        public GenericRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

   
        public void Add(TEntity entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name+ 's';
                var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "id");

                var columnNames = string.Join(",", properties.Select(p => p.Name));
                var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

                var query = $"INSERT INTO [{tableName}] ({columnNames}) VALUES ({parameterNames});";
                Console.WriteLine(query);
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var property in properties)
                    {
                        command.Parameters.AddWithValue("@" + property.Name, property.GetValue(entity));
                    }

                    command.ExecuteNonQuery();
                }
            }
        }
        public TEntity GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;
                var primaryKey = "Id";

                var query = $"SELECT * FROM {tableName} WHERE {primaryKey} = @Id;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return MapReaderToObject(reader);
                    }
                    return null;
                }
            }
        }
        public IEnumerable<TEntity> GetAllFood()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;

                var query = $"SELECT * FROM {tableName} where category= @cat;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cat", "food");
                    var reader = command.ExecuteReader();
                    var entities = new List<TEntity>();
                    while (reader.Read())
                    {
                        entities.Add(MapReaderToObject(reader));
                    }
                    return entities;
                }
            }
        }
        public IEnumerable<TEntity> GetAllDrink()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;

                var query = $"SELECT * FROM {tableName} where category= @cat;";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@cat", "drink");
                    var reader = command.ExecuteReader();
                    var entities = new List<TEntity>();
                    while (reader.Read())
                    {
                        entities.Add(MapReaderToObject(reader));
                    }
                    return entities;
                }
            }
        }
        public IEnumerable<TEntity> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var tableName = typeof(TEntity).Name;

                var query = $"SELECT * FROM {tableName};";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    var entities = new List<TEntity>();
                    while (reader.Read())
                    {
                        entities.Add(MapReaderToObject(reader));
                    }
                    return entities;
                }
            }
        }
        private TEntity MapReaderToObject(SqlDataReader reader)
        {
            var entity = Activator.CreateInstance<TEntity>();
            foreach (var property in typeof(TEntity).GetProperties())
            {
                if (property.Name != "Id")
                {
                    property.SetValue(entity, reader[property.Name]);
                }
            }
            return entity;
        }
    }
}
