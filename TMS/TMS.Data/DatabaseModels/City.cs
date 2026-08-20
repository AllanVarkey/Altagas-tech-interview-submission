using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.BusinessLogic.DatabaseModels
{
    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; } = default!;
        public string TimezoneId { get; set; } = default!; 
    }
}
