namespace Restaurant.Application.Features.Users.Interfaces;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
