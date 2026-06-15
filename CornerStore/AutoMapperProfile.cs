using CornerStore.Models;
using CornerStore.Models.DTOs;
using AutoMapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Cashier, CashierDTO>();
        CreateMap<CashierDTO, Cashier>();
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
        CreateMap<OrderProduct, OrderProductDTO>();
        CreateMap<OrderProductDTO, OrderProduct>();
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();
    }
}