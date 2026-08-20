using System;
using System.Collections.Generic;
using System.Text;
using TMS.Data.DatabaseModels;

namespace TMS.Data.Interfaces
{
    public interface ICityRepository
    {
        City? GetCityById(int id);
    }
}
