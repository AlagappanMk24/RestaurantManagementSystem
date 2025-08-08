using MediatR;
using Restaurant.Application.Features.Customers.Commands.CreateCustomer;

namespace Restaurant.Application.Features.Customers.Commands.CreateMultipleCustomers;

public class CreateMultipleCustomersCommand : IRequest<List<int>>
{
    public List<CreateCustomerCommand> Customers { get; set; } = new();
}
