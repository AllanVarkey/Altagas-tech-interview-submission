
using TMS.Data.Models;

namespace TMS.Data.Interfaces
{
    public interface ITripsRepository
    {
        Task<IEnumerable<Trips>> GetAllTripsAsync();
        Task<Trips> GetTripByIdAsync(int tripId);
        Task<IEnumerable<Trips>> AddTrips(IEnumerable<Trips> tripsWrite);
    }
}
