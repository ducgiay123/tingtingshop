using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Core.src.RepoAbstract
{
    public interface IAddressRepository
    {
        Task<IEnumerable<Address>> GetAllAddressesByUserIdAsync(Guid customerId);
        Task<Address> GetAddressByIdAsync(Guid addressId);
        Task<Address> CreateAddressAsync(Address address);
        Task<bool> UpdateAddressByIdAsync(Guid addressId, Address address);
        Task<bool> DeleteAddressByIdAsync(Guid addressId);
    }
}