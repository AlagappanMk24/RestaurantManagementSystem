using MediatR;
using Restaurant.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerByEmail;

public class GetCustomerByEmailQuery(string email) : IRequest<CustomerDto>
{
    public string Email { get; } = email;
}
