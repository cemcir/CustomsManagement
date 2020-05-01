using CustomersManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.IRepository
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order);

        List<Order> FindOrderByCustomerId(int customerId);

        bool OrderDelete(int orderId);

        Order FindOrderById(int id);
    }
}
