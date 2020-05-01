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
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _OrderRepo;
        private readonly ILogger _Logger;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository orderRepository, ILoggerFactory loggerFactory, IMapper mapper)
        {
            this._OrderRepo = orderRepository;
            this.mapper = mapper;
            this._Logger = loggerFactory.CreateLogger(nameof(OrderController));
        }

        [HttpPost("addorder")]
        public IActionResult AddOrder(OrderResource orderResource) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState.GetErrorMessages());
                }
                else {
                    Order order = mapper.Map<OrderResource, Order>(orderResource);
                    var orders = this._OrderRepo.AddOrder(order);
                    if (orders != null) {
                        return Ok(new ApiResponse<Order> {Status=true,Entry=orders });
                    }

                    return BadRequest(new ApiResponse<Order> {Status=false,Entry=null,ErrorCode=ErrorMessageCode.CreateError });
                }
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Order> { Status = false, Entry = null, ErrorCode = ErrorMessageCode.CreateError });
            }
        }

        [HttpGet("getorder/{id:int}")]
        public IActionResult GetOrder(int id) {
            try {
                var orders=this._OrderRepo.FindOrderByCustomerId(id);
                if (orders != null) {
                    return Ok(new ApiListResponse<Order> {Status=true,Entry=orders });
                }
                return BadRequest(new ApiListResponse<Order> {Status=false,Entry=null,ErrorMessage=ErrorMessageCode.UserNotFound});
            }
            catch (Exception ex) {
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiListResponse<Order> { Status = false, Entry = null, ErrorMessage = ErrorMessageCode.CreateError });
            }
        }

        [HttpDelete("deleteorder")]
        public IActionResult DeleteOrder(IdentityResource identityResource)
        {
            try {
                var order = this._OrderRepo.FindOrderById(identityResource.Id);
                if (order != null) {
                    var result = this._OrderRepo.OrderDelete(order.Id);
                    if (result==true) {
                        return Ok(new ApiResponse<Order> { Status = true, Entry = null });
                    }
                    return BadRequest(new ApiResponse<Order> {Status=false,Entry=null,ErrorCode=ErrorMessageCode.CreateError});
                }
                return BadRequest(new ApiResponse<Order> { Status = false, Entry = null, ErrorCode = ErrorMessageCode.UserNotFound });
            }
            catch (Exception ex){
                _Logger.LogError(ex.Message);
                return BadRequest(new ApiResponse<Order> { Status = false, Entry = null, ErrorCode = ErrorMessageCode.CreateError });
            } 
        }
    }
}