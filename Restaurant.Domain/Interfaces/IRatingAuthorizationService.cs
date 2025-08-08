using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces;

public interface IRatingAuthorizationService
{
    bool Authorize(Rating rating, ResourceOperation operation);
}
