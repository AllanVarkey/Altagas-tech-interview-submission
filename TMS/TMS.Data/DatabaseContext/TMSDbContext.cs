using Microsoft.EntityFrameworkCore;

using TMS.Data.DatabaseModels;
using TMS.Data.Enums;


namespace TMS.Data.DatabaseContext
{
    public class TMSDbContext: DbContext
    {
        public TMSDbContext(DbContextOptions<TMSDbContext> options) : base(options)
        {

        }

        //city table is seeded 
        public DbSet<City> Cities { get; set; } = default!;
        public DbSet<Trips> Trips { get; set; } = default!;
        public DbSet<RailCarEventRecord> RailCarEventRecord { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City { CityId = 1, CityName = "Vancouver", TimezoneId = "Pacific Standard Time" },
                new City { CityId = 2, CityName = "Victoria", TimezoneId = "Pacific Standard Time" },
                new City { CityId = 3, CityName = "Kelowna", TimezoneId = "Pacific Standard Time" },
                new City { CityId = 4, CityName = "Kamloops", TimezoneId = "Pacific Standard Time" },
                new City { CityId = 5, CityName = "Prince George", TimezoneId = "Pacific Standard Time" },

                new City { CityId = 6, CityName = "Calgary", TimezoneId = "Mountain Standard Time" },
                new City { CityId = 7, CityName = "Edmonton", TimezoneId = "Mountain Standard Time" },
                new City { CityId = 8, CityName = "Lethbridge", TimezoneId = "Mountain Standard Time" },
                new City { CityId = 9, CityName = "Red Deer", TimezoneId = "Mountain Standard Time" },
                new City { CityId = 10, CityName = "Fort McMurray", TimezoneId = "Mountain Standard Time" },

                new City { CityId = 11, CityName = "Regina", TimezoneId = "Canada Central Standard Time" },
                new City { CityId = 12, CityName = "Saskatoon", TimezoneId = "Canada Central Standard Time" },
                new City { CityId = 13, CityName = "Moose Jaw", TimezoneId = "Canada Central Standard Time" },

                new City { CityId = 14, CityName = "Brandon", TimezoneId = "Central Standard Time" },
                new City { CityId = 15, CityName = "Winnipeg", TimezoneId = "Central Standard Time" },

                new City { CityId = 16, CityName = "Thunder Bay", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 17, CityName = "Sault Ste. Marie", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 18, CityName = "Sudbury", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 19, CityName = "North Bay", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 20, CityName = "Barrie", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 21, CityName = "Toronto", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 22, CityName = "Mississauga", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 23, CityName = "Hamilton", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 24, CityName = "London", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 25, CityName = "Kitchener", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 26, CityName = "Windsor", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 27, CityName = "St. Catharines", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 28, CityName = "Oshawa", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 29, CityName = "Kingston", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 30, CityName = "Ottawa", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 31, CityName = "Gatineau", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 32, CityName = "Montreal", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 33, CityName = "Quebec City", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 34, CityName = "Sherbrooke", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 35, CityName = "Trois-Rivières", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 36, CityName = "Saguenay", TimezoneId = "Eastern Standard Time" },
                new City { CityId = 37, CityName = "Rimouski", TimezoneId = "Eastern Standard Time" },

                new City { CityId = 38, CityName = "Edmundston", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 39, CityName = "Fredericton", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 40, CityName = "Moncton", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 41, CityName = "Saint John", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 42, CityName = "Bathurst", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 43, CityName = "Charlottetown", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 44, CityName = "Summerside", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 45, CityName = "Sydney", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 46, CityName = "Truro", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 47, CityName = "New Glasgow", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 48, CityName = "Dartmouth", TimezoneId = "Atlantic Standard Time" },
                new City { CityId = 49, CityName = "Halifax", TimezoneId = "Atlantic Standard Time" }
            );
            // setting the one to many relation between the trips and the railcar event records.
            // A trip can have many railcar event records but a railcar event record can only belong to one trip
            modelBuilder.Entity<Trips>(trip =>
            {
                trip.HasKey(tripObject => tripObject.TripId);
                trip.HasMany(tripObject => tripObject.RailCarEventRecords)
                    .WithOne(tp => tp.Trip)
                    .HasForeignKey(x => x.TripId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // In teh railcar event the eventtype needs to be parsed to a CHAR becuase we are using enums .
            
            modelBuilder.Entity<RailCarEventRecord>(railCardEventRecord =>
            {
                railCardEventRecord.HasKey(railCardEventRecordObject => railCardEventRecordObject.EventId);
                railCardEventRecord.Property(x => x.EventType)
                    .HasConversion(
                        v => ((char)v).ToString(),
                        v => (RailCarEventType)char.Parse(v)
                    );
            });

        }
    }
}
