using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public interface ISchedulerMonitorService
    {
        IList<SchedulerMonitorModel> GetSchedulerLogList(Hashtable param);
        IList<ExamMonitorModel> GetExamLogList(Hashtable param); 
    }
}