using Hangfire;
using HangfireScheduler.Infrastructure.Contract;
using HangfireScheduler.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HangfireScheduler.Jobs
{
    [AutomaticRetry(Attempts = 0)]
    public class HelloWorldJob : IJob
    {
        private readonly string _name = "HelloWorldJob";
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly IUserService _userService;

        public string Name { get => _name; }
        public HelloWorldJob(ILogger<HelloWorldJob> logger,IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [DisableConcurrentExecution(int.MaxValue)]
        public async Task Execute()
        {
            var users = _userService.GetUsers();
            _logger.LogInformation("this is hello WorldJob");
            //await Task.Delay(TimeSpan.FromMinutes(2));
            //return Task.CompletedTask;
        }
    }
}
