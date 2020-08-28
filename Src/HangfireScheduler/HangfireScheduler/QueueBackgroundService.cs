using Hangfire;
using HangfireScheduler.Infrastructure;
using HangfireScheduler.Infrastructure.Contract;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HangfireScheduler
{
    public class QueueBackgroundService:BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<JobSchedule> _jobSchedule;

        public QueueBackgroundService(IServiceProvider serviceProvider,IEnumerable<JobSchedule> jobSchedule)
        {
                _serviceProvider = serviceProvider;
                 _jobSchedule = jobSchedule;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            foreach (var schedule in _jobSchedule)
            {
                switch (schedule.JobType)
                {
                    case JobType.BackGroundMethod:
                        IJob backgroundMethod = _serviceProvider.GetRequiredService(schedule.Job) as IJob;
                        BackgroundJob.Enqueue(() => backgroundMethod.Execute());
                        break;
                    case JobType.RecurringJob:
                        IJob recurringJob = _serviceProvider.GetRequiredService(schedule.Job) as IJob;
                        if (recurringJob != null)
                        {
                            var jobName = string.IsNullOrEmpty(recurringJob.Name) ? recurringJob.GetType().FullName + ".RecurringJob" : recurringJob.Name;
                            RecurringJob.AddOrUpdate(jobName,() => recurringJob.Execute(), schedule.CronExpression);
                        }
                        break;
                    default:
                        break;
                }
            }
           return Task.CompletedTask;
        }
    }
}
