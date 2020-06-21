using MediatR;

namespace BasketballLeague.Application.Substitutions.Commands.DeleteSubstitution
{
    public class DeleteSubstitutionCommand : IRequest
    {
        public int Id { get; set; }
    }
}
