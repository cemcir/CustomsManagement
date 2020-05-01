using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Models.Resource
{
    public class StudentResource
    {
        [Required]
        public string StudentName { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}
