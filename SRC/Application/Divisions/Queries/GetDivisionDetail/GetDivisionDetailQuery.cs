using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionDetail
{

    public class GetDivisionDetailQuery : IRequest<Division>
    {
        public int Id { get; set; }
    }
}