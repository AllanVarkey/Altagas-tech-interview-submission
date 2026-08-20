using AutoMapper;
using TMS.Data.DatabaseModels;
using TMS.Data.Enums;
using TMS.Dtos;

namespace TMS.Automapper
{
    public class TMSAutoMapperProfile : Profile
    {
        public TMSAutoMapperProfile()
        {
            CreateMap<Trips, TripRead>()
                .ForMember(dest => dest.RailCarEventRecords, opt => opt.MapFrom(src => src.RailCarEventRecords));
            CreateMap<TripWrite, Trips>()
                .ForMember(dest => dest.RailCarEventRecords, opt => opt.MapFrom(src => src.RailCarEventRecords));

            CreateMap<RailCarEventRecord, RailCarEventRecordRead>();
            CreateMap<RailCarEventRecordWrite, RailCarEventRecord>()
                .ForMember(dest => dest.EventType, opt => opt.MapFrom(src => src.EventType))
                .ForMember(dest => dest.EventTime, opt => opt.MapFrom(src => src.EventTime))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
                .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.EquipmentId));
        }
    }
}
