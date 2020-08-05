using MediatR;

namespace BasketballLeague.Application.Profiles.Queries.GetProfileDetail
{
    public class GetProfileDetailQuery : IRequest<Profile>
    {
        public string Username { get; set; }
    }
}
