using System;
using System.Collections.Generic;
using System.Text;
using TMS.Data.DatabaseModels;

namespace TMS.Data.Interfaces
{
    public interface IRailCarEventRecordRepository
    {
        List<RailCarEventRecord> GetAllRailCarEventRecords();
        RailCarEventRecord? GetRailCarEventRecordById(int railCarEventRecordId);
        RailCarEventRecord AddRailCarEventRecord(RailCarEventRecord railCarEventRecordToWrite);
    }
}
