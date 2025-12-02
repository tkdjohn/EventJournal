using EventJournal.Data.Entities;

namespace EventJournal.Data {
    public interface IBaseRepository<T> where T : BaseEntity {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByResourceIdAsync(Guid resourceId);
        Task<T> AddUpdateAsync(T source);
        Task<IEnumerable<T>> AddUpdateAsync(IEnumerable<T> sources);
        void Delete(T entity);

        Task SaveChangesAsync();
    }
}