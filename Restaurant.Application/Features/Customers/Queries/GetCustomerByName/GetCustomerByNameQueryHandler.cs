using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerByName;

public class GetCustomerByNameQueryHandler(ILogger<GetCustomerByNameQueryHandler> logger,
 ICustomerRepository customersRepository,
    ICustomerAuthorizationService customerAuthorizationService,
 IMapper mapper) : IRequestHandler<GetCustomerByNameQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByNameQuery request, CancellationToken cancellationToken)
    {
        var customer = await customersRepository.GetByNameAsync(request.Name)
                ?? throw new NotFoundNameException(nameof(Customer), request.Name);

        if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
            throw new ForbidException();

        var customerDto = mapper.Map<CustomerDto>(customer);

        logger.LogInformation("Getting Customer {CustomerName}", request.Name);

        return customerDto;
    }
}
