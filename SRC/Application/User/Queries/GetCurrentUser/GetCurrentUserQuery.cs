using MediatR;

namespace BasketballLeague.Application.User.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<Dto.User>
    {
    }
}
