using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Features.Orders.Dtos;
using Restaurant.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using Restaurant.Application.Features.Users.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Application.Customers.Dtos;

namespace Restaurant.Application.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryHandler(ILogger<GetOrdersByCustomerIdQueryHandler> logger,
     IMapper mapper,
     IOrderRepository ordersRepository,
     ICustomerRepository customersRepository,
     IOrderAuthorizationService orderAuthorizationService,
     IUserContext userContext) : IRequestHandler<GetOrdersByCustomerIdQuery, CustomerOrdersDto>
    {
        public async Task<CustomerOrdersDto> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await customersRepository.GetByIdAsync(request.CustomerId)
                     ?? throw new NotFoundException(nameof(Customer), request.CustomerId.ToString());

            var user = userContext.GetCurrentUser()
                ?? throw new ForbidException();

            if (!orderAuthorizationService.CanViewCustomerOrder(customer.ApplicationUserId))
                throw new ForbidException();

            var (orders, totalCount) = await ordersRepository.GetAllMatchingAsync(request.PageSize,
                request.PageNumber,
                request.SortBy,
                request.SortDirection);

            var ordersByCustomer = orders.Where(o => o.CustomerId == request.CustomerId);

            logger.LogInformation("Getting all orders By {CustomerId}", request.CustomerId);

            var pagedOrders = new PagedResult<OrderDto>(
                mapper.Map<List<OrderDto>>(ordersByCustomer),
                ordersByCustomer.Count(),
                request.PageSize,
                request.PageNumber);

            return new CustomerOrdersDto
            {
                Customer = mapper.Map<CustomerDto>(customer),
                OrdersPaged = pagedOrders
            };
        }
    }
}