using CustomersManagement.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Models.Resource
{
    public class CustomerResource {

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public int Zip { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }
}
