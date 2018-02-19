namespace Promotion.Coupon.Application.Interfaces.Base
{
    public interface IApplicationBase<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}