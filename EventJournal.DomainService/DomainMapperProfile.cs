using AutoMapper;
using EventJournal.Data.Entities;
using EventJournal.Data.Entities.UserTypes;
using EventJournal.DomainDto;
using EventJournal.DomainDto.UserTypes;

namespace EventJournal.DomainService {
    public class DomainMapperProfile : Profile {
        public DomainMapperProfile() {

            //TODO: use reflection to find objects that inherit from BaseDto and map them
            //TODO: also move to bootstrapper
            CreateMap<Intensity, IntensityDto>().ReverseMap();
            CreateMap<DetailType, DetailTypeDto>().ReverseMap();
            CreateMap<Detail, DetailDto>().ReverseMap();
            CreateMap<EventType, EventTypeDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
        }
    }
}
