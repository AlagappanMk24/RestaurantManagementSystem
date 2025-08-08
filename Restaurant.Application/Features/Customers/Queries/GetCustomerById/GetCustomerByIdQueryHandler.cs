using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler(ILogger<GetCustomerByIdQueryHandler> logger,
 ICustomerRepository customersRepository,
 ICustomerAuthorizationService customerAuthorizationService,
 IMapper mapper) : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await customersRepository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException(nameof(Customer), request.Id.ToString());

        if (!customerAuthorizationService.Authorize(customer, ResourceOperation.Read))
            throw new ForbidException();

        var customerDto = mapper.Map<CustomerDto>(customer);

        //restaurantDto.LogoSasUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUrl);

        logger.LogInformation("Getting customer {CustomerId}", request.Id);

        return customerDto;
    }
}
