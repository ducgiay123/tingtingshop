using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopWeb.API.Database;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;

namespace ShopWeb.API.Repo
{
    public class AddressRepository : IAddressRepository
    {
        private readonly WebShopDbContext _dbContext;

        public AddressRepository(WebShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesByUserIdAsync(Guid customerId)
        {
            var customerAddresses = await _dbContext.Addresses
                .Where(address => address.UserId == customerId)
                .ToListAsync();
            return customerAddresses;
        }
        public async Task<Address> GetAddressByIdAsync(Guid addressId)
        {
            return await _dbContext.Addresses.FindAsync(addressId);
        }

        public async Task<Address> CreateAddressAsync(Address address)
        {
            _dbContext.Addresses.Add(address);
            await _dbContext.SaveChangesAsync();

            return address;
        }

        public async Task<bool> UpdateAddressByIdAsync(Guid addressId, Address address)
        {
            var existingAddress = await _dbContext.Addresses.FindAsync(addressId);
            if (existingAddress == null)
                return false;

            existingAddress.StreetName = address.StreetName;
            existingAddress.StreetNumber = address.StreetNumber;
            existingAddress.UnitNumber = address.UnitNumber;
            existingAddress.PostalCode = address.PostalCode;
            existingAddress.City = address.City;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAddressByIdAsync(Guid addressId)
        {
            var address = await _dbContext.Addresses.FindAsync(addressId);

            if (address is null)
            {
                return false;
            }

            _dbContext.Addresses.Remove(address);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}