using TMS.Data.Enums;

namespace TMS.Dtos
{
    public class RailCarEventRecordBase
    {
        public string EquipmentId { get; set; } = default!;
        public RailCarEventType EventType { get; set; }
        public DateTime EventTime { get; set; }
        public int CityId { get; set; }
        public int TripId { get; set; }
    }
    public class RailCarEventRecordRead: RailCarEventRecordBase
    {
        public int EventId { get; set; }
    }
    public class RailCarEventRecordWrite: RailCarEventRecordBase
    {
    }
}
