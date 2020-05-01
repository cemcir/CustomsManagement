using AutoMapper;
using CustomersManagement.Domain;
using CustomersManagement.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Mapping
{
    public class CustomerMapping:Profile
    {
        public CustomerMapping() {
            CreateMap<CustomerResource, Customer>();
            CreateMap<Customer, CustomerResource>();
            CreateMap<CustomerUpdateResource, Customer>();
            CreateMap<Customer, CustomerUpdateResource>();
        }
    }
}
