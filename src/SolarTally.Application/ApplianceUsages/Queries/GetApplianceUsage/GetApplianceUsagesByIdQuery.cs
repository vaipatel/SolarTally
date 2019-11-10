using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
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
            var applianceUsages = await _context.ApplianceUsages
                .Where(s => s.ConsumptionId == request.ConsumptionId)
                .ToListAsync(cancellationToken);
            
            var auDtos = new List<ApplianceUsageDto>();

            foreach(var au in applianceUsages)
            {
                auDtos.Add(_mapper.Map<ApplianceUsageDto>(au));
            }

            var auListDto = new ApplianceUsagesListDto()
            {
                Items = auDtos
            };

            return auListDto;
        }
    }
}