using AutoMapper;
using System.Runtime.CompilerServices;
using TMS.Data.DatabaseModels;
using TMS.Data.Enums;
using TMS.Data.Interfaces;
using TMS.Dtos;

namespace TMS.Services
{
    public class TripServiceClass : ITripServiceClass
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

        public List<Trips> BuildTripsFromRailCarEvents(List<RailCarEventRecordWrite> railcarEventRecords)
        {
            var trips =  new List<Trips>();
            // since there are multiple equipments, we have to group them by equipmentId
            var groupedByEquipment = railcarEventRecords.GroupBy(r => r.EquipmentId);
            foreach (var equipmentGroup in groupedByEquipment)
            {

                Trips currentTrip = null;
                List<RailCarEventRecord> currentTripEvents = null;


                foreach (var eventRecord in equipmentGroup)
                {


                    var city = cityRepository.GetCityById(eventRecord.CityId);

                    var timeInUTC = DateTimeConvertToUtc(eventRecord.EventTime, city.TimezoneId);
                    eventRecord.EventTime = timeInUTC;

                    switch (eventRecord.EventType)
                    {
                        case RailCarEventType.Released: // W = trip start
                            currentTripEvents = new List<RailCarEventRecord>();
                            currentTripEvents.Add(mapper.Map<RailCarEventRecord>(eventRecord));
                            currentTrip = new Trips
                            {
                                OriginCityId = eventRecord.CityId.ToString(),
                                EquipmentId = eventRecord.EquipmentId,
                                StartDate = eventRecord.EventTime,
                                RailCarEventRecords = currentTripEvents
                            };
                            break;

                        case RailCarEventType.Placed: // Z = trip end
                            if (currentTrip != null)
                            {
                                currentTrip.DestinationCityId = eventRecord.CityId.ToString();
                                currentTrip.EndDate = eventRecord.EventTime;
                                currentTrip.TotalHours = (int)currentTrip.EndDate.Subtract(currentTrip.StartDate).TotalHours;
                                currentTripEvents?.Add(mapper.Map<RailCarEventRecord>(eventRecord));
                                trips.Add(currentTrip);
                            }
                            currentTrip =null;
                            currentTripEvents = null;
                            break;

                        default: // A: Arrival , D: Departures
                            if (currentTrip != null)  
                            {
                                currentTripEvents?.Add(mapper.Map<RailCarEventRecord>(eventRecord));
                            }
                            break;
                    }
                }
            }

            return trips;
        }


        private DateTime DateTimeConvertToUtc(DateTime localTime, string timeZoneId)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            try
            {
                return TimeZoneInfo.ConvertTimeToUtc(localTime, timeZone);
            }
            catch (ArgumentException)
            {
                // Time falls in DST gap, adjust by 1 hour and retry
                localTime = localTime.AddHours(1);
                return TimeZoneInfo.ConvertTimeToUtc(localTime, timeZone);
            }
        }


       

    }
}
