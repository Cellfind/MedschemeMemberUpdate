using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedschemeMemberUpdateSMSService
{
    public partial class Service1 : ServiceBase
    {
        public bool Abort = false;
        Thread t = null;
        //public Logger logger;
        public int servicesleeptime;
        JobScheduler scheduler;

        public Service1()
        {
            InitializeComponent();
            this.CanStop = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        private static IScheduler _scheduler;

        protected override void OnStart(string[] args)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();
            _scheduler.Start();

            scheduler = new JobScheduler();

            scheduler.CronMedschemeNumberUpdate();
        }

        protected override void OnStop()
        {
            if (scheduler != null)
            {
                scheduler.Stop();

            }
        }

        public class JobScheduler
        {
            public void CronMedschemeNumberUpdate()
            {
                string CronRuntime = ConfigurationManager.AppSettings["CronRuntime"];

                // construct a scheduler factory
                IJobDetail job = JobBuilder.Create<Sender>()
                        .WithIdentity("CronSMSSender", "TaskOneGroup")
                        .Build();
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("CronPayMeNowReports", "TaskOneGroup")
                .StartNow()
                .WithCronSchedule(CronRuntime)
                .Build();
                _scheduler.ScheduleJob(job, trigger);
                _scheduler.TriggerJob(job.Key);
            }


            public void Stop()
            {
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
                scheduler.Shutdown();
            }
        }
    }
}
