using CustomersManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.IRepository
{
    public interface ICustomerRepository { 
        List<Customer> GetCustomers();
        Customer InsertCustomer(Customer customer);
        Customer FindByCustomerId(int id);
        bool FindByCustomerEmailAndZip(string email,int zip);
        Customer UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
    }
}
