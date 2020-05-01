using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersManagement.Data;
using CustomersManagement.Domain;
using Microsoft.Extensions.Logging;

namespace CustomersManagement.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly CustomerContext context;
        private readonly ILogger _Logger;

        public UserRepository(CustomerContext context, ILoggerFactory loggerFactory) {
            this.context = context;
            this._Logger = loggerFactory.CreateLogger("UserRepository");
        }

        public Users AddUser(Users user) {
            this.context.Users.Add(user);
            this.context.SaveChanges();
            return user;
        }

        public Users FindByEmail(string email) {
            return this.context.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public Users FindById(int id) {
            return this.context.Users.Find(id);
        }

        public Users FindByUserNameAndPassword(string email,string password) {
            return this.context.Users.Where(a => a.Email == email && a.Password == password).FirstOrDefault();   
        }
    }
}
