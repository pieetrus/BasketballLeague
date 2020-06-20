using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Referees.Queries.GetRefereeDetail
{

    public class GetRefereeDetailQuery : IRequest<Referee>
    {
        public int Id { get; set; }
    }
}