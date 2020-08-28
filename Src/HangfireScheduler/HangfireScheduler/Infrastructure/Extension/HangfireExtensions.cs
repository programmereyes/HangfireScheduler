using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangFire(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddHangfire(configurations =>
            {
                configurations.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions()
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    //DisableGlobalLocks = true
                });
            });
            services.AddHangfireServer();
            return services;
        }
    }
}
