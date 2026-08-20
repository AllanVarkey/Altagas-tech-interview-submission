using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using TMS.Data.DatabaseContext;
using TMS.Data.Interfaces;
using TMS.Data.Models;

namespace TMS.Data.Datastore
{
    public class TripsDatastore : ITripsRepository
    {
        public required TMSDbContext tmsDbContext { get; set; }
        public TripsDatastore(TMSDbContext _tmsDbContext)
        {
            this.tmsDbContext = _tmsDbContext;

        }

        public Task<IEnumerable<Trips>> AddTrips(IEnumerable<Trips> tripsWrite)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Trips>> GetAllTripsAsync()
        {
            return this.tmsDbContext.Trips.Include(x => x.RailCarEventRecords).ToListAsync();
        }

        public Task<Trips> GetTripByIdAsync(int tripId)
        {
            Trips trip = this.tmsDbContext.Trips.Where(item => item.TripId == tripId).Include(x => x.RailCarEventRecords).FirstOrDefault();
            return Task.FromResult(trip);
        }
    }
}
