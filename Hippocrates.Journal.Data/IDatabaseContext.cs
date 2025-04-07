using Hippocrates.Journal.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace Hippocrates.Journal.Data {
    public interface IDatabaseContext {
        DbSet<Symptom> Symptoms { get; set; }
        DbSet<IntensityReposity> Intensities { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventTypeRepository> EventTypes { get; set; }
        DbSet<EventSymptomRepository> EventSymptoms { get; set; }

        Task SaveChangesAsync();
    }
}