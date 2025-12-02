using EventJournal.Data.Entities.UserTypes;

namespace EventJournal.DomainService {
    internal interface IInternalUserTypeService : IUserTypesService {
        Task<EventType?> GetEventTypeEntityAsync(Guid resourceId);
        Task<DetailType?> GetDetailTypeEntityAsync(Guid resourceId);
    }
}
