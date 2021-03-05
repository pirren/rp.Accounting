using rp.Accounting.Domain;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface IBaseRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        void DetachLocal<T>(T entity) where T : class, IIdentifier;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task<bool> CompleteAsync();
    }
}
