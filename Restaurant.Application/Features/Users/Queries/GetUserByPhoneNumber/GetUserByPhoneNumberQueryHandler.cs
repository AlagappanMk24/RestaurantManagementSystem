using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Application.User.Dtos;

namespace Restaurant.Application.Features.Users.Queries.GetUserByPhoneNumber;

public class GetUserByPhoneNumberQueryHandler(ILogger<GetUserByPhoneNumberQueryHandler> logger,
 IMapper mapper,
 UserManager<ApplicationUser> userManager) : IRequestHandler<GetUserByPhoneNumberQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByPhoneNumberQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting User {UserPhoneNumber}", request.PhoneNumber);

        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber!.Contains(request.PhoneNumber), cancellationToken: cancellationToken)
                ?? throw new NotFoundNameException(nameof(ApplicationUser), request.PhoneNumber.ToString());

        var userDto = mapper.Map<UserDto>(user);

        return userDto;
    }
}
