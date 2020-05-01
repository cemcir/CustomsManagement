using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CustomersManagement.Domain;
using CustomersManagement.Enum;
using CustomersManagement.Extensions;
using CustomersManagement.IRepository;
using CustomersManagement.Models.Reponse;
using CustomersManagement.Models.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CustomersManagement.Controllers
{
    [Route("customer")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly ICustomerRepository _CustomerRepo;
        private readonly ILogger _Logger;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customersRepository, ILoggerFactory loggerFactory,IMapper mapper)
        {
            this._CustomerRepo = customersRepository;
            this.mapper = mapper;
            this._Logger = loggerFactory.CreateLogger(nameof(CustomerController));
        }

        [HttpGet("getcustomers")]
        public IActionResult GetCustomer() {
            try {
                var customer = this._CustomerRepo.GetCustomers();
                if (customer == null) {
                    return BadRequest("customer could not be found ");
                }
                return Ok(new ApiListResponse<Customer> {Status=true,Entry=customer });
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiListResponse<Customer> { Status = false, Entry = null });
            }
        }

        [HttpGet("getcustomer/{id:int}")]
        public IActionResult GetByIdCustomer(int id) {
            try {
                var custom = this._CustomerRepo.FindByCustomerId(id);
                if (custom != null) {
                    return Ok(new ApiResponse<Customer>{Status=true,Entry=custom});
                }
                return BadRequest(new ApiResponse<Customer> {Status=false,Entry=null,ErrorCode=ErrorMessageCode.UserNotFound });
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Customer> { Status = false, Entry = null,ErrorCode=ErrorMessageCode.CreateError});
            }
        }

        [HttpPost("addcustomer")]
        public IActionResult AddCustomer(CustomerResource customerResource) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState.GetErrorMessages());
                }
                else {
                    Customer customer = mapper.Map<CustomerResource, Customer>(customerResource);
                    if (this._CustomerRepo.FindByCustomerEmailAndZip(customer.Email, customer.Zip)==false) {
                        var custom = this._CustomerRepo.InsertCustomer(customer);
                        if (custom != null) {
                            return Ok(new ApiResponse<Customer> { Status = true, Entry = custom });
                        }
                        return BadRequest(new ApiResponse<Customer> { Status = false, Entry = null ,ErrorCode=ErrorMessageCode.CreateError });
                    }
                    else {
                        return BadRequest(new ApiResponse<Customer> {Status=false,Entry=null,ErrorCode=ErrorMessageCode.EntityAlreadyExist });
                    }
                }
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Users> { Status = false, Entry = null,ErrorCode=ErrorMessageCode.CreateError });
            }
        }

        [HttpPut("updatecustomer")]
        public IActionResult UpdateCustomer(CustomerUpdateResource customerUpdateResource) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState.GetErrorMessages());
                }
                else {
                    Customer customer = mapper.Map<CustomerUpdateResource, Customer>(customerUpdateResource);
                   // var customers = this._CustomerRepo.FindByCustomerId(identityResource.Id);
                    
                   // if (customers != null) {

                        var custom = this._CustomerRepo.UpdateCustomer(customer);
                        if (custom != null) {
                            return Ok(new ApiResponse<Customer> { Status = true, Entry = custom });
                        }
                        return BadRequest(new ApiResponse<Customer> {Status=false,Entry=null });
                    //}
                    //return BadRequest(new ApiResponse<Customer> {Status=false,Entry=null,ErrorCode=   ErrorMessageCode.UserNotFound });
                }
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Customer> { Status = false, Entry = null });
            }
        }

        [HttpDelete("deletecustomer")]
        public IActionResult DeleteCustomer(IdentityResource identityResource) {
            try {
                var custom = this._CustomerRepo.FindByCustomerId(identityResource.Id);
                if (custom != null) {
                    var result = this._CustomerRepo.DeleteCustomer(custom);
                    if (result==true) {
                        return Ok(new ApiResponse<Customer> {Status=true,Entry=custom});          
                    }
                    return BadRequest(new ApiResponse<Customer> {Status=false,Entry=null,ErrorCode=ErrorMessageCode.CreateError}); 
                }
                return BadRequest(new ApiResponse<Customer> {Status=false,Entry=null,ErrorCode=ErrorMessageCode.UserNotFound});
            }
            catch (Exception ex) {

                throw;
            }
        }
    }
}