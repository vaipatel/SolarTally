using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarTally.Application.Sites.Queries.Dtos;
using SolarTally.Application.Sites.Queries.GetSitesList;
using System.Threading.Tasks;

namespace SolarTally.WebUI_Ng.Controllers
{
    public class SitesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<SiteDetailsList>> GetAll()
        {
            return Ok(await Mediator.Send(new GetSitesListQuery()));
        }

        // [HttpPost]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesDefaultResponseType]
        // public async Task<IActionResult> Upsert(UpsertSiteCommand command)
        // {
        //     var id = await Mediator.Send(command);

        //     return Ok(id);
        // }

        // [HttpDelete("{id}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        // public async Task<IActionResult> Delete(int id)
        // {
        //     await Mediator.Send(new DeleteSiteCommand { Id = id });

        //     return NoContent();
        // }
    }
}