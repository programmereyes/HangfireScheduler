using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HangfireScheduler.Infrastructure.Contract
{
    public interface IJob
    {
        string Name { get; }
        Task Execute();
    }
}
