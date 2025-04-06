using Hippocrates.Journal.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hippocrates.Journal.Data {

    public class DatabaseContext {
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Event> Events { get; set; }
        public DatabaseContext() {
            // needed by ef clit tools
        }

        public DatabaseContext() {
            
        }
    }
}
