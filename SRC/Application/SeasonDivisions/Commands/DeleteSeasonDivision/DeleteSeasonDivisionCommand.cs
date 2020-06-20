using MediatR;

namespace BasketballLeague.Application.SeasonDivisions.Commands.DeleteSeasonDivision
{
    public class DeleteSeasonDivisionCommand : IRequest
    {
        public int Id { get; set; }
    }
}
