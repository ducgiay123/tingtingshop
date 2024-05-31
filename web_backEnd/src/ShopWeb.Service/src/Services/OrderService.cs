using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopWeb.Core.src.Common;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineItemsRepository _orderLineItemsRepository;

        private readonly IAddressRepository _addressRepository;

        public OrderService(IOrderRepository orderRepository, IOrderLineItemsRepository orderLineItemsRepository, IMapper mapper, IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _orderLineItemsRepository = orderLineItemsRepository;
            _mapper = mapper;
            _addressRepository = addressRepository;
        }

        public async Task<OrderReadDto> CreateOrderAsync(Guid userId, OrderCreateDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.UserId = userId;
            order = await _orderRepository.CreateNewOrderAsync(order);
            var orderReadDto = _mapper.Map<OrderReadDto>(order);

            return orderReadDto;
        }

        public async Task<bool> DeleteOrderByIdAsync(Guid id)
        {
            var success = await _orderRepository.DeleteOrderByIdAsync(id);
            if (!success)
            {
                throw AppException.NotFound($"Order with ID {id} not found.");
            }

            return success;
        }

        public async Task<IEnumerable<OrderReadDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();

            var orderDtos = _mapper.Map<IEnumerable<OrderReadDto>>(orders);

            return orderDtos;
        }

        public async Task<OrderReadDto> GetOrderByIdAsync(Guid id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw AppException.NotFound($"Order with ID {id} not found.");
            }
            var orderDto = _mapper.Map<OrderReadDto>(order);
            return orderDto;
        }

        public async Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            if (orders == null)
            {
                throw AppException.NotFound($"Orders for user with ID {userId} not found.");
            }
            var orderDtos = _mapper.Map<IEnumerable<OrderReadDto>>(orders);
            return orderDtos;
        }
    }
}

