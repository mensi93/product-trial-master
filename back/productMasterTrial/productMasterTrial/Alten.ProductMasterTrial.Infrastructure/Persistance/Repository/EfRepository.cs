using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace Alten.ProductMasterTrial.Infrastructure.Persistance.Repository
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class
    {
        public EfRepository(ProductTrialDbContext dbContext) : base(dbContext)
        {
        }
    }
}
