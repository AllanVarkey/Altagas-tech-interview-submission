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

                // iterating over each record in the equipment group
                foreach (var eventRecord in equipmentGroup)
                {
                    // get the city for the event record
                    var city = cityRepository.GetCityById(eventRecord.CityId);
                    // pass the time with the city timezoneId to convert to UTC. 
                    var timeInUTC = DateTimeConvertToUtc(eventRecord.EventTime, city.TimezoneId);
                    eventRecord.EventTime = timeInUTC;

                    switch (eventRecord.EventType)
                    {
                        // case When the Trip Started. 
                        // in this case we will be setting the Start Time and the railcardEvent records
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
                        // case when the trip ended
                        // in this case we are calculating the total hours and setting the destination city and end date
                        // However i noticed there is an issue with the total hours . its not calculating accurately . I will have to look into it and fix it
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
                        // these are the intermittant arrival and departure between the release and place events. 
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
                // There is a DST gap. so im adding 1 hour to the local time to handle the DST gap. This is a workaround and may not be accurate for all cases.
                localTime = localTime.AddHours(1);
                return TimeZoneInfo.ConvertTimeToUtc(localTime, timeZone);
            }
        }


       

    }
}
