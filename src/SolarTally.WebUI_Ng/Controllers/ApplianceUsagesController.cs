using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}