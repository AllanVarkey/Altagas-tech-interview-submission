using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using TMS.Data.DatabaseContext;
using TMS.Data.Interfaces;
using TMS.Data.DatabaseModels;

namespace TMS.Data.Datastore
{
    public class TripsDatastore : ITripsRepository
    {
        public required TMSDbContext tmsDbContext { get; set; }
        public TripsDatastore(TMSDbContext _tmsDbContext)
        {
            this.tmsDbContext = _tmsDbContext;

        }

        public List<Trips> AddTrips(IEnumerable<Trips> tripsToWrite)
        {
            foreach(Trips trip in tripsToWrite)
            {
                this.tmsDbContext.Trips.Add(trip);
            }
            this.tmsDbContext.SaveChanges();
            return tripsToWrite.ToList();
        }

        public List<Trips> GetAllTrips()
        {
            var result =   this.tmsDbContext.Trips.Include(x => x.RailCarEventRecords).ToList();
            return result;
        }

        public Trips? GetTripById(int tripId)
        {
            Trips? trip = this.tmsDbContext.Trips.Include(x => x.RailCarEventRecords).FirstOrDefault(item => item.TripId == tripId); 
            return trip;
        }
    }
}
