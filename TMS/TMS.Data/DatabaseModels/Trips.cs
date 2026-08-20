using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Data.Models
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

        public IEnumerable<RailCardEventRecord> RailCarEventRecords { get; set; } = new List<RailCardEventRecord>();


    }
}
