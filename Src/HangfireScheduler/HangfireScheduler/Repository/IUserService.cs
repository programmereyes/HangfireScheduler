using HangfireScheduler.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireScheduler.Repository
{
    public interface IUserService
    {
        List<User> GetUsers();
    }
}
