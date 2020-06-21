using BasketballLeague.Domain.Entities;
using MediatR;

namespace BasketballLeague.Application.FreeThrows.Queries.GetFreeThrowDetail
{
    public class GetFreeThrowDetailQuery : IRequest<FreeThrow>
    {
        public int Id { get; set; }
    }
}
