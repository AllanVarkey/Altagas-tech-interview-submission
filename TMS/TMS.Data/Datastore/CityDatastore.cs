using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TMS.Data.DatabaseContext;
using TMS.Data.DatabaseModels;
using TMS.Data.Interfaces;
using TMS.Data.Migrations;

namespace TMS.Data.Datastore
{
    public class CityDatastore : ICityRepository
    {
        protected readonly TMSDbContext _dbContext;
        public CityDatastore(TMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public City? GetCityById(int id)
        {
            var foundCity =  _dbContext.Cities.FirstOrDefault(c => c.CityId == id);
            return foundCity;
        }
    }
}
