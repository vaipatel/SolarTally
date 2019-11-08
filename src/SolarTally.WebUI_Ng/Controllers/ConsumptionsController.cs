using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarTally.Application.Consumptions.Queries.Dtos;
using SolarTally.Application.Consumptions.Queries.GetConsumptionById;
using System.Threading.Tasks;

namespace SolarTally.WebUI_Ng.Controllers
{
    // [Authorize]
    public class ConsumptionsController : BaseController
    {
        [HttpGet("{id}")]
        // [AllowAnonymous]
        public async Task<ActionResult<ConsumptionDto>> Get(int id)
        {
            return Ok(await Mediator.Send(new GetConsumptionByIdQuery()
            {
                Id = id
            }));
        }
    }
}