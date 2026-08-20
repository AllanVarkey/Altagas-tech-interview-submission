using TMS.Data.DatabaseModels;
using TMS.Dtos;

namespace TMS.Services
{
    public interface ITripServiceClass
    {
        List<Trips> BuildTripsFromRailCarEvents(List<RailCarEventRecordWrite> railcarEventRecords);
    }
}
