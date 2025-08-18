using AutoMapper;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class DomainMapperProfile : Profile {
        public DomainMapperProfile() {
            CreateMap<Intensity, IntensityDto>().ReverseMap();
            CreateMap<DetailType, DetailTypeDto>().ReverseMap();
            CreateMap<Detail, DetailDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
