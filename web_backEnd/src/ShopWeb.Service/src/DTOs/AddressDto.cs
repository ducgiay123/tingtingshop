using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopWeb.Core.src.Entity;

namespace ShopWeb.Service.src.DTOs
{
    public class AddressCreateDto
    {
        public string StreetName { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public string UnitNumber { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

    }

    public class AddressReadDto
    {
        public Guid Id { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public string UnitNumber { get; set; } = string.Empty;
        // public string AddressLine1 { get; set; } = string.Empty;
        // public string AddressLine2 { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

    }

    public class AddressUpdateDto
    {
        // public int Id { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public string UnitNumber { get; set; } = string.Empty;
        // public string AddressLine1 { get; set; }
        // public string AddressLine2 { get; set; }
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        // public Address UpdateAddress(Address oldAddress)
        // {
        //     oldAddress.StreetName = StreetName;
        //     oldAddress.StreetNumber = StreetNumber;
        //     oldAddress.UnitNumber = UnitNumber;
        //     // oldAddress.AddressLine1 = AddressLine1;
        //     // oldAddress.AddressLine2 = AddressLine2;
        //     oldAddress.PostalCode = PostalCode;
        //     oldAddress.City = City;
        //     return oldAddress;
        // }
    }
}