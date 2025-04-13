using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class EventSymptomRepository(IDatabaseContext db) : BaseRepository<EventSymptom>(db, db.EventSymptoms) {
        protected override void CopyEntity(EventSymptom destinationEntity, EventSymptom sourceEntity) {
            //TODO: should these really call a copy method on the entity itself?
            //TODO: could this be done with automapper or some such
            destinationEntity.Event = sourceEntity.Event;
            destinationEntity.Symptom = sourceEntity.Symptom;
            destinationEntity.Intensity = sourceEntity.Intensity;
            destinationEntity.Notes = sourceEntity.Notes;
        }
    }
}
