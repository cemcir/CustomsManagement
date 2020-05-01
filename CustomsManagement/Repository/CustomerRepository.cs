using AutoMapper;
using CustomersManagement.Data;
using CustomersManagement.Domain;
using CustomersManagement.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _Context;
        private readonly ILogger _Logger;
        private readonly IMapper mapper;

        public CustomerRepository(CustomerContext context,ILoggerFactory loggerFactory,IMapper mapper) {
            this._Context = context;
            this.mapper = mapper;
            _Logger = loggerFactory.CreateLogger("CustomerRepository");
        }

        public bool DeleteCustomer(Customer customer) {
            try {
                var custom = this._Context.Customers.Find(customer.Id);
                if (custom != null) {
                    this._Context.Customers.Remove(custom);
                    this._Context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return false;
            }
        }

        public bool FindByCustomerEmailAndZip(string email, int zip) {
            try {
                Customer customer =this._Context.Customers.Where(c => c.Email == email || c.Zip==zip).FirstOrDefault();
                if (customer != null) {
                    return true;
                }
                return false;
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return false;
            }
        }

        public Customer FindByCustomerId(int id) {
            return this._Context.Customers.
                        Include(o => o.Orders).
                        Include(c => c.City).
            FirstOrDefault(custom=>custom.Id==id);
        }

        public List<Customer> GetCustomers() {
            try {
                return this._Context.Customers.
                      Include(c => c.City).
                      Include(o => o.Orders).
                      ToList();
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return null;
            }
        }

        public Customer InsertCustomer(Customer customer) {
            var transaction = this._Context.Database.BeginTransaction();

            try {
                /*
                City city = this._Context.Cities.Find(customer.CityId);
                Customer custom = new Customer {
                    FirstName = customer.FirstName,
                    LastName=customer.LastName,
                    Address=customer.Address,
                    Email=customer.Email,
                    City=city,
                    Zip=customer.Zip,
                    Gender=customer.Gender,
                    OrderCount=0,
                    Orders=customer.Orders
                };
                */
                this._Context.Customers.Add(customer);
                this._Context.SaveChanges();
                transaction.Commit();
                return customer;
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                transaction.Rollback();
                return null;
            }
        }

        public Customer UpdateCustomer(Customer customer) {
            
            var transaction = this._Context.Database.BeginTransaction();
            try {
                
                var customers = this._Context.Customers.Find(customer.Id);
                int customerId = customer.Id;
                if (customers != null) {
                    
                    List<Order> orders = this._Context.Orders.Where(o => o.CustomerId == customerId).ToList();
                    this._Context.Customers.Remove(customers);
                    customer.Id = 0;
                    var custom=this._Context.Customers.Add(customer);
                    if (custom != null) {
                        foreach (var item in orders) {
                            item.Id =0;
                            item.CustomerId = customerId+1;
                            this._Context.Orders.Add(item);
                            this._Context.SaveChanges();
                        }
                    }
                    /*
                    if (this.DeleteCustomer(customers)) {
                        customer.Id = 0;
                        var custom = this.InsertCustomer(customer);
                        if (custom != null) {
                            foreach (var item in orders) {
                                item.Id = 0;
                                item.CustomerId = customerId;
                                this._Context.Orders.Add(item);
                                this._Context.SaveChanges();
                            }    
                        }
                    }
                    */
                    this._Context.SaveChanges();
                    transaction.Commit();
                    return customers;
                }
                return null;
            }
            catch (Exception ex) {
                transaction.Rollback();
                _Logger.LogError(ex.Message);
                return null;
            }
            
        }
        
    }
}
