using EventJournal.DomainEntities;
using EventJournal.DomainEntities.UserTypes;

namespace EventJournal.Data.UserTypeRepositories {
    public class DetailTypeRepository(IDatabaseContext db) : BaseRepository<DetailType>(db, db.DetailTypes), IDetailTypeRepository {
    }
}
