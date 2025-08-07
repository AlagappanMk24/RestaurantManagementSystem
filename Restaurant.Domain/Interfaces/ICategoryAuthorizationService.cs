using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Interfaces;

public interface ICategoryAuthorizationService
{
    bool CanModifyCategory(Category category);
}