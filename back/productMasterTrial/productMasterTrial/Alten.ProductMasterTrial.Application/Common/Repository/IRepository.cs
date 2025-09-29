using Ardalis.Specification;

namespace Alten.ProductMasterTrial.Application.Common.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class
    {
    }
}
