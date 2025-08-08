using AutoMapper;
using Restaurant.Application.Features.Customers.Commands.CreateCustomer;
using Restaurant.Application.Features.Customers.Commands.UpdateCustomer;
using Restaurant.Domain.Entities;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Dtos;
public class CustomersProfile : Profile
{
    public CustomersProfile()
    {
        CreateMap<Customer, CustomerDto>();

        CreateMap<CreateCustomerCommand, Customer>();
        CreateMap<UpdateCustomerCommand, Customer>();
    }
}