using MediatR;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerByPhoneNumber;

public class GetCustomerByPhoneNumberQuery(string phoneNumber) : IRequest<CustomerDto>
{
    public string PhoneNumber { get; } = phoneNumber;
}
