using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class SymptomRepository(IDatabaseContext db) : BaseRepository<Symptom>(db, db.Symptoms) {

    }
}
