using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>:IEntityRepository<TEntity>
        where TEntity:class,IEntity,new ()  
        where TContext: DbContext,new()
   {
       public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
       {
           using (TContext context = new TContext())
           {
               return filter == null
                   ? context.Set<TEntity>().ToList()
                   : context.Set<TEntity>().Where(filter).ToList();
           }
       }

       public TEntity Get(Expression<Func<TEntity, bool>> filter)
       {
           using (TContext context = new TContext())
           {
               return context.Set<TEntity>().SingleOrDefault(filter);


           }
       }

       public void Add(TEntity entity)
       {
           //Idisposable patteren implementation of c#
           //using bittiği anda bellegı temizleme
           using (TContext context = new TContext())
           {
               var addedEntity = context.Entry(entity); // referansı yakala
               addedEntity.State = EntityState.Added; // eklenecek nesne
               context.SaveChanges(); // ekle kaydet anlamında
           }
       }

       public void Update(TEntity entity)
       {
           using (TContext context = new TContext())
           {
               var uptatedEntity = context.Entry(entity);
               uptatedEntity.State = EntityState.Modified;
               context.SaveChanges();
           }
       }

       public void Delete(TEntity entity)
       {
           using (TContext context = new TContext())
           {
               var deletedEntity = context.Entry(entity);
               deletedEntity.State = EntityState.Deleted;
               context.SaveChanges();
           }
       }

        
    }
}
