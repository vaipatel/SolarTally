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

namespace SolarTally.Application.Sites.Queries.GetSitesList
{
    /// <summary>
    /// Request DTO for getting all the Sites in a SiteListVm.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSitesListQuery : IRequest<SitesListVm>
    {
        // No props in our Query DTO at the moment. Maybe later we add UserId.
    }

    /// <summary>
    /// Handler for handling the GetSitesListQuery request.
    /// </summary>
    public class GetSitesListQueryHandler :
        IRequestHandler<GetSitesListQuery, SitesListVm>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSitesListQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SitesListVm> Handle(
            GetSitesListQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                from site in _context.Sites
                join consumption in _context.Consumptions on site.Id equals consumption.Id
                select new { Site = site, ConsumptionTotal = consumption.ConsumptionTotal };
            
            var queryOut = await 
                query.AsNoTracking().ToListAsync(cancellationToken);
            
            var siteDtos = new List<SiteDto>();
            foreach( var o in queryOut)
            {
                var siteDto = _mapper.Map<SiteDto>(o.Site);
                siteDto.ConsumptionTotal = o.ConsumptionTotal;
                siteDtos.Add(siteDto);
            }
            
            var vm = new SitesListVm
            {
                SiteDtos = siteDtos,
                Count = siteDtos.Count
            };

            return vm;
        }
    }
}