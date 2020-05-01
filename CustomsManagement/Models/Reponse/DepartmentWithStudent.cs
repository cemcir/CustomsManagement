using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Models.Reponse
{
    public class DepartmentWithStudent
    {
        public string DepartmentName { get; set; }

        public int StudentCount { get; set; }

        public DepartmentWithStudent(string departmentName,int studentCount) {
            this.DepartmentName = departmentName;
            this.StudentCount = studentCount;
        }

    }
}
