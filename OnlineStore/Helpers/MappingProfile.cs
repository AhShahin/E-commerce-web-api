using AutoMapper;
using OnlineStore.Dtos;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /*CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.Age, opt => {
                opt.MapFrom(src => src.DoB.CalculateAge());
            });*/

            CreateMap<User, UserForListDto>();
            CreateMap<User, UserForDetailedDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<Address, AddressForListDto>()
                .ForMember(dest => dest.CityName, opt => {
                    opt.MapFrom(src => src.City.Name);
                }).ForMember(dest => dest.CountryName, opt => {
                    opt.MapFrom(src => src.Country.Name);
                }); 
            CreateMap<AddressForUpdateDto, Address>();
            CreateMap<AddressForCreationDto, Address>();
            CreateProjection<Product, ProductForListDto>();

            CreateMap<Product, ProductForListDto>()
                .ForMember(dest => dest.Brand, opt =>
                {
                    opt.MapFrom(src => src.Brand.Name);
                }).ForMember(dest => dest.Style, opt =>
                {
                    opt.MapFrom(src => src.Style.Name);
                }).ForMember(dest => dest.Material, opt =>
                {
                    opt.MapFrom(src => src.Material.Name);
                }).ForMember(dest => dest.SelectedProductOption, opt =>
                {
                    opt.MapFrom(src => src.ProductOptions.Where(po => po.IsMain).First());
                }).ForMember(dest => dest.Colors, opt =>
                {
                    opt.MapFrom(src => src.ProductOptions.Select(po => po.Color));
                }).ForMember(dest => dest.Category, opt =>
                {
                    opt.MapFrom(src => src.Category.Name);
                }).ForMember(dest => dest.SubCategory, opt =>
                {
                    opt.MapFrom(src => src.SubCategory.Name);
                });
            CreateMap<Product, ProductForDetailsDto>();
            CreateMap<ProductForUpdateDto, Product>();
            CreateMap<ProductForCreationDto, Product>();
            CreateMap<Product, ProductsListForOrderDTO>()
                .ForMember(dest => dest.Brand, opt =>
                {
                    opt.MapFrom(src => src.Brand.Name);
                });

            CreateMap<ProductOptions, ProductOptionsForItemsList>()
            .ForMember(dest => dest.sizes, opt =>
            {
                opt.MapFrom(src => src.ProductOptions_Sizes
                .Select(c => new SizeForListDto { Id = c.SizeId, Name = c.Size.Name.ToString(), Quantity = c.Quantity }).ToList());
            });

            CreateMap<ProductOptionsForProductCreationDto, ProductOptions>();
            CreateMap<Order, OrderForListDto>()
                .ForMember(dest => dest.ShippingMethod, opt =>
                {
                    opt.MapFrom(src => src.shippingMethod.Name);
                })
                .ForMember(dest => dest.OrderStatus, opt =>
                {
                    opt.MapFrom(src => src.orderStatus.Name);
                })
                .ForMember(dest => dest.PaymentMethod, opt =>
                {
                    opt.MapFrom(src => src.UserPaymentMethod.paymentType.Name);
                });

            CreateMap<ProductCategory, ProductCategoryForListDto>();
            CreateMap<ProductSubCategory, ProductSubCtgForItemListDto>();
            CreateMap<OrderStatus, OrderStatusForListDto>();
            CreateMap<OrderForUpdateDto, Order>();
            CreateMap<OrderForCreationDto, Order>();
            CreateMap<OrderProduct, OrderProductForListDto>();
            CreateMap<Brand, BrandForListDto>();
            CreateMap<Image, ImagesForListDto>();
            CreateMap<Color, ColorForListDto>();
        }
    }
}
