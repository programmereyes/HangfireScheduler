using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireScheduler.Infrastructure
{
    public class JobSchedule
    {
        public Type Job { get; private set; }
        public JobType JobType { get; private set; }
        public string CronExpression { get; private set; }
        public JobSchedule(Type job, JobType jobType, string cronExpression)
        {
            Job = job;
            JobType = jobType;
            CronExpression = cronExpression;
        }
    }
}
