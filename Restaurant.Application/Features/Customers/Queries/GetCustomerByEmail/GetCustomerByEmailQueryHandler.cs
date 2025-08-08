using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerByEmail;

public class GetCustomerByEmailQueryHandler(ILogger<GetCustomerByEmailQueryHandler> logger,
 ICustomerRepository customersRepository,
 ICustomerAuthorizationService customerAuthorizationService,
 IMapper mapper) : IRequestHandler<GetCustomerByEmailQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByEmailQuery request, CancellationToken cancellationToken)
    {
        var customer = await customersRepository.GetByEmailAsync(request.Email)
                ?? throw new NotFoundEmailException(nameof(Customer), request.Email);

        if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
            throw new ForbidException();

        var customerDto = mapper.Map<CustomerDto>(customer);

        logger.LogInformation("Getting Customer {CustomerEmail}", request.Email);

        return customerDto;
    }
}
