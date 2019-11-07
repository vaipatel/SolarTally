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

namespace SolarTally.Application.Sites.Queries.GetSiteById
{
    /// <summary>
    /// Request DTO for getting single site by its Id.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSiteByIdQuery : IRequest<SitePartialDto>
    {
        /// Id of the site
        public int Id { get; set; }
    }

    /// <summary>
    /// Handler for handling the GetSiteByIdQuery request.
    /// </summary>
    public class GetSiteByIdQueryHandler :
        IRequestHandler<GetSiteByIdQuery, SitePartialDto>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSiteByIdQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SitePartialDto> Handle(GetSiteByIdQuery request,
            CancellationToken cancellationToken)
        {
            var query =
                from site in _context.Sites
                join consumption in _context.Consumptions on site.Id equals consumption.Id
                where site.Id == request.Id
                select new { Site = site, ConsumptionTotal = consumption.ConsumptionTotal };
            
            var queryRes = await
                query.AsNoTracking().SingleAsync(cancellationToken);
            
            var siteDto = _mapper.Map<SitePartialDto>(queryRes.Site);
            siteDto.ConsumptionTotal = queryRes.ConsumptionTotal;

            return siteDto;
        }
    }
}