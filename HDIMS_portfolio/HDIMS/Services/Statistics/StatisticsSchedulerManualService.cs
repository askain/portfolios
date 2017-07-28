using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Collections;
using Amib.Threading;
using Common.Logging;
using System.Threading;
using HDIMS.Utils.Login;
using HDIMS.Models;

namespace HDIMS.Services.Statistics
{

    /// <summary>
    /// http://dotnetdave.net/blog/queueing-with-the-smart-thread-pool---update
    /// </summary>
    public sealed class StatisticsSchedulerManualService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(StatisticsSchedulerManualService));
        #endregion

        static readonly StatisticsSchedulerManualService instance = new StatisticsSchedulerManualService();
        private static Queue Queues;
        private static SmartThreadPool pool;
        private static ArrayList handles;
        public ArrayList Handles
        {
            get { return handles; }
            set { handles = value; }
        }
        public int ActiveThreads
        {
            get { return pool.ActiveThreads; }
        }
        public int InUseThreads
        {
            get { return pool.InUseThreads; }
        }
        public int AddJob(object job)
        {
            IWorkItemResult wir = pool.QueueWorkItem(new WorkItemCallback(DoRealWork), job);
            handles.Add(wir);
            return handles.IndexOf(wir);
        }
        private object DoRealWork(object job)
        {
            Hashtable param = job as Hashtable;
            EmpData empData = new EmpData();

            log.Info(string.Format("수동 스케쥴러 시작 : {0} {1} / {2} / {3}", empData.GetEmpData(0), empData.GetEmpData(1), param["SP_type"], param["OBSDT"]));
            
            
            try
            {
                switch(param["SP_type"].ToString()) {
                    case "SP_STAT_EXDAM":
                    DaoFactory.Instance.Update("Statistics.SP_STAT_EXDAM", param);
                    break;
                    case "SP_STAT_EXRF":
                    DaoFactory.Instance.Update("Statistics.SP_STAT_EXRF", param);
                    break;
                    case "SP_STAT_EXWL":
                    DaoFactory.Instance.Update("Statistics.SP_STAT_EXWL", param);
                    break;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            log.Info(string.Format("수동 스케쥴러 종료 : {0} {1} / {2} / {3}", empData.GetEmpData(0), empData.GetEmpData(1), param["SP_type"], param["OBSDT"]));

            return "Finished";
        }
        public static StatisticsSchedulerManualService Instance
        {
            get
            {
                return instance;
            }
        }
        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static StatisticsSchedulerManualService()
        {
            handles = new ArrayList();
            STPStartInfo sinfo = new STPStartInfo();
            sinfo.MaxWorkerThreads = 1;
            sinfo.MinWorkerThreads = 1;
            sinfo.UseCallerHttpContext = true;
            pool = new SmartThreadPool(sinfo);
        }
        StatisticsSchedulerManualService()
        {
        }
    }
}