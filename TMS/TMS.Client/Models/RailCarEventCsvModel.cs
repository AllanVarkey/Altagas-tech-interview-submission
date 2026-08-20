using CsvHelper.Configuration.Attributes;

namespace TMS.Client.Models
{
    public class RailCarEventCsvModel
    {
        [Name("Equipment Id")]
        public string EquipmentId { get; set; } = default!;
        [Name("Event Code")]
        public string EventCode { get; set; } = default!;
        [Name("Event Time")]
        public string EventTime { get; set; } = default!;
        [Name("City Id")]
        public int CityId { get; set; }
    }
}
