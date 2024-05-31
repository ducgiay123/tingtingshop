using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopWeb.Core.src.Common;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Controller.src
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderReadDto>> AddOrder(OrderCreateDto orderDto)
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw AppException.Unauthorized("User is not logged in");
            }

            Guid guidId;
            if (!Guid.TryParse(userId, out guidId))
            {
                throw AppException.BadRequest("Invalid user ID");
            }
            if (orderDto == null)
            {
                throw AppException.BadRequest("Invalid request body");
            }

            var orderReadDto = await _orderService.CreateOrderAsync(guidId , orderDto);
            return Ok(orderReadDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrdersByUserId()
        {
            string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                throw AppException.Unauthorized("User is not logged in");
            }

            Guid guidId;
            if (!Guid.TryParse(userId, out guidId))
            {
                throw AppException.BadRequest("Invalid user ID");
            }

            var orders = await _orderService.GetOrdersByUserIdAsync(guidId);
            if (orders == null || !orders.Any())
            {
                throw AppException.NotFound("No orders found for this user");
            }

            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getall")]
        public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getbyId/{id:guid}")]
        public async Task<ActionResult<OrderReadDto>> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw AppException.NotFound($"Order with ID {id} not found");
            }
            return Ok(order);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderService.DeleteOrderByIdAsync(id);
            if (!result)
            {
                throw AppException.NotFound($"Order with ID {id} not found");
            }
            return NoContent();
        }
    }
}
