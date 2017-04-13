using System;
namespace RusoCars.DAL
{
    interface IGenericRepository<TEntity>
     where TEntity : class
    {
        void Delete(int id);
        void Delete(TEntity entityToDelete);
        System.Collections.Generic.List<TEntity> GetAll();
        TEntity GetByID(int? id);
        TEntity GetByID(int? id, int? categoryId);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
