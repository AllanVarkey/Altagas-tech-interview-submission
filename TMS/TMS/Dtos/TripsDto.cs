using TMS.Data.DatabaseModels;

namespace TMS.Dtos
{
    public class TripBase
    {
       
        public string EquipmentId { get; set; } = default!;
        public string OriginCityId { get; set; } = default!;
        public string DestinationCityId { get; set; } = default!;
        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime EndDate { get; set; } = new DateTime();
        public int TotalHours { get; set; } = 0;
        public IEnumerable<RailCarEventRecordRead> RailCarEventRecords { get; set; } = default!;
    }

    public class TripRead: TripBase
    {
        public int TripId { get; set; }
    }

    public class  TripWrite: TripBase 
    {
        
    }
}
