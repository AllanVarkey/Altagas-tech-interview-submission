
using TMS.Data.Enums;

namespace TMS.Data.DatabaseModels
{
    public class RailCarEventRecord
    {
        public int EventId { get; set; }
        public string EquipmentId { get; set; } = default!;
        public RailCarEventType EventType { get; set; }
        public DateTime EventTime { get; set; }
        public int CityId { get; set; }
        public int TripId { get; set; }
        public Trips Trip { get; set; } = default!;

    }
    
}
