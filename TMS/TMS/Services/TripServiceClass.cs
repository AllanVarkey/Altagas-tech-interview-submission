using AutoMapper;
using System.Runtime.CompilerServices;
using TMS.Data.DatabaseModels;
using TMS.Data.Enums;
using TMS.Data.Interfaces;
using TMS.Dtos;

namespace TMS.Services
{
    public class TripServiceClass
    {
        private readonly ITripsRepository tripsRepository;
        private readonly ICityRepository  cityRepository;
        private readonly IRailCarEventRecordRepository railCarEventRecordRepository;
        private readonly IMapper mapper;
        public TripServiceClass(ITripsRepository tripsRepository, ICityRepository cityRepository, IRailCarEventRecordRepository railCarEventRecordRepository, IMapper _mapper)
        {
            this.tripsRepository = tripsRepository;
            this.cityRepository = cityRepository;
            this.railCarEventRecordRepository = railCarEventRecordRepository;
            this.mapper = _mapper;
        }

        public List<Trips> BuildTrips(List<RailCarEventRecordWrite> railcarEventRecords)
        {
            var trips =  new List<Trips>();
            // since there are multiple equipments, we have to group them by equipmentId
            var groupedByEquipment = railcarEventRecords.GroupBy(r => r.EquipmentId);
            foreach (var equipmentGroup in groupedByEquipment)
            {

                Trips currentTrip = null;
                List<RailCarEventRecord> currentTripEvents = null;

                // each equipment entry has a cityid so we can use that to get the timezone from the city table.
                // once we have that we can convert the event time to the utc based on that timezone of the city.
                foreach (var eventRecord in equipmentGroup)
                {


                    var city = cityRepository.GetCityById(eventRecord.CityId);

                    var timeInUTC = DateTimeConvertToUtc(eventRecord.EventTime, city.TimezoneId);
                    eventRecord.EventTime = timeInUTC;

                    switch (eventRecord.EventType)
                    {
                        case RailCarEventType.Released: // W = trip start
                            currentTripEvents = new List<RailCarEventRecord>();
                            currentTripEvents.Add(eventRecord);
                            currentTrip = new Trips
                            {
                                EquipmentId = eventRecord.EquipmentId,
                                StartDate = eventRecord.EventTime,
                                RailCarEventRecords = currentTripEvents
                            };
                    
                            break;

                        case RailCarEventType.Placed: // Z = trip end
                            if (currentTrip != null)
                            {
                                currentTrip.EndDate = eventRecord.EventTime;
                                currentTripEvents?.Add(eventRecord);
                                trips.Add(currentTrip);
                                currentTrip = null;
                                currentTripEvents = null;
                            }
                            break;

                        default:
                            currentTripEvents?.Add(eventRecord);
                            break;
                    }
                }
            }

            return trips;
        }


        private DateTime DateTimeConvertToUtc(DateTime localTime, string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            return TimeZoneInfo.ConvertTimeToUtc(localTime, timeZone);
        }


       

    }
}
