using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Shots.Queries.GetShotDetail
{
    public class GetShotDetailQuery : IRequest<Shot>
    {
        public int Id { get; set; }
    }
}
