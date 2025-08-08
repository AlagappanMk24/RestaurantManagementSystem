using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerByPhoneNumber;

public class GetCustomerByPhoneNumberQueryHandler(ILogger<GetCustomerByPhoneNumberQueryHandler> logger,
 IMapper mapper,
 ICustomerAuthorizationService customerAuthorizationService,
 ICustomerRepository customersRepository) : IRequestHandler<GetCustomerByPhoneNumberQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        var customer = await customersRepository.GetByPhoneNumberAsync(request.PhoneNumber)
               ?? throw new NotFoundPhoneNumberException(nameof(Customer), request.PhoneNumber);

        if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
            throw new ForbidException();

        var customerDto = mapper.Map<CustomerDto>(customer);


        logger.LogInformation("Getting Customer {CustomerPhoneNumber}", request.PhoneNumber);

        return customerDto;
    }
}
