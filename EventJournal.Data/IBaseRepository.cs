using EventJournal.Data.Entities;

namespace EventJournal.Data {
    public interface IBaseRepository<T> where T : BaseEntity {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByResourceIdAsync(Guid resourceId);
        Task<T> AddUpdateAsync(T source);
        Task DeleteAsync(T entity);
    }
}