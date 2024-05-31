using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.RepoAbstract;
using ShopWeb.Service.src.DTOs;
using ShopWeb.Service.src.ServicesAbstract;

namespace ShopWeb.Service.src.Services
{
    public class AddressService : IAddressService
    {
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepo;
        public AddressService(IAddressRepository addressRepo, IMapper mapper)
        {
            _addressRepo = addressRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressReadDto>> GetAllAddressesByUserIdAsync(Guid customerId)
        {
            var addresses = await _addressRepo.GetAllAddressesByUserIdAsync(customerId);
            var addressDTo = _mapper.Map<IEnumerable<AddressReadDto>>(addresses);
            return addressDTo;
        }

        public async Task<AddressReadDto> GetAddressByIdAsync(Guid addressId)
        {
            var address = await _addressRepo.GetAddressByIdAsync(addressId);
            var addressReadDto = _mapper.Map<AddressReadDto>(address);
            return addressReadDto;
        }


        public async Task<AddressReadDto> CreateAddressAsync(AddressCreateDto addressDto, Guid userId)
        {
            // var address = addressDto.CreateAddress(new Address());
            var address = _mapper.Map<Address>(addressDto);
            address.UserId = userId;
            var createdAddress = await _addressRepo.CreateAddressAsync(address);
            var addressReadDto = _mapper.Map<AddressReadDto>(createdAddress);
            return addressReadDto;
        }

        public async Task<bool> UpdateAddressByIdAsync(AddressUpdateDto addressDto, Guid addressId)
        {
            // var existingAddress = await _addressRepo.GetAddressByIdAsync(addressId);
            // if (existingAddress == null)
            //     return false;
            var address = _mapper.Map<Address>(addressDto);
            // existingAddress = addressDto.UpdateAddress(existingAddress);
            return await _addressRepo.UpdateAddressByIdAsync(addressId, address);
        }

        public async Task<bool> DeleteAddressByIdAsync(Guid addressId)
        {
            return await _addressRepo.DeleteAddressByIdAsync(addressId);
        }
    }
}