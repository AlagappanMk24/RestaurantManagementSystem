//using AutoMapper;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace Restaurants.Application.Dishes.Commands.UpdateDish
//{
//    public class UpdateDishCommandHandler(ILogger<UpdateDishCommandHandler> logger,
//    IDishesRepository dishesRepository,
//     IDishAuthorizationService dishAuthorizationService,
//    IMapper mapper) : IRequestHandler<UpdateDishCommand>
//    {
//        public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
//        {
//            logger.LogInformation("Updating Dish with id: {DishId} | Restaurant Id: {RestaurantId}",
//                              request.Id, request.RestaurantId);

//            var dish = await dishesRepository.GetByIdAsync(request.Id)
//                ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());

//            if (dish.RestaurantId != request.RestaurantId)
//                throw new NotFoundException(nameof(Dish), request.Id.ToString());

//            if (!dishAuthorizationService.Authorize(dish, ResourceOperation.Update))
//                throw new ForbidException();

//            mapper.Map(request, dish);

//            await dishesRepository.SaveChanges();
//        }
//    }

//}
