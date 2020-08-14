using MediatR;

namespace BasketballLeague.Application.Divisions.Queries.GetDivisionDetail
{

    public class GetDivisionDetailQuery : IRequest<DivisionDto>
    {
        public int Id { get; set; }
    }
}