using System.Data.Entity;
using Promotion.Coupon.Entity.Entities;
using Promotion.Coupon.Entity.Interfaces;

namespace Promotion.Coupon.Repository.Repositories.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        public TEntity Get(int id)
        {
            using (var context = new GymPass())
            {
                return context.Set<TEntity>().Find(id);
            }
        }

        public void Insert(TEntity entity)
        {
            using (var context = new GymPass())
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new GymPass())
            {
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
            
        }

        public void Delete(TEntity entity)
        {
            using (var context = new GymPass())
            {
                
            }
        }
    }
}
