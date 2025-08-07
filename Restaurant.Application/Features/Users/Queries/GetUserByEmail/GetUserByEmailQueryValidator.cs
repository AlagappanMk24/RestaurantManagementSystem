using FluentValidation;
using Restaurant.Application.Features.Users.Queries.GetUserByEmail;

namespace Restaurant.Application.User.Queries.GetUserByEmail
{
    public class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailQueryValidator()
        {
            RuleFor(c => c.Email)
                 .NotEmpty().WithMessage("Email is required")
                 .EmailAddress().WithMessage("Email must be a valid email address");
        }
    }
}
