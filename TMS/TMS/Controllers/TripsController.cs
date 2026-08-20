using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Data.DatabaseModels;
using TMS.Data.Interfaces;
using TMS.Dtos;

namespace TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        public required ITripsRepository tripsRepository { get; set; }
        public IMapper autoMapper { get; set; }
        public TripsController(ITripsRepository _tripsRepository, IMapper _automapper)
        {
            tripsRepository = _tripsRepository;
            autoMapper = _automapper;
        }

        [HttpGet("GetAllTrips")]
        public IActionResult GetAllTrips()
        {
            var trips =  tripsRepository.GetAllTrips();
            return Ok(trips);
        }

        [HttpGet("GetTripById/{tripId}")]
        public IActionResult GetTripById(int tripId)
        {
            var trip =  tripsRepository.GetTripById(tripId);
            if (trip == null)
            {
                return NotFound();
            }
            return Ok(trip);
        }

        [HttpPost("AddTrip")]
        public  IActionResult AddTrip([FromBody] IEnumerable<TripWrite> tripToWriteList)
        {
            var tripsToWrite = autoMapper.Map<IEnumerable<Trips>>(tripToWriteList);
            var addedTrips = tripsRepository.AddTrips(tripsToWrite);
            return Ok(addedTrips);
        }
    }
}
