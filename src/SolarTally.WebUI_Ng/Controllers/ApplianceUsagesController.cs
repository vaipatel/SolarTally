using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;
using SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesList;
using System.Threading.Tasks;

namespace SolarTally.WebUI_Ng.Controllers
{
    public class ApplianceUsagesController : BaseController
    {
        public async Task<ActionResult<ApplianceUsagesListDto>> GetAll()
        {
            return Ok(await Mediator.Send(new GetApplianceUsagesListQuery()));
        }
    }
}