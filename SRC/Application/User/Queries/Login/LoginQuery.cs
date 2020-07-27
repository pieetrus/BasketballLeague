using FluentValidation;
using MediatR;

namespace BasketballLeague.Application.User.Queries.Login
{
    public class LoginQuery : IRequest<Dto.User>
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class QueryValidator : AbstractValidator<LoginQuery>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

}
