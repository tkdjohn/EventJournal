using Hippocrates.Journal.DomainEntities;

namespace Hippocrates.Journal.Data {
    public class IntensityRepository(IDatabaseContext db) : BaseRepository<Intensity>(db, db.Intensities) {
        protected override void CopyEntity(Intensity destinationEntity, Intensity sourceEntity) {
            //TODO: could this be done with automapper or some such
            destinationEntity.Level = sourceEntity.Level;
            destinationEntity.Name = sourceEntity.Name;
            destinationEntity.Description = sourceEntity.Description;
            destinationEntity.DefaultSortType = sourceEntity.DefaultSortType;
        }
    }
}
