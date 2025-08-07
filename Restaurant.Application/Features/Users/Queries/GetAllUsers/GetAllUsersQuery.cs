using MediatR;
using Restaurant.Application.Common;
using Restaurant.Domain.Constants;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<PagedResult<UserDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
