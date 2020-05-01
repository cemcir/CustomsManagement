using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Domain
{
    public class Student
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string SurName { get; set; }

        public virtual Department Department { get; set; }

        public int DepartmentId { get; set; }
    }
}
