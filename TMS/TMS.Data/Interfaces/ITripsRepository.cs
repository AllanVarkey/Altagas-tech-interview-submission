
using TMS.Data.DatabaseModels;


namespace TMS.Data.Interfaces
{
    public interface ITripsRepository
    {
        List<Trips> GetAllTrips();
        Trips? GetTripById(int tripId);
        List<Trips> AddTrips(IEnumerable<Trips> tripsToWrite);
    }
}
