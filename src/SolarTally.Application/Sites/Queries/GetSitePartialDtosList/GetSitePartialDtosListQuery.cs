using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;
using SolarTally.Application.Sites.Queries.Dtos;

namespace SolarTally.Application.Sites.Queries.GetSitePartialDtosList
{
    /// <summary>
    /// Request DTO for getting all the Sites in a SitePartialDtosListVm.
    /// </summary>
    /// <remarks>
    /// The handler is placed after the request.
    /// </remarks>
    public class GetSitePartialDtosListQuery : IRequest<SitePartialDtosListVm>
    {
        // No props in our Query DTO at the moment. Maybe later we add UserId.
    }

    /// <summary>
    /// Handler for handling the GetSitePartialDtosListQuery request.
    /// </summary>
    public class GetSitePartialDtosListQueryHandler :
        IRequestHandler<GetSitePartialDtosListQuery, SitePartialDtosListVm>
    {
        private readonly ISolarTallyDbContext _context;
        private readonly IMapper _mapper;

        public GetSitePartialDtosListQueryHandler(ISolarTallyDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SitePartialDtosListVm> Handle(
            GetSitePartialDtosListQuery request,
            CancellationToken cancellationToken)
        {
            var sites = await _context.Sites.ToListAsync(cancellationToken);
            var sitePartialDtos = new List<SitePartialDto>();
            foreach( var site in sites)
            {
                var sitePartialDto = _mapper.Map<SitePartialDto>(site);
                sitePartialDtos.Add(sitePartialDto);
            }
            
            var vm = new SitePartialDtosListVm
            {
                Sites = sitePartialDtos,
                Count = sitePartialDtos.Count
            };

            return vm;
        }
    }
}