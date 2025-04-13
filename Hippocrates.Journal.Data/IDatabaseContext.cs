using Hippocrates.Journal.DomainEntities;
using Microsoft.EntityFrameworkCore;

namespace Hippocrates.Journal.Data {
    public interface IDatabaseContext {
        DbSet<Symptom> Symptoms { get; set; }
        DbSet<Intensity> Intensities { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<EventType> EventTypes { get; set; }
        DbSet<EventSymptom> EventSymptoms { get; set; }

        Task SaveChangesAsync();
    }
}