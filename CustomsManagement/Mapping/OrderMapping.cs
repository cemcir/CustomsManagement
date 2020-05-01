using AutoMapper;
using CustomersManagement.Domain;
using CustomersManagement.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Mapping
{
    public class OrderMapping:Profile {
        public OrderMapping() {
            CreateMap<OrderResource, Order>();
            CreateMap<Order, OrderResource>();
        }
    }
}
