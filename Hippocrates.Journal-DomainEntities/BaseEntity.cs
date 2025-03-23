using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hippocrates.Journal_DomainEntities {
    public class BaseEntity {
        [Required]
        public required DateTime CreatedDate { get; set; }
        [Required]
        public required DateTime UpdatedDate { get; set; }
    }
}
