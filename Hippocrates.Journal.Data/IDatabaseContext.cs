using EventJournal.DomainEntities;
using EventJournal.DomainEntities.UserTypes;
using Microsoft.EntityFrameworkCore;

namespace EventJournal.Data {
    public interface IDatabaseContext {
        DbSet<Event> Events { get; set; }
        DbSet<EventType> EventTypes { get; set; }
        DbSet<Detail> Details { get; set; }
        DbSet<DetailType> DetailTypes { get; set; }
        DbSet<Intensity> Intensities { get; set; }

        Task SaveChangesAsync();
    }
}