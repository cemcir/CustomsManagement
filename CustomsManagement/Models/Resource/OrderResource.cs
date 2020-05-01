using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Models.Resource
{
    public class OrderResource {
        
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}
