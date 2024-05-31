using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EcomShop.Application.src.DTO;
using ShopWeb.Core.src.Entity;
using ShopWeb.Core.src.Role;
using ShopWeb.Service.src.DTOs;

namespace ShopWeb.Service.src.Share
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryReadDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Role, opt =>
            {
                opt.MapFrom(src => src.Role == UserRole.Admin ? "Admin" : "Customer");
            });
            CreateMap<UserCreateDto, User>()
                        .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();

            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.imageUrls.Select(img => img.Url)));
            CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.imageUrls, opt => opt.MapFrom(src => src.Images.Select(url => new ImageUrl { Url = url })));
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderedLineItems))
                .ReverseMap();

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore Id when mapping from DTO to entity
                .ForMember(dest => dest.OrderedLineItems, opt => opt.MapFrom(src => src.OrderItems.Select(item => new OrderedLineItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                })))
                .ReverseMap();

            CreateMap<OrderItemCreateDto, OrderedLineItem>().ReverseMap();
            CreateMap<OrderedLineItem, OrderItemReadDto>().ReverseMap();

            CreateMap<Review, ReviewReadDto>()
               .ForMember(dest => dest.userName,
                          opt => opt.MapFrom(src => src.User != null
                              ? $"{src.User.FirstName} {src.User.LastName}"
                              : string.Empty));

            // Map ReviewCreateDto to Review
            CreateMap<ReviewCreateDto, Review>();

            // Map ReviewUpdateDto to Review
            CreateMap<ReviewUpdateDto, Review>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));


        }
    }
}