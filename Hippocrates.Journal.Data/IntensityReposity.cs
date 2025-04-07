using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class IntensityReposity(IDatabaseContext db) : BaseRepository<Symptom>(db, db.Symptoms) {
    }
}
