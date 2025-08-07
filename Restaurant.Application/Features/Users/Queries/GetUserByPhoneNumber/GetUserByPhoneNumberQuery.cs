using MediatR;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Queries.GetUserByPhoneNumber
{
    public class GetUserByPhoneNumberQuery(string phoneNumber) : IRequest<UserDto>
    {
        public string PhoneNumber { get; } = phoneNumber;
    }
}
