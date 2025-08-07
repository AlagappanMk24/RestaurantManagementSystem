using FluentValidation;

namespace Restaurant.Application.Features.Users.Queries.GetUserByName;

public class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
{
    public GetUserByNameQueryValidator()
    {
        RuleFor(dto => dto.FullName)
            .MaximumLength(100)
            .WithMessage("Max Length Of Name is 100 Characters");
    }
}
