using MediatR;

namespace Restaurant.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand(int id) : IRequest
{
    public int Id { get; } = id;
}
