using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarTally.Application.ApplianceUsages.Commands.AddApplianceUsage;
using SolarTally.Application.ApplianceUsages.Commands.UpdateApplianceUsage;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesById;
using SolarTally.Application.Consumptions.Queries.Dtos;
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
        public async Task<ActionResult<ApplianceUsageDto>> AddToConsumption(
            [FromBody] AddApplianceUsageCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost]
        public async Task<ActionResult<ConsumptionDto>> UpdateApplianceUsage(
            [FromBody] UpdateApplianceUsageCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}