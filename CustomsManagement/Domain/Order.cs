using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Domain
{
    public class Order {

        public int Id { get; set; }

        public virtual Product Product { get; set; }

        public int ProductId { get; set; }

        public virtual Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }
    }
}
