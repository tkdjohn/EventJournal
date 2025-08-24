using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.PublicModels {
    public class EventDataResponseModel {
        public required IList<DetailTypeDto> DetailTypes { get; set; }
        public required IList<EventTypeDto> EventTypes { get; set; }
        public required IList<EventDto> Events { get; set; }
    }
}