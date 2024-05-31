using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.ServicesAbstract
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressReadDto>> GetAllAddressesByUserIdAsync(Guid customerId);
        Task<AddressReadDto> GetAddressByIdAsync(Guid addressId);
        Task<AddressReadDto> CreateAddressAsync(AddressCreateDto addressDto, Guid userId);
        Task<bool> UpdateAddressByIdAsync(AddressUpdateDto address, Guid addressId);
        Task<bool> DeleteAddressByIdAsync(Guid addressId);
    }
}