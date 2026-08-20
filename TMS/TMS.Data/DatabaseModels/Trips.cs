using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TMS.Data.DatabaseModels
{
    public class Trips
    {
        public int TripId { get; set; }
        public string EquipmentId { get; set; } = default!;
        public string OriginCityId { get; set; } = default!;
        public string DestinationCityId { get; set; } = default!;

        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime EndDate { get; set; } = new DateTime();
        public int TotalHours { get; set; } = 0;
        
        [JsonIgnore]
        public IEnumerable<RailCarEventRecord> RailCarEventRecords { get; set; } = new List<RailCarEventRecord>();


    }
}
