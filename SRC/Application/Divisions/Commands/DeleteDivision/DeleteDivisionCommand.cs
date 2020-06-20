using MediatR;

namespace BasketballLeague.Application.Divisions.Commands.DeleteDivision
{
    public class DeleteDivisionCommand : IRequest
    {
        public int Id { get; set; }
    }
}
