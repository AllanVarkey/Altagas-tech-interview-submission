using System;
using System.Collections.Generic;
using System.Text;
using TMS.Data.DatabaseContext;
using TMS.Data.DatabaseModels;
using TMS.Data.Interfaces;

namespace TMS.Data.Datastore
{
    public class RailCarEventRecordDatastore: IRailCarEventRecordRepository
    {
        protected readonly TMSDbContext dbContext;
        public RailCarEventRecordDatastore(TMSDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public RailCarEventRecord AddRailCarEventRecord(RailCarEventRecord railCarEventRecordToWrite)
        {
            this.dbContext.RailCarEventRecord.Add(railCarEventRecordToWrite);
            this.dbContext.SaveChanges();
            return railCarEventRecordToWrite;
        }

        public List<RailCarEventRecord> GetAllRailCarEventRecords()
        {
            return this.dbContext.RailCarEventRecord.ToList();
        }

        public RailCarEventRecord? GetRailCarEventRecordById(int railCarEventRecordId)
        {
            return this.dbContext.RailCarEventRecord.Find(railCarEventRecordId);
        }
    }
}
