using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class EventTypeRepository(IDatabaseContext db) : BaseRepository<Symptom>(db, db.Symptoms) {
    }
}
