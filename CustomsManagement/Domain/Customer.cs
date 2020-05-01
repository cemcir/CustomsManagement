using CustomersManagement.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        public int Zip { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public virtual City City { get; set; }

        public int CityId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        [Required]
        public int OrderCount { get; set; }
    }
}
