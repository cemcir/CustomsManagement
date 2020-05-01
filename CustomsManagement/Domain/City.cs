using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Domain
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public virtual List<Customer> Customers { get; set; }
       
    }
}
