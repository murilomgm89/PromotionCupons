namespace Promotion.Coupon.Entity.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}