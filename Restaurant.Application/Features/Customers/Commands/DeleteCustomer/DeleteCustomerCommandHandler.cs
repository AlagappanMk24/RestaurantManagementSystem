using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger,
ICustomerRepository customersRepository,
ICustomerAuthorizationService customerAuthorizationService) : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customersRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

        if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Delete))
            throw new ForbidException();

        await customersRepository.DeleteAsync(customer);

        logger.LogInformation("Deleting Customer with id: {CustomerId}", request.Id);
    }
}
