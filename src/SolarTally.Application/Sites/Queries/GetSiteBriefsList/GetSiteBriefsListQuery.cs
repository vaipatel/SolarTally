using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Application.Sites.Queries.Dtos;

namespace SolarTally.Application.Sites.Queries.GetSiteBriefsList
{
    /// <summary>
    /// Request DTO for getting all the Sites in a SiteBriefsListDto.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSiteBriefsListQuery : IRequest<SiteBriefsListDto>
    {
        // No props in our Query DTO at the moment. Maybe later we add UserId.
    }

    /// <summary>
    /// Handler for handling the GetSiteBriefsListQuery request.
    /// </summary>
    public class GetSiteBriefsListQueryHandler :
        IRequestHandler<GetSiteBriefsListQuery, SiteBriefsListDto>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteBriefsListQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SiteBriefsListDto> Handle(
            GetSiteBriefsListQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                from site in _context.Sites
                join consumption in _context.Consumptions on site.Id equals consumption.Id
                orderby site.Id
                select new { Site = site, ConsumptionTotal = consumption.ConsumptionTotal };
            
            var queryOut = await 
                query.AsNoTracking().ToListAsync(cancellationToken);
            
            var siteDtos = new List<SiteBriefDto>();
            foreach( var o in queryOut)
            {
                var siteDto = _mapper.Map<SiteBriefDto>(o.Site);
                siteDto.ConsumptionTotal = o.ConsumptionTotal;
                siteDtos.Add(siteDto);
            }
            
            var result = new SiteBriefsListDto
            {
                Items = siteDtos,
            };

            return result;
        }
    }
}