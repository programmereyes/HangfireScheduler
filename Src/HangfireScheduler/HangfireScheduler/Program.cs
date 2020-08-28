using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using HangfireScheduler.Infrastructure.Contract;
using HangfireScheduler.Jobs;
using HangfireScheduler.Infrastructure;
using HangfireScheduler.Repository;

namespace HangfireScheduler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<HelloWorldJob>();
                    services.AddSingleton<IUserService, UserService>();
                    services.AddSingleton(new JobSchedule(typeof(HelloWorldJob), JobType.RecurringJob, Cron.Minutely()));
                    services.AddHangFire(hostContext.Configuration);
                    services.AddHostedService<QueueBackgroundService>();
                });
    }
}
