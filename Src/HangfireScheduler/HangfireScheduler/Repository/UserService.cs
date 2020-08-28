using HangfireScheduler.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangfireScheduler.Repository
{
    public class UserService : IUserService
    {
        public List<User> GetUsers()
        {
            return new List<User>()
           {
               new User()
               {
                   Firstname="Jhon",
                   Lastname="Cena"
               },
               new User()
               {
                   Firstname="Michel",
                   Lastname="Scofield"
               }
           };
        }
    }
}
