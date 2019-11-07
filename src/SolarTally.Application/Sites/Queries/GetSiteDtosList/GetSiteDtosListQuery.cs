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

namespace SolarTally.Application.Sites.Queries.GetSiteDtosList
{
    /// <summary>
    /// Request DTO for getting all the Sites in a SiteDtosListVm.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSiteDtosListQuery : IRequest<SiteDtosListVm>
    {
        // No props in our Query DTO at the moment. Maybe later we add UserId.
    }

    /// <summary>
    /// Handler for handling the GetSiteDtosListQuery request.
    /// </summary>
    public class GetSiteDtosListQueryHandler :
        IRequestHandler<GetSiteDtosListQuery, SiteDtosListVm>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteDtosListQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SiteDtosListVm> Handle(
            GetSiteDtosListQuery request,
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
            
            var vm = new SiteDtosListVm
            {
                SiteDtos = siteDtos,
                Count = siteDtos.Count
            };

            return vm;
        }
    }
}