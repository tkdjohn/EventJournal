using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class EventSymptomRepository(IDatabaseContext db) : BaseRepository<Symptom>(db, db.Symptoms) {
    }
}
