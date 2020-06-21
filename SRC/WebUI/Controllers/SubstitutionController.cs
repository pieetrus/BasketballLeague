using BasketballLeague.Application.Substitutions.Queries.GetSubstitutionsList;
using BasketballLeague.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketballLeague.WebUI.Controllers
{

    public class SubstitutionController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<Substitution>> GetAll()
        {
            return Ok(await Mediator.Send(new GetSubstitutionsListQuery()));
        }

        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Substitution>> Get(int id)
        //{
        //    var player = await Mediator.Send(new GetSubstitutionDetailQuery { Id = id });

        //    return Ok(player);
        //}

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesDefaultResponseType]
        //public async Task<IActionResult> Create([FromBody]CreateSubstitutionCommand command)
        //{
        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Update(int id, [FromBody] UpdateSubstitutionCommand command)
        //{
        //    command.Id = id;

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteSubstitutionCommand { Id = id });

        //    return NoContent();
        //}
    }
}