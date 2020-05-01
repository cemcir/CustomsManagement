using CustomersManagement.Domain;
using CustomersManagement.Models.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomersManagement.Repository
{
    public interface IStudentRepository {
        List<Student> GetStudents();
        Student AddStudent(Student student);
        List<DepartmentWithStudent> GroupByStudent();
    }
}
