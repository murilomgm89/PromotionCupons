using Promotion.Coupon.Entity.Interfaces;
using Promotion.Coupon.Application.Interfaces.Base;
using Promotion.Coupon.Repository.Repositories.Base;

namespace Promotion.Coupon.Application.Applications.Base
{
    public class ApplicationBase<TEntity> : IApplicationBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;

        public ApplicationBase()
        {
            _repositoryBase = new RepositoryBase<TEntity>();
        }
        public TEntity Get(int id)
        {
            return _repositoryBase.Get(id);
        }

        public void Insert(TEntity entity)
        {
            _repositoryBase.Insert(entity);
        }

        public void Update(TEntity entity)
        {
            _repositoryBase.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _repositoryBase.Delete(entity);
        }
    }
}