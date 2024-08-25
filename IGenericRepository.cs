namespace EspressoPatronum.Models.Interfaces
{
    public interface IGenericRepository<TEntity>
    {
        void Add(TEntity entity);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAllFood();
        IEnumerable<TEntity> GetAllDrink();
        IEnumerable<TEntity> GetAll();
     
    }
}
