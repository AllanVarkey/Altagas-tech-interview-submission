using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Client.Models;
using TMS.Data.DatabaseModels;
using TMS.Data.Enums;
using TMS.Data.Interfaces;
using TMS.Dtos;
using TMS.Services;

namespace TMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        public required ITripsRepository tripsRepository { get; set; }
        public required ITripServiceClass tripServiceClass { get; set; }
        public IMapper autoMapper { get; set; }
        public TripsController(ITripsRepository _tripsRepository, ITripServiceClass _tripServiceClass ,IMapper _automapper)
        {
            tripsRepository = _tripsRepository;
            tripServiceClass = _tripServiceClass;
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
        
        // this is where the uplaod csv file will be handled. 
        // calling the helper function in the tripService class to build the trips from rail car events

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File Is empty");

            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            using var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

            var rawRecords = csv.GetRecords<RailCarEventCsvModel>().ToList();

            var railCardEventRecordWriteDtos = rawRecords.Select(r => new RailCarEventRecordWrite
            {
                EquipmentId = r.EquipmentId,
                EventType = (RailCarEventType)r.EventCode[0],
                EventTime = DateTime.Parse(r.EventTime),
                CityId = r.CityId
            }).ToList();

            var trips = tripServiceClass.BuildTripsFromRailCarEvents(railCardEventRecordWriteDtos);
            var savedTrips = tripsRepository.AddTrips(trips);
            return Ok(savedTrips);
        }
    }
}
