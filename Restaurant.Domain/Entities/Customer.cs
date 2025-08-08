using System.ComponentModel.DataAnnotations;

namespace Restaurant.Domain.Entities;
public class Customer
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;

    [EmailAddress, MaxLength(100)]
    public string Email { get; set; } = default!;

    [Phone, MaxLength(15)]
    public string PhoneNumber { get; set; } = default!;

    // Navigation Property

    public ICollection<Rating> Ratings { get; set; } = [];
    public ICollection<Order> Orders { get; set; } = [];
    public ICollection<RestaurantEntity> FavoriteRestaurants { get; set; } = [];


    // الربط مع جدول (IdentityUser)        
    public string? ApplicationUserId { get; set; } = default!;
    //[ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? User { get; set; } = default!;
}
