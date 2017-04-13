using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RusoCars.Models;
using System.Data.Entity;
using System.Data;
using System.Diagnostics.Contracts;

namespace RusoCars.DAL
{
    public class GenericRepository<TEntity> :  RusoCars.DAL.IGenericRepository<TEntity> where TEntity : class
    {
        internal CarsRusoEntities context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository() { }

        public GenericRepository(CarsRusoEntities context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual List<TEntity> GetAll()
        {
            return null;
        }
    
        public virtual List<TEntity> GetAllFromCategory(int categoryId)
        {
            return null;
        }
        public virtual TEntity GetByID(int? id)
        {
            return null;
        }

        public virtual TEntity GetByID(int? id, int? categoryId)
        {
            return null;
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }
       
        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }

   






}
