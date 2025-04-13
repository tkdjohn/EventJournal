using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class EventRepository(IDatabaseContext db) : BaseRepository<Event>(db, db.Events) {
        protected override void CopyEntity(Event destinationEntity, Event sourceEntity) {
            //TODO: should these really call a copy method on the entity itself?
            //TODO: could this be done with automapper or some such
            destinationEntity.Description = sourceEntity.Description;
            destinationEntity.EventTime = sourceEntity.EventTime;
        }
    }
}
