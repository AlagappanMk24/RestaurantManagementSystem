using MediatR;
using Restaurant.Application.Common;
using Restaurant.Domain.Constants;
using Restaurants.Application.Customers.Dtos;

namespace Restaurant.Application.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersQuery : IRequest<PagedResult<CustomerDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
