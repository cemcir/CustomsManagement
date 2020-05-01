using CustomersManagement.Data;
using CustomersManagement.Domain;
using CustomersManagement.IRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CustomerContext _Context;
        private readonly ILogger _Logger;

        public OrderRepository(CustomerContext context, ILoggerFactory loggerFactory) {

            this._Context = context;
            _Logger = loggerFactory.CreateLogger("OrderRepository");
        }

        public Order AddOrder(Order order) {
            var transaction = this._Context.Database.BeginTransaction();
            try {
                order.Date = DateTime.Now;
                this._Context.Orders.Add(order);
                this._Context.SaveChanges();
                //List<Order> orders = this._Context.Orders.Where(o => o.CustomerId == order.CustomerId).ToList();
                var orderCount=this._Context.Orders.Where(o=>o.CustomerId == order.CustomerId).Count();
                var custom = this._Context.Customers.Find(order.CustomerId);
                custom.OrderCount = orderCount;
                this._Context.Customers.Update(custom);
                this._Context.SaveChanges();
                transaction.Commit();
                return order;
            }
            catch (Exception ex) {
                transaction.Rollback();
                _Logger.LogError(ex.Message);
                return null;
            }
        }

        public List<Order> FindOrderByCustomerId(int customerId) {
            try {
                List<Order> orderList = this._Context.Orders.Where(o => o.CustomerId == customerId).ToList();
                if (orderList.Count>0) {
                    return orderList;
                }
                return null;
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return null;
            }
        }

        public Order FindOrderById(int id) {
            try {
                var order = this._Context.Orders.Find(id);
                if (order != null) {
                    return order;
                }
                return null;
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return null;
            }
        }

        public bool OrderDelete(int orderId) {
            var transaction = this._Context.Database.BeginTransaction();
            try {
                var order = this._Context.Orders.Find(orderId);
                if (order != null) {
                    this._Context.Orders.Remove(order);
                    this._Context.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                transaction.Commit();
                return false;
            }
            catch (Exception ex) {
                transaction.Rollback();
                _Logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
