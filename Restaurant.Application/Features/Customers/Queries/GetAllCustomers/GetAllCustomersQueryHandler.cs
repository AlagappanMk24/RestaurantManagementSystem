using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQueryHandler(ILogger<GetAllCustomersQueryHandler> logger,
 IMapper mapper,
 ICustomerAuthorizationService customerAuthorizationService,
 ICustomerRepository customersRepository) : IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerDto>>
{
    public async Task<PagedResult<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        if (!customerAuthorizationService.Authorize(new(), ResourceOperation.Read))
            throw new ForbidException();

        var (customers, totalCount) = await customersRepository.GetAllMatchingAsync(request.SearchPhrase,
            request.PageSize,
            request.PageNumber,
            request.SortBy,
            request.SortDirection);

        var customersDtos = mapper.Map<IEnumerable<CustomerDto>>(customers);

        var result = new PagedResult<CustomerDto>(customersDtos, totalCount, request.PageSize, request.PageNumber);

        logger.LogInformation("Getting all customers");

        return result;
    }
}
