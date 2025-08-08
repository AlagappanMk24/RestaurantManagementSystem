using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerById;
public class GetCustomerByIdQuery(int id) : IRequest<CustomerDto>
{
    public int Id { get; } = id;
}