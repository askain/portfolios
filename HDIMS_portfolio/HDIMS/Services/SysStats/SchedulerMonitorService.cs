using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public class SchedulerMonitorService : ISchedulerMonitorService
    {
        public IList<SchedulerMonitorModel> GetSchedulerLogList(Hashtable param)
        {
            IList<SchedulerMonitorModel> list = new List<SchedulerMonitorModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<SchedulerMonitorModel>("SysStats.GetSchedulerLogList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        public IList<ExamMonitorModel> GetExamLogList(Hashtable param)
        {
            IList<ExamMonitorModel> list = new List<ExamMonitorModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ExamMonitorModel>("SysStats.GetExamLogList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
    }
}