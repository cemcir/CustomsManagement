using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersManagement.Data;
using CustomersManagement.Domain;
using CustomersManagement.Models.Reponse;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomersManagement.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CustomerContext _Context;
        private readonly ILogger _Logger;
        
        public StudentRepository(CustomerContext context,ILoggerFactory loggerFactory)
        {
            this._Context = context;
            this._Logger = loggerFactory.CreateLogger("StudentRepository");
        }

        public Student AddStudent(Student student)
        {
            Department department = this._Context.Departments.Find(student.DepartmentId);
            
            try {
                Student st = new Student {
                    StudentName = student.StudentName,
                    SurName = student.SurName,
                    Department=department
                };
                
                this._Context.Students.Add(st);
                this._Context.SaveChanges();
                return student;
            }

            catch(Exception ex) {
                _Logger.LogError(ex.Message);
                return null;
            }
        }
        
        public List<Student> GetStudents() {
            return this._Context.Students.
              Include(d => d.Department).
              ToList();
        }
        
        public List<DepartmentWithStudent> GroupByStudent() {
            List<DepartmentWithStudent> departmentWithStudents = new List<DepartmentWithStudent>();
             var students= this._Context.Students.GroupBy(s =>s.Department)
                            .Select(Department => new { department = Department.Key.DepartmentName, studentCount = Department.Count() });
            foreach (var item in students) { 
                //Console.WriteLine("{0}. sınıfta {1} öğrenci var.", Satir.department.DepartmentName, Satir.OgrenciSayisi);
                departmentWithStudents.Add(new DepartmentWithStudent(item.department,item.studentCount));
            }
            return departmentWithStudents;
        }
        
    }
}
