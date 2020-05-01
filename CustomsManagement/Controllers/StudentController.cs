using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomersManagement.Domain;
using CustomersManagement.Extensions;
using CustomersManagement.Models.Reponse;
using CustomersManagement.Models.Resource;
using CustomersManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomersManagement.Controllers
{
    [Route("student")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _StudentRepo;
        private readonly ILogger _Logger;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository customersRepository, ILoggerFactory loggerFactory,IMapper mapper )
        {
            this._StudentRepo = customersRepository;
            this.mapper = mapper;
            this._Logger = loggerFactory.CreateLogger(nameof(StudentController));
        }

        [HttpGet("GetStudents")]
        //[ProducesResponseType(typeof(List<Student>), 200)]
        //[ProducesResponseType(typeof(List<ApiResponse<Student>>), 400)]
        public IActionResult Students() {
            try {
                var students = this._StudentRepo.GetStudents();

                if (students == null) {
                    return BadRequest("customers can not be found");
                }
                return Ok(new ApiListResponse<Student> {Status =true, Entry=students });
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiListResponse<Student> { Status = false,Entry=null });
            }
        }

        [HttpPost("addstudent")]
        public IActionResult AddStudent([FromBody] StudentResource studentResource) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState.GetErrorMessages());
                }
                else {
                    Student student = mapper.Map<StudentResource, Student>(studentResource);
                    var st = this._StudentRepo.AddStudent(student);
                    if (st != null) {
                        return Ok(new ApiResponse<Student> { Status = true, Entry = st });
                    }
                    return BadRequest(new ApiResponse<Student> { Status = false, Entry = null });
                }
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                Console.WriteLine("HATA MESAJI"+ex.Message);
                return BadRequest(new ApiResponse<Student> { Status = false, Entry = null });
            }
        }

        [HttpGet("GetGroupByStudent")]
        public IActionResult GetGroupByStudent() {
            try {
                var students = this._StudentRepo.GroupByStudent();
                return Ok(new ApiListResponse<DepartmentWithStudent> { Status = true, Entry = students });
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiListResponse<DepartmentWithStudent> { Status = false, Entry = null });
            }
        }

    }
}