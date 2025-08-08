using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerByName;
public class GetCustomerByNameQuery(string name) : IRequest<CustomerDto>
{
    public string Name { get; } = name;
}