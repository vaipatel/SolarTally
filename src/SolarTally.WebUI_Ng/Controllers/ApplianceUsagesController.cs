using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarTally.Application.ApplianceUsages.Commands.AddApplianceUsage;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesById;
using System.Threading.Tasks;

namespace SolarTally.WebUI_Ng.Controllers
{
    public class ApplianceUsagesController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplianceUsagesListDto>> GetForConsumption(int id)
        {
            return Ok(await Mediator.Send(new GetApplianceUsagesByIdQuery()
            { ConsumptionId = id }));
        }

        [HttpPut("{id}")]
        // [ProducesResponseType(StatusCodes.Status204NoContent)]
        // [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApplianceUsageDto>> AddToConsumption([FromBody] AddApplianceUsageCommand command)
        {
            var resp = await Mediator.Send(command);

            return Ok(resp);
        }
    }
}