using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SolarTally.Application.Common.Interfaces;

namespace SolarTally.Application.Sites.Queries.GetSitesList
{
    /// <summary>
    /// Request DTO for getting all the sites in a SitesListVm.
    /// </summary>
    /// <remarks>
    /// The handler is nested with the request for Jason Taylor's practice.
    /// </remarks>
    public class GetSitesListQuery : IRequest<SitesListVm>
    {
        // No props in our Query DTO at the moment. Maybe later we add UserId.
        
        /// <summary>
        /// Handler for handling the GetSitesListsQuery request.
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

            public async Task<SitesListVm> Handle(GetSitesListQuery request,
                CancellationToken cancellationToken)
            {
                var sites = await _context.Sites
                    .ProjectTo<SiteDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
                
                var vm = new SitesListVm
                {
                    Sites = sites,
                    Count = sites.Count
                };

                return vm;
            }
        }
    }
}