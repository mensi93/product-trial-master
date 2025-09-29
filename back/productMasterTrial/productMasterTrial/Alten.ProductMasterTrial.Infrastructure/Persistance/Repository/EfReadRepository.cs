using Alten.ProductMasterTrial.Application.Common.Interfaces;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;

namespace Alten.ProductMasterTrial.Infrastructure.Persistance.Repository
{
    public class EfReadRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class
    {
        public EfReadRepository(ProductTrialDbContext dbContext) : base(dbContext)
        {
        }
    }
}
