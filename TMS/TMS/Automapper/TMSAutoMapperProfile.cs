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
        }
    }
}
