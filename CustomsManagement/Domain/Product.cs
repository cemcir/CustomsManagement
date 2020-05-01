using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Domain
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
