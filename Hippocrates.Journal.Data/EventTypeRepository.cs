using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class EventTypeRepository(IDatabaseContext db) : BaseRepository<EventType>(db, db.EventTypes) {
        protected override void CopyEntity(EventType destinationEntity, EventType sourceEntity) {
            //TODO: should these really call a copy method on the entity itself?
            //TODO: could this be done with automapper or some such
            destinationEntity.Description = sourceEntity.Description;
            destinationEntity.Name = sourceEntity.Name;
        }
    }
}
