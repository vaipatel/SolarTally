using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Application.Appliances.Queries.Dtos;
using SolarTally.Application.ApplianceUsages.Queries.Dtos;

namespace
SolarTally.Application.ApplianceUsages.Queries.GetApplianceUsagesById
{
    /// <summary>
    /// Request DTO for getting appliance usages by the consumption Id (the 
    /// site id).
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetApplianceUsagesByIdQuery :
        IRequest<ApplianceUsagesListDto>
    {
        /// Id of the consumption ( == site id )
        public int ConsumptionId { get; set; }
    }

    /// <summary>
    /// Handler for handling the GetApplianceUsagesByIdQuery request.
    /// </summary>
    public class GetApplianceUsagesByIdQueryHandler :
        IRequestHandler<GetApplianceUsagesByIdQuery, ApplianceUsagesListDto>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetApplianceUsagesByIdQueryHandler(
            ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApplianceUsagesListDto> Handle(
            GetApplianceUsagesByIdQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                from au in _context.ApplianceUsages
                where au.ConsumptionId == request.ConsumptionId
                join appliance in _context.Appliances on au.Appliance.Id equals appliance.Id
                orderby au.Id
                select new { ApplianceUsage = au, Appliance = appliance };
            
            var queryOut = await 
                query.AsNoTracking().ToListAsync(cancellationToken);
            
            var auDtos = new List<ApplianceUsageDto>();
            foreach( var o in queryOut)
            {
                var auDto = _mapper.Map<ApplianceUsageDto>(o.ApplianceUsage);
                auDto.ApplianceDto = _mapper.Map<ApplianceDto>(o.Appliance);
                auDtos.Add(auDto);
            }
            
            var result = new ApplianceUsagesListDto
            {
                Items = auDtos,
            };

            return result;
        }
    }
}