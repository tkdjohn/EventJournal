using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class SymptomRepository(IDatabaseContext db) : BaseRepository<Symptom>(db, db.Symptoms) {
        protected override void CopyEntity(Symptom destinationEntity, Symptom sourceEntity) {
            //TODO: should these really call a copy method on the entity itself?
            //TODO: could this be done with automapper or some such
            destinationEntity.Description = sourceEntity.Description;
            destinationEntity.Name = sourceEntity.Name;
        }
    }
}
