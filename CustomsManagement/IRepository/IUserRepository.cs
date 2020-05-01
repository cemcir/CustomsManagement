using CustomersManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Repository
{
    public interface IUserRepository
    {
        Users FindByUserNameAndPassword(string userName,string password);
        Users FindByEmail(string email);
        Users FindById(int id);
        Users AddUser(Users user);
    }
}
