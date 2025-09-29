using Ardalis.Specification;

namespace Alten.ProductMasterTrial.Application.Common.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
    {
    }
}
