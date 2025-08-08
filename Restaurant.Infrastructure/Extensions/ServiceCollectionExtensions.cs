using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Features.Restaurants.Commands.DeleteRestaurant;
using Restaurant.Application.Features.Users.Implementations;
using Restaurant.Application.Features.Users.Interfaces;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;
using Restaurant.Infrastructure.Data.Context;
using Restaurant.Infrastructure.Repositories;
using Restaurant.Infrastructure.Seeders;
using Restaurant.Infrastructure.Services;
using Restaurant.Infrastructure.Services.Authorize;
using Restaurants.Infrastructure.Repositories;

namespace Restaurant.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RestaurantDb");

        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging());

        services.AddIdentityApiEndpoints<ApplicationUser>()
           .AddRoles<IdentityRole>() // To Support the role claim in access token
                                     //.AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>() // To Add More Attributes In Token
           .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IRestaurantRepository, RestaurantsRepository>();
        services.AddScoped<IDishesRepository, DishesRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IRatingsRepository, RatingsRepository>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleSeeder, RoleSeeder>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ICategoryAuthorizationService, CategoryAuthorizationService>();
        services.AddScoped<ICustomerAuthorizationService, CustomerAuthorizationService>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();
        services.AddScoped<IOrderAuthorizationService, OrderAuthorizationService>();
        services.AddScoped<IRatingAuthorizationService, RatingAuthorizationService>();
        services.AddHttpContextAccessor();
    }
}
